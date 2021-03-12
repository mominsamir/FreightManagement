import React from 'react';
import { Form, Col, Row, Button, Input,  DatePicker } from 'antd';
import { handleErrors } from 'Utils/errorHandler';
import { useHistory } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { AppDispatch } from 'redux/store';
import truckService from 'services/truck';
import { Truck } from 'types/vehicle';

interface Props {
  truck?: Truck;
  mode: 'EDIT' | 'ADD';
  onOk: () => void;
  onCancel: () => void;
}

const TruckModel: React.FC<Props> = ({ truck, mode, onOk, onCancel }: Props) => {
  const history = useHistory();
  const dispatch = useDispatch<AppDispatch>();
  const [form] = Form.useForm();

  const save = (values: Truck) => {
    (async () => {
      try {
        if(mode ==='ADD'){
          await truckService.create(values);
        }else{
          await truckService.update(values.id ,values);
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
        initialValues={truck}>
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
              rules={[{ required: true, message: 'VIN is required.' }]}
              label="VIN">
              <Input allowClear={true} placeholder="VIN"></Input>
            </Form.Item>
          </Col>          
          {mode==='EDIT' && ( 
          <Col span={6}>
            <Form.Item
              name="nextMaintanceDate"
              rules={[{ required: true, message: 'Next Maintance Date is required.' }]}
              label="Next Maintance Date">
                <DatePicker  placeholder="Next Maintance Date"/>
            </Form.Item>
          </Col>
          )}
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

export default TruckModel;
