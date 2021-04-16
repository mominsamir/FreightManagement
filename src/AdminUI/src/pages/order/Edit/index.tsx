import React, { useEffect, useState } from 'react';
import {  Button, Col,  DatePicker,  Form,  Input,  Row, Select } from 'antd';
import { Messages } from 'components';
import { Link, useHistory, useParams } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { handleErrors } from 'Utils/errorHandler';
import { AppDispatch } from 'redux/store';
import orderService from 'services/order';
import productService from 'services/product';
import customerService from 'services/customer';
import ModelView from 'components/ModelView';
import { SearchOutlined, PlusOutlined, TableOutlined,MinusCircleOutlined  } from '@ant-design/icons';
import { jsonToOrderUpdate, Order } from 'types/order';
import { FuelProduct } from 'types/product';
import { Location } from 'types/location';

interface EditOrderProps {
    id: string;  
}

const UpdateOrder: React.FC = () => {
    const[order, setOrder] = useState<Order>();
    const props  = useParams<EditOrderProps>();
    const history = useHistory();
    const dispatch = useDispatch<AppDispatch>();  

    useEffect(() => {
        (async () => {
            try {
                let id = parseInt(props.id);
                var ds = await orderService.find(id);
                setOrder(ds);
            } catch (error) {
                handleErrors(error, history, dispatch);
            }
        })();
    }, [dispatch, history,props]);

    const buttons = [
        <Link to={`/dispatch/orders/add`}>
        <Button icon={<PlusOutlined />}>Add new</Button>
        </Link>,
        <Link to={`/dispatch/orders/${props.id}`}>
            <Button icon={<SearchOutlined />}>View</Button>
        </Link>,
        <Link to={`/dispatch/orders`}>
            <Button icon={<TableOutlined />}>All</Button>
        </Link>
      ]
    
    return (
        <ModelView title="Update Order" extra={buttons}>
            <Row gutter={8} >
                <Col span={24}>
                    <Messages />
                    {order && 
                        <UpdateOrderModel order={order}  />
                    }
                </Col>
            </Row>
        </ModelView>
    );
}

export interface UpdateProps {
    order: Order;
}

const Option = Select.Option;

  
export const UpdateOrderModel: React.FC<UpdateProps> = ({ order }: UpdateProps) => {
    const history = useHistory();
    const [model, setModel] = useState<Order>(order);
    const [currentCustomer, setCurrentCustomer] = useState<number>(order.customer.id);
    const [matchingLocation, setMatchingLocation] = useState<Location[]>([]);
    const [allProduct, setAllProduct] = useState<FuelProduct[]>([]);
    const dispatch = useDispatch<AppDispatch>();
    const [form] = Form.useForm();

    const save = (values: Order) => {
      (async () => {
        try {
            let order = jsonToOrderUpdate(values);
            await orderService.update(values?.id, order);
            history.push(`/dispatch/orders/${values?.id}`);
        } catch (error) {
          handleErrors(error, history, dispatch, form);
        }
      })();
    };


    useEffect(() => {
        (async () => {
            try {
                if(currentCustomer !== 0){
                    var customer = await customerService.find(currentCustomer!);
                    setMatchingLocation(customer.locations);
                }
            } catch (error) {
                handleErrors(error, history, dispatch);
            }
        })();
      }, [dispatch, history,currentCustomer]); 
      
      useEffect(() => {
        (async () => {
            try {
                var searchParam = {
                    page: 1,
                    pageSize: 10,
                    sortData :[],
                    filterData: []
                } 
                var products = await productService.search(searchParam);
                setAllProduct(products.data);
            } catch (error) {
                handleErrors(error, history, dispatch);
            }
        })();
      }, [dispatch, history]);       
    
   
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
                            name="customerId"
                            label="Customer">
                            <h4>{model?.customer.name}</h4>        
                        </Form.Item>
                    </Col>
                    <Col span={8}>
                        <Form.Item
                            name="orderDate"
                            rules={[{ required: true, message: 'Order Date is required.' }]}
                            label="Order Date">
                            <DatePicker style={{width:'100%'}} placeholder="Order Date"></DatePicker> 
                        </Form.Item>
                    </Col>
                    <Col span={8}>
                        <Form.Item
                            name="shipDate"
                            rules={[{ required: true, message: 'Shipment Date is required.' }]}
                            label="Shipment Date">
                            <DatePicker style={{width:'100%'}} placeholder="Shipment Date"></DatePicker> 
                        </Form.Item>
                    </Col>                    
                </Row>
                <Form.List name="orderItems">
                    {(fields, { add, remove }) => (
                        <>
                            <Row gutter={24}>
                                <Col span={22} style={{padding :20}}><h3>Order Items</h3></Col>                            
                                <Col span={2} style={{padding :20}}>
                                    <Button type="primary" onClick={()=> add()}>Add Line</Button>
                                </Col>
                            </Row>
                            <Row gutter={6}>
                                <Col span={6}><h4>Location</h4></Col>
                                <Col span={6}><h4>Fuel Product</h4></Col>
                                <Col span={6}><h4>Qnt</h4></Col>
                                <Col span={5}><h4>LodeCode</h4></Col>
                                <Col span={1}><h4>Action</h4></Col>                                
                            </Row>                                                                                                   
                            {fields.map((field) => (
                            <Row gutter={6} key={field.key}>
                                <Col span={6}>
                                    <Form.Item
                                        hidden={true}
                                        fieldKey={[field.fieldKey, "id"]}
                                        name={[field.name, "id"]}>
                                        <Input  placeholder="id"></Input>
                                    </Form.Item>                                                       
                                    <Form.Item
                                        name={[field.name, "location","id"]}
                                        fieldKey={[field.fieldKey, "location","id"]}                                        
                                        rules={[{ required: true, message: 'Location is required.' }]}>
                                        <Select
                                            showArrow={true}
                                            allowClear        
                                            placeholder="Search Location">
                                            {matchingLocation.map((v) => (
                                                <Option key={v.id} value={v.id}>
                                                {v.name} 
                                                </Option>
                                            ))}
                                        </Select>
                                    </Form.Item>
                                </Col>                        
                                <Col span={6}>                           
                                    <Form.Item
                                        name={[field.name, "fuelProduct","id"]}
                                        fieldKey={[field.fieldKey, "fuelProduct","id"]}
                                        rules={[{ required: true, message: 'Product is required.' }]}>
                                        <Select style={{width:'100%'}} >
                                            {allProduct.map((v) => (
                                                <Option key={v.id} value={v.id}>
                                                    {v.name} 
                                                </Option>
                                            ))}                                        
                                        </Select> 
                                    </Form.Item>
                                </Col>                        
                                <Col span={6}>                           
                                    <Form.Item
                                        name={[field.name, "quantity"]}
                                        rules={[{ required: true, message: 'Quantity is required.' }]}
                                        fieldKey={[field.fieldKey, "quantity"]}>
                                        <Input style={{width:'100%',textAlign: 'right'}} type="number" placeholder="Quantity" />
                                    </Form.Item>
                                </Col>
                                <Col span={5}>
                                    <Form.Item
                                        name={[field.name, "loadCode"]}
                                        fieldKey={[field.fieldKey, "loadCode"]}>
                                        <Input allowClear={true} placeholder="Load Code"></Input>
                                    </Form.Item>                                
                                </Col>
                                <Col span={1}>
                                    <Button type="primary" onClick={()=>remove(field.name)} icon={<MinusCircleOutlined />}>
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
  

export default UpdateOrder;