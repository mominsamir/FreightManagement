import React, { useEffect, useState } from 'react';
import {  Button, Col, Descriptions, Row, Table} from 'antd';
import { Messages } from 'components';
import { Link, useHistory} from 'react-router-dom';
import {useParams} from 'react-router';
import { useDispatch } from 'react-redux';
import { AppDispatch } from 'redux/store';
import  orderService  from 'services/order';
import { handleErrors } from 'Utils/errorHandler';
import { Column } from 'types/dataTable';
import { Order } from 'types/order';
import ModelView from 'components/ModelView';
import { PlusOutlined, EditOutlined, TableOutlined } from '@ant-design/icons';
import { toDateTimeString } from 'Utils/dateUtils';
import { FuelProduct } from 'types/product';
import { Location } from 'types/location';


interface ViewOrderProps {
  id: string;  
}

const { Item } = Descriptions;    


const ViewOrder: React.FC<ViewOrderProps> = () => {

    const[location, setLocation] = useState<Order>();

    const props  = useParams<ViewOrderProps>();
    const history = useHistory();
    const dispatch = useDispatch<AppDispatch>();  
  

    useEffect(() => {
        (async () => {
            try {
                let id = parseInt(props.id);
                var ds = await orderService.find(id);
                setLocation(ds);
            } catch (error) {
                console.log('error occured..')
                handleErrors(error, history, dispatch);
            }
        })();
    }, [dispatch, history , props]);



    const columns: Column[]= [
        {
          title: 'Product',
          dataIndex: 'fuelProduct',
          render: (f:FuelProduct) =>f.name
        },
        {
            title: 'Location',
            dataIndex: 'location',
            render: (f:Location) =>f.name
        },
        {
            title: 'Quantity',
            dataIndex: 'quantity',
        },
        {
            title: 'LoadCode',
            dataIndex: 'loadCode',
        }
      ];      

      const buttons = [
        <Link to={`/dispatch/orders/add`}>
        <Button icon={<PlusOutlined />}>Add new</Button>
        </Link>,
        <Link to={`/dispatch/orders/edit/${props.id}`}>
            <Button icon={<EditOutlined />}>Edit</Button>
        </Link>,
        <Link to={`/dispatch/orders`}>
            <Button icon={<TableOutlined />}>All</Button>
        </Link>
      ]  

    return (
        <ModelView title="View Order" extra={buttons}>      
                <Row gutter={8}>
                    <Col span={24}>
                        <Messages />
                    </Col>
                    <Col span={24}>
                        <Descriptions bordered>
                            <Item label="Customer">{location?.customer.name}</Item>
                            <Item label="Email">{location?.customer.email}</Item>
                            <Item label="Delivery Address">{location?.customer.street},  
                                {location?.customer.city},  
                                {location?.customer.state}, 
                                {location?.customer.zipCode}</Item>
                            <Item label="Order Date">{toDateTimeString(location?.orderDate)}</Item>
                            <Item label="Shipment Date">{toDateTimeString(location?.shipDate)}</Item>
                            <Item label="Total Qnt">{location?.totalQnt}</Item>                    
                            <Item label="Status">{location?.statusLabel}</Item>                    
                        </Descriptions>
                    </Col>
                    <Col span={24}>
                        <Table 
                          title={()=> (<h4>Order Items</h4>)}
                          columns={columns} 
                          rowKey="id"
                          pagination={false}
                          dataSource={location?.orderItems}>
                        </Table>              
                    </Col>            
                </Row>
      </ModelView>        
    );
};




export default ViewOrder;

