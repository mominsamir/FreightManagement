import React, { FC } from 'react';
import { Form, Input} from 'antd';
import { LockOutlined } from '@ant-design/icons';
import { useDispatch } from 'react-redux';
import styles from './Login.module.less';
import Modal from 'antd/lib/modal/Modal';
import { FormInstance } from 'antd/lib/form';
import { ChangePassword } from 'types/config';
import { AppDispatch } from 'redux/store';
import { handleErrors } from 'Utils/errorHandler';
import { useHistory } from 'react-router-dom';
import configService from 'services/config';


interface ChangePasswordFormModelProps {
  visible: boolean;
  onCancel: () => void;
}


export const ChangePasswordModel: FC<ChangePasswordFormModelProps> = ({  visible, onCancel}) => {
  const [form] = Form.useForm();
  const dispatch = useDispatch<AppDispatch>();
  const history = useHistory();
  
  const changePassword = (changePassword: ChangePassword) =>{
    (async () => {
      try {
        await configService.changePassword(changePassword);
      } catch (error) {
        handleErrors(error,history,dispatch,form) 
      }
    })();    

  } 

  return (
    <Modal
      title="Change Password"
      okText="Change Password"
      cancelText="Cancel"    
      onCancel={onCancel}
      onOk={() => {
        form
          .validateFields()
          .then(values => {
            changePassword(values as ChangePassword);
            form.resetFields();            
          })
          .catch(info => {
            console.log('Validate Failed:', info);
          });
      }}    
      visible={visible}>
        <ChangePasswordForm form={form} ></ChangePasswordForm>
    </Modal>

    );
}

interface ChangePasswordFormProps {
  form: FormInstance;
}


const ChangePasswordForm: FC<ChangePasswordFormProps> = (param) => {

  return (
    <Form form={param.form} className="login-form" >
      <div className={styles.line}></div>
      <Form.Item name="oldPassword" rules={[{ required: true, message: 'Please enter your current password!' }]}>
        <Input prefix={<LockOutlined style={{ color: 'rgba(0,0,0,.25)' }} />} type="password" placeholder="Current Password" />
      </Form.Item>
      <Form.Item name="password" rules={[{ required: true, message: 'Please enter your Password!' }]}>
        <Input prefix={<LockOutlined style={{ color: 'rgba(0,0,0,.25)' }} />} type="password" placeholder="New password" />
      </Form.Item>      
      <Form.Item name="confirmPassword"
        rules={[
          {
            required: true,
            message: 'Please confirm your password!',
          },
          ({ getFieldValue }) => ({
            validator(rule, value) {
              if (!value || getFieldValue('password') === value) {
                return Promise.resolve();
              }
              return Promise.reject('The two passwords that you entered do not match!');
            },
          }),
        ]}>
        <Input prefix={<LockOutlined style={{ color: 'rgba(0,0,0,.25)' }} />} type="password" placeholder="Confirm Password" />
      </Form.Item>
    </Form>
  );
}

export default ChangePassword;