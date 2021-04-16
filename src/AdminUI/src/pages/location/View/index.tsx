import React, { useEffect, useState } from 'react';
import {  Button, Col, Descriptions, Row, Table} from 'antd';
import { Messages } from 'components';
import { Link, useHistory} from 'react-router-dom';
import {useParams} from 'react-router';
import { useDispatch } from 'react-redux';
import { AppDispatch } from 'redux/store';
import  locationService  from 'services/location';
import { handleErrors } from 'Utils/errorHandler';
import { Column } from 'types/dataTable';
import { Location } from 'types/location';
import ModelView from 'components/ModelView';
import { PlusOutlined, EditOutlined, TableOutlined } from '@ant-design/icons';

interface ViewLocationProps {
  id: string;  
}

const { Item } = Descriptions;    


const ViewCustomer: React.FC<ViewLocationProps> = () => {

    const[location, setLocation] = useState<Location>();
    const props  = useParams<ViewLocationProps>();
    const history = useHistory();
    const dispatch = useDispatch<AppDispatch>();  
  

    useEffect(() => {
        (async () => {
          try {
                let id = parseInt(props.id);
                var ds = await locationService.find(id);
                setLocation(ds);
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
            title: 'Fuel Grade',
            dataIndex: 'fuelGradeLabel',
            key: 'fuelGradeLabel'
        },
        {
            title: 'Capactity',
            dataIndex: 'capactity',
            key: 'capactity'
        }
      ];      

      const buttons = [
        <Link to={`/dispatch/locations/add`}>
        <Button icon={<PlusOutlined />}>Add new</Button>
        </Link>,
        <Link to={`/dispatch/locations/edit/${props.id}`}>
            <Button icon={<EditOutlined />}>Edit</Button>
        </Link>,
        <Link to={`/dispatch/locations`}>
            <Button icon={<TableOutlined />}>All</Button>
        </Link>
      ]

    return (
      <ModelView title="Location" extra={buttons}>
        <Row gutter={8}>
            <Col span={24}>
                <Messages />
            </Col>
            <Col span={24}>
                <Descriptions bordered>
                    <Item label="Name">{location?.name}</Item>
                    <Item label="Email">{location?.email}</Item>
                    <Item label="Street">{location?.street}</Item>
                    <Item label="City">{location?.city}</Item>
                    <Item label="State">{location?.state}</Item>                    
                    <Item label="ZipCode">{location?.zipCode}</Item>
                    <Item label="Country">{location?.country}</Item>
                    <Item label="Status">{location?.isActive}</Item>
                </Descriptions>
            </Col>
            <Col span={24}>
                <Table 
                  title={()=> (<h4>Location Tanks</h4>)}
                  columns={columns} 
                  rowKey="id"
                  pagination={false}
                  dataSource={location?.tanks}>
                </Table>              
            </Col>            
        </Row>
      </ModelView>
    );
};




export default ViewCustomer;

