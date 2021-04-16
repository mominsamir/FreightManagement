import React, { useEffect, useState } from 'react';
import {  Button, Col, Descriptions, Row, Table} from 'antd';
import { Messages } from 'components';
import { Link, useHistory} from 'react-router-dom';
import {useParams} from 'react-router';
import { useDispatch } from 'react-redux';
import { AppDispatch } from 'redux/store';
import  customerService  from 'services/customer';
import { handleErrors } from 'Utils/errorHandler';
import { Column } from 'types/dataTable';
import { Customer } from 'types/customer';
import ModelView from 'components/ModelView';
import { PlusOutlined, EditOutlined, TableOutlined } from '@ant-design/icons';


interface ViewCustomerProps {
  id: string;  
}

const { Item } = Descriptions;    


const ViewCustomer: React.FC<ViewCustomerProps> = () => {

    const[customer, setCustomer] = useState<Customer>();
    const props  = useParams<ViewCustomerProps>();
    const history = useHistory();
    const dispatch = useDispatch<AppDispatch>();  
  

    useEffect(() => {
        (async () => {
          try {
                let id = parseInt(props.id);
                var ds = await customerService.find(id);
                setCustomer(ds);
          } catch (error) {
            handleErrors(error, history, dispatch);
          }
        })();
      }, [dispatch, history,props]);



    const columns: Column[]= [
        {
          title: 'Name',
          dataIndex: 'name',
          key: 'name'
        },
        {
            title: 'Email',
            dataIndex: 'email',
            key: 'email'
        },
        {
            title: 'Street',
            dataIndex: 'street',
            key: 'street'
        },
        {
            title: 'City',
            dataIndex: 'city',
            key: 'city'
        },
        {
            title: 'ZipCode',
            dataIndex: 'zipCode',
            key: 'zipCode'
        },
        {
          title: 'Active',
          key: 'isActive',
          dataIndex: 'isActive'
        }
      ];      

      const buttons = [
        <Link to={`/dispatch/customers/add`}>
        <Button icon={<PlusOutlined />}>Add new</Button>
        </Link>,
        <Link to={`/dispatch/customers/edit/${props.id}`}>
            <Button icon={<EditOutlined />}>Edit</Button>
        </Link>,
        <Link to={`/dispatch/customers`}>
            <Button icon={<TableOutlined />}>All</Button>
        </Link>
      ]  

    return (
      <ModelView title="View Customer" extra={buttons}>      
        <Row gutter={8}>
            <Col span={24}>
                <Messages />
            </Col>
            <Col span={24}>
                <Descriptions bordered>
                    <Item label="Name">{customer?.name}</Item>
                    <Item label="Email">{customer?.email}</Item>
                    <Item label="Street">{customer?.street}</Item>
                    <Item label="City">{customer?.city}</Item>
                    <Item label="State">{customer?.state}</Item>                    
                    <Item label="ZipCode">{customer?.zipCode}</Item>
                    <Item label="Country">{customer?.country}</Item>
                    <Item label="Status">{customer?.isActive}</Item>
                </Descriptions>
            </Col>
            <Col span={24}>
                <Table 
                  title={()=> (<h4>Customer Locations</h4>)}
                  columns={columns} 
                  rowKey="id"
                  pagination={false}
                  dataSource={customer?.locations}>
                </Table>              
            </Col>            
        </Row>
      </ModelView>        
    );
};




export default ViewCustomer;

