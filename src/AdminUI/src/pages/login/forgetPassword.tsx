import React, { FC } from 'react';
import { Layout,  Row, Col, Form, Input, Button } from 'antd';
import { LoginOutlined,  UserOutlined } from '@ant-design/icons';
import { useDispatch, useSelector } from 'react-redux';
//import orgInfoSelector from 'redux/selectors/orgInfo';
import styles from './Login.module.less';
import { UserIdendity } from 'types/user';
import services from 'services';
import * as messagesAction from 'redux/slices/messages';
import { AppDispatch } from 'redux/store';


const ForgetPassword: FC = () => {
//  const orgInfo = useSelector(orgInfoSelector.getOrgInfo);
  
  const { Footer,  Content } = Layout;

  
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
              <h3>Forget Password</h3>
              <ForgetPasswordForm></ForgetPasswordForm>
            </div>
          </Col>
        </Row>
      </Content>
      <Footer>

      </Footer>
  </Layout>

);
}

const ForgetPasswordForm: FC = () => {
  const [form] = Form.useForm();
  const dispatch = useDispatch<AppDispatch>();
  

  const handleSubmit = (values:UserIdendity) => {
    (async () => {
      try {
        await services.configService.login(values);
      } catch (error) {
        dispatch(messagesAction.addError(['Invalid email provided.']));
      }
    })();
  };


  return (
    <Form form={form} onFinish={handleSubmit} className="login-form">
      <div className={styles.line}></div>
      <Form.Item name="email" rules={[{ required: true, message: 'Please input your registered email!' }]}>
        <Input prefix={<UserOutlined style={{ color: 'rgba(0,0,0,.25)' }} />} placeholder="Email" />
      </Form.Item>
      <Form.Item>
        <Button htmlType="submit" type="primary" icon={<LoginOutlined />}>
          Reset Password
        </Button>
      </Form.Item>
    </Form>
  );
}

export default ForgetPassword;