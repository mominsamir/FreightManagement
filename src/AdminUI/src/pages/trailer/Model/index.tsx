import React from 'react';
import { Form, Col, Row, Button, Input,  InputNumber } from 'antd';
import { handleErrors } from 'Utils/errorHandler';
import { useHistory } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { AppDispatch } from 'redux/store';
import trailerService from 'services/trailer';
import { Trailer } from 'types/vehicle';

interface Props {
  trailer?: Trailer;
  mode: 'EDIT' | 'ADD';
  onOk: () => void;
  onCancel: () => void;
}

const TrailerModel: React.FC<Props> = ({ trailer, mode, onOk, onCancel }: Props) => {
  const history = useHistory();
  const dispatch = useDispatch<AppDispatch>();
  const [form] = Form.useForm();

  const save = (values: Trailer) => {
    (async () => {
      try {
        if(mode ==='ADD'){
          await trailerService.create(values);
        }else{
          await trailerService.update(values.id ,values);
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
        initialValues={trailer}>
        <Row gutter={8} >
          <Col span={6}>
            <Form.Item
                hidden={true}
                name="id">
                <Input  placeholder="id"></Input>
                </Form.Item>            
            <Form.Item
              name="numberPlate"
              rules={[{ required: true, message: 'Number is required.' }]}
              label="Number">
              <Input allowClear={true} placeholder="Name"></Input>
            </Form.Item>
          </Col>
          <Col span={6}>
            <Form.Item
              name="vin"
              rules={[{ required: true, message: 'VIN # is required.' }]}
              label="VIN #">
              <Input allowClear={true} placeholder="VIN #"></Input>
            </Form.Item>
          </Col> 
          <Col span={6}>
            <Form.Item
              name="capacity"
              rules={[{ required: true, message: 'Trailer Capacity is required.' }]}
              label="Capacity">
              <InputNumber placeholder="Capacity"></InputNumber>
            </Form.Item>
          </Col>        
          <Col span={6}>
            <Form.Item
              name="compartment"
              rules={[{ required: true, message: 'Compartment Count is required.' }]}
              label="Compartment">
              <InputNumber  placeholder="Compartment"></InputNumber>
            </Form.Item>
          </Col>            
        </Row>
        <Row>
          <Col span={20}></Col>
          <Col span={2}>
            <Form.Item>
              <Button style={{width:'100%'}} type="primary" htmlType="submit">
              {mode === 'ADD'? 'Save' : 'Update'} 
              </Button>
            </Form.Item>
          </Col>
          <Col span={2}>
            <Form.Item>
              <Button  style={{width:'100%'}} type="text" htmlType="button" onClick={onCancel}>
                 Cancel
              </Button>
            </Form.Item>
          </Col>
        </Row>
      </Form>
    </div>
  );
};

export default TrailerModel;
