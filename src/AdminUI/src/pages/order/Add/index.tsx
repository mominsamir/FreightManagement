import React, { useEffect, useState } from 'react';
import { Form,Button, Col, Input, Row, Select,  DatePicker } from 'antd';
import { Messages } from 'components';
import { Link, useHistory } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { handleErrors } from 'Utils/errorHandler';
import { AppDispatch } from 'redux/store';
import orderServices from 'services/order';
import customerService from 'services/customer';
import productService from 'services/product';
import { MinusCircleOutlined, TableOutlined } from '@ant-design/icons';
import ModelView from 'components/ModelView';
import { Order } from 'types/order';
import moment from 'moment';
import { Customer } from 'types/customer';
import { FilterData } from 'types/dataTable';
import { Location } from 'types/location';
import { FuelProduct } from 'types/product';


const Option = Select.Option; 


const CreateOrder: React.FC = () => {
    var order : Order = {
        id:  0,   
        orderDate : moment(),
        shipDate : moment(),
        status : 0,
        statusLabel: '',
        totalQnt: 0,
        customer: {
            id: 0,
            name: '',
            city: '',
            country: '',
            isActive: false,
            email: '',
            state: '',
            zipCode:'',
            street: '',
            locations : [],
        },
        orderItems: []
    }

    const buttons = [
        <Link to={`/dispatch/orders`}>
            <Button icon={<TableOutlined />}>All</Button>
        </Link>
      ]

    return (
        <ModelView title="Create Order" extra={buttons}>        
            <Row gutter={8} >
                <Col span={24}>
                    <Messages />
                    <OrderModel order={order} mode="ADD"/>
                </Col>
            </Row>
        </ModelView>
    )
}


export interface CreateProps {
    order: Order;
    mode: 'EDIT' | 'ADD'
}

  
export const OrderModel: React.FC<CreateProps> = ({ order, mode }: CreateProps) => {
    const history = useHistory();
    const [model, setModel] = useState<Order>(order);
    const [currentCustomer, setCurrentCustomer] = useState<number>(0);
    const [matchingCustomer, setMatchingCustomer] = useState<Customer[]>([]);
    const [matchingLocation, setMatchingLocation] = useState<Location[]>([]);
    const [allProduct, setAllProduct] = useState<FuelProduct[]>([]);
    const dispatch = useDispatch<AppDispatch>();
    const [form] = Form.useForm();

    const save = (values: Order) => {
      (async () => {
        try {
            var result: any = await orderServices.create(values);
            console.log(result)
            history.push(`/dispatch/orders/${result.id}`);
        } catch (error) {
          handleErrors(error, history, dispatch, form);
        }
      })();
    };
    

    useEffect(() => {
        form.setFieldsValue(model)
    }, [form, model])


    const customerSearch = (searchText: string) => {
        if(searchText ===undefined || searchText === '')
          return;
        (async () => {
          try {
            var filterParam: FilterData = {
                name: "name",
                value: searchText,
                operator: 'CONTAIN'
            };
            var searchParam = {
                page: 1,
                pageSize: 10,
                sortData :[],
                filterData: [filterParam]
            } 

            let drivers = await customerService.search(searchParam);
            setMatchingCustomer(drivers.data);
          } catch (error) {
            handleErrors(error, history, dispatch);
          }
        })();
    };

    const onCustomerSelected = (customerId: number) => {
        setCurrentCustomer(customerId);
    }

    useEffect(() => {
        (async () => {
            try {
                if(currentCustomer !== 0){
                    var customer = await customerService.find(currentCustomer);
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
                            rules={[{ required: true, message: 'Customer is required.' }]}
                            label="Customer">
                            <Select
                                showSearch={true}
                                showArrow={false}
                                filterOption={false}
                                defaultActiveFirstOption={false}
                                onSearch={customerSearch}
                                onSelect={onCustomerSelected}
                                allowClear        
                                placeholder="Search Driver">
                                {matchingCustomer.map((v) => (
                                    <Option key={v.id} value={v.id}>
                                    {v.name} 
                                    </Option>
                                ))}
                            </Select>
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
                <Form.List name="OrderLines">
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
                                        name={[field.name, "locationId"]}
                                        fieldKey={[field.fieldKey, "locationId"]}                                        
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
                                        name={[field.name, "fuelProductId"]}
                                        fieldKey={[field.fieldKey, "fuelProductId"]}
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
  


export default CreateOrder;