import React, { useEffect, useState } from 'react';
import { Form,Button, Col, Input,  Row,  Radio } from 'antd';
import { Messages } from 'components';
import { Link, useHistory, useParams } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { handleErrors } from 'Utils/errorHandler';
import { AppDispatch } from 'redux/store';
import customerServices from 'services/customer';
import { Customer } from 'types/customer';
import { PlusOutlined, SearchOutlined, TableOutlined } from '@ant-design/icons';
import ModelView from 'components/ModelView';


interface EditCustomerProps {
    id: string;  
}

const EditCustomer: React.FC = () => {
    const[model, setModel] = useState<Customer>();
    const props  = useParams<EditCustomerProps>();
    const history = useHistory();
    const dispatch = useDispatch<AppDispatch>();  

    useEffect(() => {
        (async () => {
            try {
                let id = parseInt(props.id);
                var ds = await customerServices.find(id);
                setModel(ds);
            } catch (error) {
                handleErrors(error, history, dispatch);
            }
        })();
    }, [dispatch, history,props]);

    const buttons = [
      <Link to={`/dispatch/customers/add`}>
      <Button icon={<PlusOutlined />}>Add new</Button>
      </Link>,
      <Link to={`/dispatch/customers/${props.id}`}>
          <Button icon={<SearchOutlined />}>View</Button>
      </Link>,
      <Link to={`/dispatch/customers`}>
          <Button icon={<TableOutlined />}>All</Button>
      </Link>
    ]      
    
    return (
      <ModelView title="Edit Customer" extra={buttons}>          
        <Row gutter={8} >
            <Col span={24}><h4>Update Location</h4></Col>        
            <Col span={24}>
                <Messages />
                {model && 
                <CustomerCreateModel customer={model} />
                }
            </Col>
        </Row>
      </ModelView>  
    );    
}


interface CreateProps {
    customer?: Customer;
  }
  
  const CustomerCreateModel: React.FC<CreateProps> = ({ customer }: CreateProps) => {
    const history = useHistory();
    const dispatch = useDispatch<AppDispatch>();
    const [form] = Form.useForm();
  
    const save = (values: Customer) => {
      (async () => {
        try {
            await customerServices.update(values.id,values);
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
            <Col span={8}>
                <Form.Item
                    name="isActive"
                    label="Status"
                    rules={[{ required: true, message: 'Please pick an item!' }]}>
                    <Radio.Group>
                    <Radio.Button value={true}>Active</Radio.Button>
                    <Radio.Button value={false}>In Active</Radio.Button>
                    </Radio.Group>
                </Form.Item>   
            </Col>      
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
        <Row gutter={8}>
          <Col span={20}></Col>
          <Col span={2}>
            <Form.Item>
              <Button style={{width:'100%'}} type="primary" htmlType="submit">Save</Button>
            </Form.Item>
          </Col>
          <Col span={2}>
            <Form.Item>
              <Button style={{width:'100%'}} type="dashed" htmlType="button" >
                 Cancel
              </Button>
            </Form.Item>
          </Col>
        </Row>
        </Form>
      </div>
    );
  };
  


export default EditCustomer;