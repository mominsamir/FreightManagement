import React from 'react';
import { Form,Button, Col, Input, Modal, Row } from 'antd';
import { Messages } from 'components';
import { useHistory } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { handleErrors } from 'Utils/errorHandler';
import { AppDispatch } from 'redux/store';
import customerServices from 'services/customer';
import { Customer } from 'types/customer';

interface Props {
  customer?: Customer;
  cancel: () => void;
}

const CreateCustomer: React.FC<Props> = ({ customer, cancel }: Props) => (
    <Modal
        visible={true}
        title="Create new Customer"
        onCancel={cancel}
        width="80%"
        footer={null}
        maskClosable={false}>
        <Messages />
        <CustomerCreateModel customer={customer} onOk={cancel} onCancel={cancel} />
    </Modal>
);


interface CreateProps {
    customer?: Customer;
    onOk: () => void;
    onCancel: () => void;
  }
  
  const CustomerCreateModel: React.FC<CreateProps> = ({ customer, onOk, onCancel }: CreateProps) => {
    const history = useHistory();
    const dispatch = useDispatch<AppDispatch>();
    const [form] = Form.useForm();
  
    const save = (values: Customer) => {
      (async () => {
        try {
            await customerServices.create(values);
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
          initialValues={customer}>
        <Row gutter={8} >
          <Col span={6}>
            <Form.Item
                hidden={true}
                name="id">
                <Input  placeholder="id"></Input>
                </Form.Item>            
            <Form.Item
              name="name"
              rules={[{ required: true, message: 'Name is required.' }]}
              label="Name">
              <Input allowClear={true} placeholder="Name"></Input>
            </Form.Item>
          </Col>
          <Col span={6}>
            <Form.Item
              name="email"
              rules={[{ required: true, message: 'Email is required.' }]}
              label="Email">
              <Input allowClear={true} placeholder="Email"></Input>
            </Form.Item>
          </Col>
        </Row>
        <Row gutter={8} >
            <Col span={6}>
                <Form.Item
                    name="street"
                    rules={[{ required: true, message: 'Street is required.' }]}
                    label="Street">
                    <Input allowClear={true} placeholder="Street"></Input>
                </Form.Item>
            </Col>
            <Col span={6}>
                <Form.Item
                    name="city"
                    rules={[{ required: true, message: 'City is required.' }]}
                    label="City">
                    <Input allowClear={true} placeholder="City"></Input>
                </Form.Item>
            </Col>
            <Col span={6}>
                <Form.Item
                    name="state"
                    rules={[{ required: true, message: 'state is required.' }]}
                    label="State">
                    <Input allowClear={true} placeholder="state"></Input>
                </Form.Item>
            </Col>                        
        </Row>
        <Row gutter={8} >
            <Col span={6}>
                <Form.Item
                    name="zipCode"
                    rules={[{ required: true, message: 'ZipCode is required.' }]}
                    label="ZipCode">
                    <Input allowClear={true} placeholder="ZipCode"></Input>
                </Form.Item>
            </Col>
            <Col span={6}>
                <Form.Item
                    name="country"
                    rules={[{ required: true, message: 'Country is required.' }]}
                    label="Country">
                    <Input allowClear={true} placeholder="Country"></Input>
                </Form.Item>
            </Col>
        </Row>        
        <Row>
          <Col span={20}></Col>
          <Col span={2}>
            <Form.Item>
              <Button type="primary" htmlType="submit">Save</Button>
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
  


export default CreateCustomer;