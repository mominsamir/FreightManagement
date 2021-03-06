import React, { FC, useState } from 'react';
import { Row, Col, Form, Input, Button } from 'antd';
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

const Login: FC = () => {
//  const orgInfo = useSelector(orgInfoSelector.getOrgInfo);
  const [isUserLoggedIn,setUserLoggedIn] = useState<boolean>(false);
  const location = useLocation();


  if (isUserLoggedIn) {
    return null;
  }
  
  return (
    <Row justify="space-around" align="middle" style={{height:'70vh'}}>
      <Col xs={{ span: 12 }} lg={{ span: 8 }}>
          <h1>Welcome to Dispatch</h1>
          <h3>Please Login</h3>
          <LoginForm location={location.toString()}></LoginForm>
      </Col>
    </Row>
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
      <Form.Item name="email" rules={[{ required: true, message: 'Please enter your email!' }]}>
        <Input prefix={<UserOutlined style={{ color: 'rgba(0,0,0,.25)' }} />} placeholder="Email" />
      </Form.Item>
      <Form.Item name="password" rules={[{ required: true, message: 'Please enter your Password!' }]}>
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

export default Login;