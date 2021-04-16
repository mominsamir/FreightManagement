import React, { useEffect, useState } from 'react';
import { Form,Button, Col, Input, Row, Select, Radio } from 'antd';
import { Messages } from 'components';
import { Link, useHistory } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { handleErrors } from 'Utils/errorHandler';
import { AppDispatch } from 'redux/store';
import locationServices from 'services/location';
import { Location, LocationTank } from 'types/location';
import { MinusCircleOutlined, TableOutlined } from '@ant-design/icons';
import ModelView from 'components/ModelView';


const Option = Select.Option; 


const CreateLocation: React.FC = () => {
    var location : Location = {
        id:  0,   
        name : '',
        email : '',
        isActive : false,
        street : '',
        city: '',
        state: '',
        country: '',
        zipCode: '',    
        tanks : []
    }

    const buttons = [
        <Link to={`/dispatch/locations`}>
            <Button icon={<TableOutlined />}>All</Button>
        </Link>
      ]

    return (
        <ModelView title="Create Location" extra={buttons}>        
            <Row gutter={8} >
                <Col span={24}>
                    <Messages />
                    <LocationModel location={location} mode="ADD"/>
                </Col>
            </Row>
        </ModelView>
    )
}


export interface CreateProps {
    location: Location;
    mode: 'EDIT' | 'ADD'
}

  
export const LocationModel: React.FC<CreateProps> = ({ location, mode }: CreateProps) => {
    const history = useHistory();
    const [model, setModel] = useState<Location>(location);
    const dispatch = useDispatch<AppDispatch>();
    const [form] = Form.useForm();

    const save = (values: Location) => {
      (async () => {
        try {
            if(mode==='EDIT'){
                await locationServices.update(model.id, values);
                history.push(`/dispatch/locations/${model.id}`);
            }else
                await locationServices.create(values);
        } catch (error) {
          handleErrors(error, history, dispatch, form);
        }
      })();
    };

    const removeTank = (locationId: number, tankUid: number):Promise<boolean> => {
       return  (async ():Promise<boolean> => {
            try {
                return await locationServices.removeTank(locationId, tankUid);
            } catch (error) {
              handleErrors(error, history, dispatch, form);
              return Promise.reject(false);
            }
          })();     
    }

    

    useEffect(() => {
        form.setFieldsValue(model)
    }, [form, model])

    const removeViaAPI= (tank: LocationTank):Promise<boolean> => {
        if(tank === undefined)
            return Promise.resolve(true);
        else {
            return removeTank(model.id,tank.id);
        }
    }

    
    return (
        <div>
            <Form 
                form={form}
                layout='vertical'
                onFinish={save} 
                initialValues={model}>
                <Row gutter={8} >
                    <Col span={8}>
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
                    <Col span={8}>
                        <Form.Item
                            name="email"
                            rules={[{ required: true, message: 'Email is required.' }]}
                            label="Email">
                            <Input allowClear={true} placeholder="Email"></Input>
                        </Form.Item>
                    </Col>
                    {mode==='EDIT' && (
                    <Col span={8}>
                        <Form.Item
                            name="isActive"
                            label="Status"
                            rules={[{ required: true, message: 'Please pick status!' }]}>
                            <Radio.Group>
                                <Radio.Button value={true}>Active</Radio.Button>
                                <Radio.Button value={false}>In Active</Radio.Button>
                            </Radio.Group>
                        </Form.Item>   
                    </Col>                    
                    )}
                </Row>
                <Row gutter={8} >
                    <Col span={8}>
                        <Form.Item
                            name="street"
                            rules={[{ required: true, message: 'Street is required.' }]}
                            label="Street">
                            <Input allowClear={true} placeholder="Street"></Input>
                        </Form.Item>
                    </Col>
                    <Col span={8}>
                        <Form.Item
                            name="city"
                            rules={[{ required: true, message: 'City is required.' }]}
                            label="City">
                            <Input allowClear={true} placeholder="City"></Input>
                        </Form.Item>
                    </Col>
                    <Col span={8}>
                        <Form.Item
                            name="state"
                            rules={[{ required: true, message: 'state is required.' }]}
                            label="State">
                            <Input allowClear={true} placeholder="state"></Input>
                        </Form.Item>
                    </Col>                        
                </Row>
                <Row gutter={8} >
                    <Col span={8}>
                        <Form.Item
                            name="zipCode"
                            rules={[{ required: true, message: 'ZipCode is required.' }]}
                            label="ZipCode">
                            <Input allowClear={true} placeholder="ZipCode"></Input>
                        </Form.Item>
                    </Col>
                    <Col span={8}>
                        <Form.Item
                            name="country"
                            rules={[{ required: true, message: 'Country is required.' }]}
                            label="Country">
                            <Input allowClear={true} placeholder="Country"></Input>
                        </Form.Item>
                    </Col>
                </Row>     
                <Form.List name="tanks">
                    {(fields, { add, remove }) => (
                        <>
                            <Row gutter={24}>
                                <Col span={22} style={{padding :20}}><h3>Location Tanks</h3></Col>                            
                                <Col span={2} style={{padding :20}}>
                                    <Button type="primary" onClick={()=> add()}>Add Tank</Button>
                                </Col>
                            </Row>
                            <Row gutter={6}>
                                <Col span={8}><h4>Tank Name</h4></Col>
                                <Col span={8}><h4>Fuel Grade</h4></Col>
                                <Col span={7}><h4>Capacity</h4></Col>
                                <Col span={1}><h4>Action</h4></Col>
                            </Row>                                                                                                   
                            {fields.map((field) => (
                            <Row gutter={6} key={field.key}>
                                <Col span={8}>
                                    <Form.Item
                                        hidden={true}
                                        fieldKey={[field.fieldKey, "id"]}
                                        name={[field.name, "id"]}>
                                        <Input  placeholder="id"></Input>
                                    </Form.Item>                                                       
                                    <Form.Item
                                        name={[field.name, "name"]}
                                        rules={[{ required: true, message: 'Tank Name is required.' }]}
                                        fieldKey={[field.fieldKey, "name"]}>
                                        <Input type="text" placeholder="Tank name" />
                                    </Form.Item>
                                </Col>                        
                                <Col span={8}>                           
                                    <Form.Item
                                        name={[field.name, "fuelGrade"]}
                                        fieldKey={[field.fieldKey, "fuelGrade"]}
                                        rules={[{ required: true, message: 'grade is required.' }]}>
                                        <Select style={{width:'100%'}} >
                                            <Option value={0}>Regular 87</Option>
                                            <Option value={1}>Plus 89</Option>
                                            <Option value={2}>Super 93</Option>
                                            <Option value={3}>Diesel Clear</Option>
                                            <Option value={4}>Diesel Dyed</Option>                                        
                                        </Select> 
                                    </Form.Item>
                                </Col>                        
                                <Col span={7}>                           
                                    <Form.Item
                                        name={[field.name, "capactity"]}
                                        rules={[{ required: true, message: 'Capacity is required.' }]}
                                        fieldKey={[field.fieldKey, "capactity"]}>
                                        <Input style={{width:'100%',textAlign: 'right'}} type="number" placeholder="Capactity" />
                                    </Form.Item>
                                </Col>
                                <Col span={1}>
                                    <Button type="primary" onClick={()=>{
                                        if(removeViaAPI(model.tanks[field.fieldKey]))
                                            remove(field.name);
                                    }} icon={<MinusCircleOutlined />}>
                                    </Button>
                                </Col>                        
                            </Row>       
                            )                 
                            )}
                        </>
                    )}
                </Form.List> 
                <Row gutter={8}>
                    <Col span={20}></Col>
                    <Col span={2}>
                        <Button style={{width:'100%'}} type="primary" htmlType="submit">Save</Button>
                    </Col>
                    <Col span={2}>                    
                        <Button style={{width:'100%'}} type="dashed" htmlType="button">Cancel</Button>
                    </Col>
                </Row>
            </Form>
        </div>
    );
  };
  


export default CreateLocation;