import React, { useState } from 'react';
import { Button, Col, DatePicker, Form, Modal, Row, Select } from 'antd';
import { Messages } from 'components';
import { User } from 'types/user';
import { useHistory } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { AppDispatch } from 'redux/store';
import { DriverScheduleCreate } from 'types/driverSchedule';
import  driverScheduleService  from 'services/driverSchedule';
import  userService  from 'services/user';
import  truckService  from 'services/truck';
import  trailerService  from 'services/trailer';
import { handleErrors } from 'Utils/errorHandler';
import { Trailer, Truck } from 'types/vehicle';

interface Props {
  cancel: () => void;
}

interface PropsForm {
  onOk: () => void;
  onCancel: () => void;
}

const { Option } = Select;    


const CreateSchedule: React.FC<Props> = ({ cancel }: Props) => {

  return (
    <Modal
      visible={true}
      title="Create Schedule"
      onCancel={cancel}
      width="80%"
      footer={null}
      maskClosable={false}>
      <Messages />
      <CreateScheduleForm onOk={cancel} onCancel={cancel} />
    </Modal>
  );
};


const CreateScheduleForm : React.FC<PropsForm> = ({ onOk, onCancel }: PropsForm) => {
  const history = useHistory();
  const dispatch = useDispatch<AppDispatch>();
  const [matchingDrivers, setMatchingDrivers] = useState<User[]>([]);
  const [matchingTrucks, setMatchingTrucks] = useState<Truck[]>([]);
  const [matchingTrailer, setMatchingTrailer] = useState<Trailer[]>([]);
  const [form] = Form.useForm();

  const save = (values: DriverScheduleCreate) => {
    (async () => {
      try {
          await driverScheduleService.create(values);
        onOk();
      } catch (error) {
        handleErrors(error, history, dispatch, form);
      }
    })();
  };

  const driverSearch = (searchText: string) => {
    if(searchText ===undefined || searchText === '')
      return;
    (async () => {
      try {
        let drivers = await userService.findDriverByName(searchText);
        setMatchingDrivers(drivers);
      } catch (error) {
        handleErrors(error, history, dispatch);
      }
    })();
  };  

  const truckSearch = (searchText: string) => {
    if(searchText ===undefined || searchText === '')
      return;
    (async () => {
      try {
        let trucks = await truckService.findTruckByNumber(searchText);
        setMatchingTrucks(trucks);
      } catch (error) {
        handleErrors(error, history, dispatch);
      }
    })();
  };  

  const trailerSearch = (searchText: string) => {
    if(searchText ===undefined || searchText === '')
      return;
    (async () => {
      try {
        let trailers = await trailerService.findTrailerByNumber(searchText);
        setMatchingTrailer(trailers);
      } catch (error) {
        handleErrors(error, history, dispatch);
      }
    })();
  };  

  return (
    <div>
      <Form 
        form={form}
        layout='vertical' 
        onFinish={save}>
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
                <Select
                  showSearch={true}
                  showArrow={false}
                  filterOption={false}
                  defaultActiveFirstOption={false}
                  onSearch={driverSearch}
                  allowClear        
                  placeholder="Search Driver">
                  {matchingDrivers.map((v) => (
                    <Option key={v.id} value={v.id}>
                      {v.firstName} {v.lastName}
                      </Option>
                  ))}
                </Select>
            </Form.Item>
          </Col>
          <Col span={6}>
            <Form.Item
              name="truckId"
              rules={[{ required: true, message: 'Truck is required.' }]}
              label="Truck">
                <Select
                  showSearch={true}
                  showArrow={false}
                  filterOption={false}
                  defaultActiveFirstOption={false}
                  onSearch={truckSearch}
                  allowClear        
                  placeholder="Search Truck">
                  {matchingTrucks.map((v) => (
                    <Option key={v.id} value={v.id}>
                      {v.numberPlate}
                      </Option>
                  ))}
                </Select>
            </Form.Item>
          </Col>
          <Col span={6}>
            <Form.Item
              name="trailerId"
              rules={[{ required: true, message: 'Trailer is required.' }]}
              label="Trailer">
                <Select
                  showSearch={true}
                  showArrow={false}
                  filterOption={false}
                  defaultActiveFirstOption={false}
                  onSearch={trailerSearch}
                  allowClear        
                  placeholder="Search Trailer">
                  {matchingTrailer.map((v) => (
                    <Option key={v.id} value={v.id}>
                      {v.numberPlate}
                      </Option>
                  ))}
                </Select>                  
            </Form.Item>
          </Col>          
         </Row>
        <Row gutter={8}>
          <Col span={20}></Col>
          <Col span={2}>
            <Form.Item>
              <Button style={{width:'100%'}} type="primary" htmlType="submit">Save</Button>
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

export default CreateSchedule;

