import React, { useEffect, useState } from 'react';
import { Button, Col, Descriptions, Row, Table} from 'antd';
import { Messages } from 'components';
import { Link, useHistory} from 'react-router-dom';
import {useParams} from 'react-router';
import { useDispatch } from 'react-redux';
import { AppDispatch } from 'redux/store';
import  dispatchOrderService  from 'services/dispatchOrder';
import { handleErrors } from 'Utils/errorHandler';
import { toDateTimeString } from 'Utils/dateUtils';
import { Column } from 'types/dataTable';
import ModelView from 'components/ModelView';
import { PlusOutlined, TableOutlined } from '@ant-design/icons';
import { Dispatch } from 'types/dispatchOrder';
import { Rack } from 'types/rack';
import { OrderItem } from 'types/order';

interface ViewDispatchProps {
  id: string;  
}

const { Item } = Descriptions;    


const ViewDispatch: React.FC<ViewDispatchProps> = () => {

    const[orderDispatch, setOrderDispatch] = useState<Dispatch>();
    const props  = useParams<ViewDispatchProps>();
    const history = useHistory();
    const dispatch = useDispatch<AppDispatch>();  
  

    useEffect(() => {
        (async () => {
          try {
                let id = parseInt(props.id);
                var ds = await dispatchOrderService.find(id);
                setOrderDispatch(ds);
          } catch (error) {
            handleErrors(error, history, dispatch);
          }
        })();
      }, [dispatch, history,props]);

    const columns: Column[]= [
        {
          title: 'Rack',
          dataIndex: 'rack',
          render: (rack:Rack) => rack.name
        },
        {
          title: 'Location',
          dataIndex: 'orderItem',
          render: (orderItem: OrderItem) => orderItem.location.name
        },
        {
          title: 'Fuel product',
          dataIndex: 'orderItem',
          render: (orderItem: OrderItem) => orderItem.fuelProduct.name
        },
        {
          title: 'Bill Of Loading',
          dataIndex: 'billOfLoading'
        },
        {
          title: 'Loaded Qnt',
          dataIndex: 'loadedQuantity'
        }
      ];      

      const buttons = [
        <Button icon={<PlusOutlined />} >Add new</Button>,
        <Link to={`/dispatch/dispatches`}>
            <Button icon={<TableOutlined />}>All</Button>
        </Link>
      ]      


    return (
      <ModelView title="Dispatch Schedule" extra={buttons}>
        <Row gutter={8}>
            <Col span={24}>
                <Messages />
            </Col>
            <Col span={24}>
                <Descriptions bordered>
                    <Item label="Driver">{orderDispatch?.driverSchedule.driver.firstName} {orderDispatch?.driverSchedule.driver.lastName}</Item>
                    <Item label="Truck">{orderDispatch?.driverSchedule.truck.numberPlate}</Item>
                    <Item label="Trailer">{orderDispatch?.driverSchedule.trailer.numberPlate}</Item>
                    <Item label="Status">{orderDispatch?.statusLabel}</Item>
                    <Item label="Dispatch Date">{toDateTimeString(orderDispatch?.dispatchDateTime)}</Item>
                    <Item label="Dispatch Start Time">{toDateTimeString(orderDispatch?.dispatchStartTime)}</Item>
                    <Item label="Dispatch End Time">{toDateTimeString(orderDispatch?.dispatchEndTime)}</Item>
                    <Item label="Loading Start Time">{toDateTimeString(orderDispatch?.loadingStartTime)}</Item>
                    <Item label="Loading End Time">{toDateTimeString(orderDispatch?.loadingEndTime)}</Item>
                    <Item label="Rack arrived On">{toDateTimeString(orderDispatch?.rackArrivalTime)}</Item>
                    <Item label="Rack Left On">{toDateTimeString(orderDispatch?.rackLeftOnTime)}</Item>
                </Descriptions>
            </Col>
            <Col span={24}>
                <Table 
                  title={()=> (<h4>Dispatached Items</h4> )}
                  columns={columns} 
                  rowKey={row=> row.id}
                  pagination={false}
                  dataSource={orderDispatch?.dispatchLoadings}
                  footer={() => (
                    <div  style={{float: 'right'}}>
                    </div>
                  )}>
                </Table>              
            </Col>  
        </Row>
      </ModelView>        
    );
};




export default ViewDispatch;

