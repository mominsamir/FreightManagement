import React from 'react';
import { Form, Col, Row, Button, Input, Select } from 'antd';
import { handleErrors } from 'Utils/errorHandler';
import { useHistory } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { AppDispatch } from 'redux/store';
import { User } from 'types/user';
import userService from 'services/user';

interface Props {
  user?: User;
  mode: 'EDIT' | 'ADD';
  onOk: () => void;
  onCancel: () => void;
}

const UserModel: React.FC<Props> = ({ user, mode, onOk, onCancel }: Props) => {
  const history = useHistory();
  const dispatch = useDispatch<AppDispatch>();
  const [form] = Form.useForm();

  const save = (values: User) => {
    (async () => {
      try {
        console.log(values);
        if(mode ==='ADD'){
          await userService.create(values);
        }else{
          await userService.update(values.id ,values);
        }
        onOk();
      } catch (error) {
        handleErrors(error, history, dispatch, form);
      }
    })();
  };

  return (
    <div>
      <Form 
        form={form}
        layout='vertical' 
        onFinish={save} 
        initialValues={user}>
        <Row gutter={8} >
          <Col span={8}>
          <Form.Item
              hidden={true}
              name="id">
              <Input  placeholder="id"></Input>
            </Form.Item>            
            <Form.Item
              name="firstName"
              rules={[{ required: true, message: 'FirstName is required.' }]}
              label="First Name">
              <Input allowClear={true} placeholder="First Name"></Input>
            </Form.Item>
          </Col>
          <Col span={8}>
            <Form.Item
              name="lastName"
              rules={[{ required: true, message: 'Last name is required.' }]}
              label="Last Name">
              <Input allowClear={true} placeholder="Last Name"></Input>
            </Form.Item>
          </Col>        
         </Row>
        <Row gutter={8} >
            <Col span={8}>
                <Form.Item
                    name="email"
                    rules={[{ required: true, message: 'Email is required.' }]}
                    label="Emaik">
                    <Input allowClear={true} placeholder="Email"></Input>
                </Form.Item>
            </Col>
            <Col span={8}>
                <Form.Item
                    name="role"
                    rules={[{ required: true, message: 'Role is required.' }]}
                    label="Role">
                    <Select  style={{ width: 120 }} >
                        <Select.Option value="ADMIN">Admin</Select.Option>
                        <Select.Option value="DISPATCHER">Dispatcher</Select.Option>
                        <Select.Option value="DRIVER">Driver</Select.Option>
                    </Select>                            
                </Form.Item>
            </Col>
        </Row>
        <Row>
          <Col span={20}></Col>
          <Col span={2}>
            <Form.Item>
              <Button type="primary" htmlType="submit">
              {mode === 'ADD'? 'Save' : 'Update'} 
              </Button>
            </Form.Item>
          </Col>
          <Col span={2}>
            <Form.Item>
              <Button type="text" htmlType="button" onClick={onCancel}>
                 Cancel
              </Button>
            </Form.Item>
          </Col>
        </Row>
      </Form>
    </div>
  );
};

export default UserModel;
