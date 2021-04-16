import React from 'react';
import { Datatable } from 'components';
import { Row, Col, Button, Space, Modal } from 'antd';
import { SearchOutlined,EditOutlined, PlusOutlined,CloseCircleOutlined  } from '@ant-design/icons';
import { DataTableFilterOption, FieldType } from 'components/Datatable/filter';
import { Column } from 'types/dataTable';
import { Link, useHistory } from 'react-router-dom';
import orderService from 'services/order';
import { Customer } from 'types/customer';
import moment from 'moment';
import { toDateTimeString } from 'Utils/dateUtils';
import { Order, OrderStatusMap } from 'types/order';
import { handleErrors } from 'Utils/errorHandler';
import { useDispatch } from 'react-redux';
import { AppDispatch } from 'redux/store';

const OrderList: React.FC = () => {

  const history = useHistory();
  const dispatch = useDispatch<AppDispatch>();  

  const columns: Column[] = [
    {
      title: 'Customer',
      dataIndex: 'customer',
      sortKey: 'customer.name',
      sorter: true,
      render: (c:Customer) =>c.name
    },
    {
        title: 'Order Date',
        dataIndex: 'orderDate',
        sorter: true,
        render: (d:moment.Moment)=> toDateTimeString(d)
    },    
    {
      title: 'Ship Date',
      dataIndex: 'shipDate',
      sorter: true,
      render: (d:moment.Moment)=> toDateTimeString(d)
    },    
    {
      title: 'Total Qnt',
      dataIndex: 'totalQnt',
      sorter: false,
    },    
    {
      title: 'Status',
      dataIndex: 'statusLabel',
      sortKey: 'status',
      sorter: true
    },
    {
      title: 'Actions',
      dataIndex: 'uid',
      sorter: false,
      render: (_: any, order: Order) => {
        return (
          <React.Fragment>
            <Space>
              <Link to={`/dispatch/orders/${order.id}`}>
                <Button icon={<SearchOutlined />}/>
              </Link>
              {order.status===0 && (
                <Link to={`/dispatch/orders/edit/${order.id}`}>
                  <Button icon={<EditOutlined />}/>
                </Link>)}
              {order.status===0 && (
                <Button icon={<CloseCircleOutlined />} onClick={()=> confirmCancelOrder(order.id)}/>
              )}
            </Space>       
          </React.Fragment>
        );
      },
    },
  ];

  const confirmCancelOrder = (id: number) => {
    Modal.confirm({
      title: 'Confirm',
      icon: <CloseCircleOutlined />,
      content: 'Are you Sure to cancel this order?',
      okText: 'Yes',
      onOk: () => cancelOrder(id),
      cancelText: 'No',
    });
  }

  const cancelOrder = (id: number) => {
     (async () => {
      try {
          await orderService.cancelOrder(id);
      } catch (error) {
          handleErrors(error, history, dispatch);
      }
  })();     
  }

  const filterOptions: DataTableFilterOption[] = [
    {
      title: 'Name',
      field: 'customer.name',
      fieldType: FieldType.STRING,
    },
    {
        title: 'Order Date',
        field: 'orderDate',
        fieldType: FieldType.DATETIME,
    },
    {
        title: 'Ship Date',
        field: 'shipDate',
        fieldType: FieldType.DATETIME,
    },
    {
        title: 'Status',
        field: 'status',
        fieldType: FieldType.OPTION,
        fieldOptions : OrderStatusMap 
    },

    
  ];

  return (
    <div>
      <React.Fragment>
        <Datatable
          searchApi={orderService.search}
          title ={"Order List"}
          filters={filterOptions}
          columns={columns}
          actions={
            <Row>
              <Col md={20}>
              <Link to={`/dispatch/orders/add`}>
                <Button icon={<PlusOutlined />}>Add new</Button>
                </Link>
              </Col>
            </Row>
          }
          rowKey="id"
        />
      </React.Fragment>
    </div>
  );
};

export default OrderList;
