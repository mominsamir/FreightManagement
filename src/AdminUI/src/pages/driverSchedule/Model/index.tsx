import React from 'react';
import { Form, Col, Row, Button, Input, Select, DatePicker } from 'antd';
import { handleErrors } from 'Utils/errorHandler';
import { useHistory } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { AppDispatch } from 'redux/store';
import { User } from 'types/user';
import userService from 'services/user';
import { DriverScheduleCreate } from 'types/driverSchedule';

interface Props {
  schedule?: DriverScheduleCreate;
  mode: 'EDIT' | 'ADD';
  onOk: () => void;
  onCancel: () => void;
}

const ScheduleAddModel: React.FC<Props> = ({ schedule, mode, onOk, onCancel }: Props) => {
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
        initialValues={schedule}>
        <Row gutter={8} >
          <Col span={6}>
            <Form.Item
              name="startTime"
              rules={[{ required: true, message: 'Start DateTime is required.' }]}
              label="Start Date">
                 <DatePicker placeholder="Start DateTime"></DatePicker> 
            </Form.Item>
          </Col>
          <Col span={6}>
            <Form.Item
              name="driverId"
              rules={[{ required: true, message: 'Driver is required.' }]}
              label="Driver">
                Driver  
            </Form.Item>
          </Col>
          <Col span={6}>
            <Form.Item
              name="truckId"
              rules={[{ required: true, message: 'Truck is required.' }]}
              label="Truck">
                Driver  
            </Form.Item>
          </Col>
          <Col span={6}>
            <Form.Item
              name="trailerId"
              rules={[{ required: true, message: 'Trailer is required.' }]}
              label="Trailer">
                trailerId  
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
              <Button style={{width:'100%'}} type="dashed" htmlType="button" onClick={onCancel}>
                 Cancel
              </Button>
            </Form.Item>
          </Col>
        </Row>
      </Form>
    </div>
  );
};

export default  ScheduleAddModel;

