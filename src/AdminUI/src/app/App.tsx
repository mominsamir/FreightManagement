import React, { useEffect, useState } from 'react';
import {Helmet} from "react-helmet";
import { Switch, Route, useHistory } from 'react-router-dom';
import { AppDispatch } from 'redux/store';
import { useDispatch } from 'react-redux';
import { useLocation, Redirect } from 'react-router-dom';
import { Messages, Menu as AntMenu } from 'components';
import {  Menu as AMenu , Dropdown, Layout,Row, Col} from 'antd';
import { withEventBus } from 'context/eventbus';
import pages from 'pages';
import * as messagesActions from 'redux/slices/messages';
import styles from './App.module.less';
import { handleErrors } from 'Utils/errorHandler';
import configService from 'services/config';
import { UserOutlined } from '@ant-design/icons';
import { ChangePasswordModel } from 'pages/login/changePassword';
import { Menu } from 'components/Menu/menu';
import { User } from 'types/user';


const App: React.FC = () => {
  const dispatch = useDispatch<AppDispatch>();
  const [visible, setVisible] = useState<boolean>(false);
  const location = useLocation();
  const history = useHistory();
  const { Header, Content } = Layout;
  const [isUserLoggedIn, setUserLoggedIn] = useState<boolean>(false);
  const [menus, setMenus] = useState<Menu[]>([]);
  const [user, setUser] = useState<User>();

  const logout = () => {
    (async () => {      
    await configService.logout();
    //clear store details
//    dispatch(configAction.clear());
    window.location.href = '/login';
    })();
  }

  useEffect(() => {
    (async () => {
      try {

        let config = await configService.loadConfig();
        setUserLoggedIn(true);
        setMenus(config.menus);
        setUser(config.user);

      } catch (err) {
        handleErrors(err, history, dispatch);
      }
    })();
  }, [dispatch,history]);

 
  if (!isUserLoggedIn && location.pathname !== '/login') {
    return <Redirect to={encodeURI(`/login?redirect=${location.pathname}`)} />;
  }

  if (isUserLoggedIn && location.pathname === '/login') {
    return <Redirect to={encodeURI(`/dispatch/home`)} />;
  }


  const dropDownMenu = (
    <AMenu >
      <AMenu.Item key="1" onClick={()=>setVisible(true)}>Change password</AMenu.Item>      
      <AMenu.Item key="2" onClick={logout}>Log out</AMenu.Item>
    </AMenu>
  );


  return (
    <div>
      <ScrollToTop />
        <Helmet>
          <title>Dispatch App</title>
        </Helmet>
        <ChangePasswordModel visible={visible} onCancel={()=>setVisible(false)} ></ChangePasswordModel>      
        <Layout className={styles.App}>
          {isUserLoggedIn && (
            <Header style={{ position: 'fixed', zIndex: 99, width: '100%', height: '65px' }}>
            <Row>
            <Col span={3}>
              <div className={styles.logo}>
                <span className={styles.suffix}>Dispatch App</span>
              </div>
            </Col>
            <Col span={1}>
              <div className={styles.logo}>
              </div>
            </Col>            
            <Col span={17}>
              <Row>
              <div style={{paddingRight:16}}>
              </div>
              <div>
               {menus.length !== 0 && (<AntMenu menu={menus}></AntMenu>)}
              </div>
              </Row>
            </Col>
            <Col span={3} style={{display: 'block', textAlign: 'right'}}>
              <Dropdown.Button  overlay={dropDownMenu} icon={<UserOutlined />}>
                  {user?.firstName}
              </Dropdown.Button>
            </Col>
          </Row>
          </Header>
            )
          }
          <Layout className={styles.content}>
            {isUserLoggedIn && (
              <Row>
                  <Col span={24}>
                      <div className={styles.messageContainer}>
                        <Messages />
                      </div>
                  </Col>
                </Row>
              )}
              <Content style={{padding:8}} >
                <Switch>
                  <Route path="/login" exact component={pages.Login} />
                  <Route path="/dispatch/home" component={pages.Homepage} />
                  <Route path="/dispatch/products" exact component={pages.Homepage} />
                  <Route path="/dispatch/products/add"  exact component={pages.Homepage} />                  
                  <Route path="/forgot-password" component={pages.ForgetPassword} />
                  <Route path="/dispatch/Users" component={pages.UserPages.List} />
                  <Route path="/dispatch/fuelProducts" component={pages.ProductPages.List} />
                  <Route path="/dispatch/racks" component={pages.RackPages.List} />
                  <Route path="/dispatch/trailers" component={pages.TrailerPages.List} />
                  <Route path="/dispatch/trucks" component={pages.TruckPages.List} />
                  <Route path="/dispatch/vendors" component={pages.VendorPages.List} />       
                  <Route path="/dispatch/schedules/:id" component={pages.DriverScheduleList.View} />                   
                  <Route path="/dispatch/schedules" component={pages.DriverScheduleList.List} />
                  <Route path="/dispatch/locations/edit/:id" component={pages.LocationPages.Edit} />
                  <Route path="/dispatch/locations/add" component={pages.LocationPages.Add} />                  
                  <Route path="/dispatch/locations/:id" component={pages.LocationPages.View} />
                  <Route path="/dispatch/locations" component={pages.LocationPages.List} />   
                  <Route path="/dispatch/customers/edit/:id" component={pages.CustomerPages.Edit} />                  
                  <Route path="/dispatch/customers/:id" component={pages.CustomerPages.View} />
                  <Route path="/dispatch/customers" component={pages.CustomerPages.List} />
                  <Route path="/dispatch/orders/edit/:id" component={pages.OrderPages.Edit} />                  
                  <Route path="/dispatch/orders/add" component={pages.OrderPages.Add} />
                  <Route path="/dispatch/orders/:id" component={pages.OrderPages.View} />                  
                  <Route path="/dispatch/orders" component={pages.OrderPages.List} />
                  <Route path="/unknown-error" component={pages.ErrorPages.UnknownError} />
                  <Route path="/not-authorized" component={pages.ErrorPages.NotAuthorized} />
                  <Route path="*" component={pages.ErrorPages.NotFound} />
                </Switch>
              </Content>
            </Layout>
          </Layout>
      <MessagesCleaner />
    </div>
  );
};

function ScrollToTop() {
  const { pathname } = useLocation();

  useEffect(() => {
    window.scrollTo(0, 0);
  }, [pathname]);

  return null;
}

const MessagesCleaner = withEventBus((props) => {
  const dispatch = useDispatch();

  useEffect(() => {
    let subscription = props.eventBusSubscribe('START_REQUEST', () => dispatch(messagesActions.reset()));

    return () => subscription.unsubscribe();
  }, [dispatch, props]);

  return null;
});

export default App;
