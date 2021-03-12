import React from 'react';
import { Form, Col, Row, Button, Input, Checkbox } from 'antd';
import { handleErrors } from 'Utils/errorHandler';
import { useHistory } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { AppDispatch } from 'redux/store';
import rackService from 'services/rack';
import { Rack } from 'types/rack';

interface Props {
  rack?: Rack;
  mode: 'EDIT' | 'ADD';
  onOk: () => void;
  onCancel: () => void;
}

const RackModel: React.FC<Props> = ({ rack, mode, onOk, onCancel }: Props) => {
  const history = useHistory();
  const dispatch = useDispatch<AppDispatch>();
  const [form] = Form.useForm();

  const save = (values: Rack) => {
    (async () => {
      try {
        if(mode ==='ADD'){
          await rackService.create(values);
        }else{
          await rackService.update(values.id ,values);
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
        initialValues={rack}>
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
              name="irsCode"
              rules={[{ required: true, message: 'IRS Code is required.' }]}
              label="IRS Code">
              <Input allowClear={true} placeholder="IRS Code"></Input>
            </Form.Item>
          </Col>
            {mode ==='EDIT' && (
                <Col span={6}>
                    <Form.Item
                        name="isActive"
                        valuePropName="checked"
                        rules={[{ required: true, message: 'Check is requred.' }]}
                        label="Is Active">
                        <Checkbox></Checkbox>                         
                    </Form.Item>
                </Col>                
            )}             
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

export default RackModel;
