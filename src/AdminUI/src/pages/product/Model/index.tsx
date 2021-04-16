import React from 'react';
import { Form, Col, Row, Button, Input, Select, Checkbox } from 'antd';
import { handleErrors } from 'Utils/errorHandler';
import { useHistory } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { AppDispatch } from 'redux/store';
import { FuelProduct } from 'types/product';
import productService from 'services/product';

interface Props {
  fuelProduct?: FuelProduct;
  mode: 'EDIT' | 'ADD';
  onOk: () => void;
  onCancel: () => void;
}

const FuelProductModel: React.FC<Props> = ({ fuelProduct, mode, onOk, onCancel }: Props) => {
  const history = useHistory();
  const dispatch = useDispatch<AppDispatch>();
  const [form] = Form.useForm();

  const save = (values: FuelProduct) => {
    (async () => {
      try {
        if(mode ==='ADD'){
          await productService.create(values);
        }else{
          await productService.update(values.id ,values);
        }
        onOk();
      } catch (error) {
        console.log(error);        
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
        initialValues={fuelProduct}>
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
              name="grade"
              rules={[{ required: true, message: 'grade is required.' }]}
              label="Fuel Grade">
                <Select  style={{ width: 120 }} >
                    <Select.Option value={0}>Regular 87</Select.Option>
                    <Select.Option value={1}>Plus 89</Select.Option>
                    <Select.Option value={2}>Super 93</Select.Option>
                    <Select.Option value={3}>Diesel Clear</Select.Option>
                    <Select.Option value={4}>Diesel Dyed</Select.Option>                                        
                </Select> 
            </Form.Item>
          </Col>        
            <Col span={6}>
                <Form.Item
                    name="uom"
                    rules={[{ required: true, message: 'UOM is required.' }]}
                    label="UOM">
                    <Select style={{ width: 120 }} >
                        <Select.Option value={1}>Gallon</Select.Option>
                        <Select.Option value={0}>Unit</Select.Option>
                        <Select.Option value={2}>Package</Select.Option>
                    </Select>                            
                </Form.Item>
            </Col>
            <Col span={6}>
                <Form.Item
                    name="isActive"
                    valuePropName="checked"
                    rules={[{ required: true, message: 'Check is requred.' }]}
                    label="Is Active">
                  <Checkbox></Checkbox>                         
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

export default FuelProductModel;
