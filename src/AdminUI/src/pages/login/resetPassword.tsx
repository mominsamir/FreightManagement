import React, { FC, useState } from 'react';
import { Layout,  Row, Col, Form, Input, Button } from 'antd';
import { LoginOutlined,  UserOutlined, LockOutlined } from '@ant-design/icons';
import { useDispatch, useSelector } from 'react-redux';
//import orgInfoSelector from 'redux/selectors/orgInfo';
import { Link, useLocation } from 'react-router-dom';
import styles from './Login.module.less';
import { UserIdendity } from 'types/user';
import services from 'services';
import * as messagesAction from 'redux/slices/messages';
import { AppDispatch } from 'redux/store';
//import configSelector from 'redux/selectors/config';

interface LoginProps  {
  location: string
}

const ResetPassword: FC = () => {
 // const orgInfo = useSelector(orgInfoSelector.getOrgInfo);
 const [isUserLoggedIn,setUserLoggedIn] = useState<boolean>(false);
  const location = useLocation();
  
  const { Footer,  Content } = Layout;

  if (isUserLoggedIn) {
    return null;
  }
  
  return (
    <Layout style={{minHeight: '100vh'}}>
      <Content>
        <Row>
          <Col
            xxl={{ span: 6, offset: 9 }}
            lg={{ span: 8, offset: 8 }}
            md={{ span: 12, offset: 6 }}
            sm={{ span: 20, offset: 2 }}
            xs={24} >
            <div >
            <h1>Welcome to orgInfo.orgName</h1>
              <h3>Please Login</h3>
              <LoginForm location={location.toString()}></LoginForm>
            </div>
          </Col>
        </Row>
      </Content>
      <Footer>

      </Footer>
  </Layout>

);
}

const LoginForm: FC<LoginProps> = ({location}:LoginProps) => {
  const [form] = Form.useForm();
  const dispatch = useDispatch<AppDispatch>();
  

  const handleSubmit = (values:UserIdendity) => {
    (async () => {
      try {
        await services.configService.login(values);
       window.location.href = location;
      } catch (error) {
        dispatch(messagesAction.addError(['Invalid email/password provided.']));
      }
    })();
  };


  return (
    <Form form={form} onFinish={handleSubmit} className="login-form">
      <div className={styles.line}></div>
      <Form.Item name="email" rules={[{ required: true, message: 'Please input your email!' }]}>
        <Input prefix={<UserOutlined style={{ color: 'rgba(0,0,0,.25)' }} />} placeholder="Email" />
      </Form.Item>
      <Form.Item name="password" rules={[{ required: true, message: 'Please input your Password!' }]}>
        <Input prefix={<LockOutlined style={{ color: 'rgba(0,0,0,.25)' }} />} type="password" placeholder="Password" />
      </Form.Item>
      <Form.Item>
        <Button htmlType="submit" type="primary" icon={<LoginOutlined />}>
          Log in
        </Button>
        <Link to="/forgot-password" className={styles.forgotPassword}>
          Forgot password
        </Link>
      </Form.Item>
    </Form>
  );
}

export default ResetPassword;