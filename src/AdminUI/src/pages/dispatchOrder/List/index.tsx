import React, { useState } from 'react';
import { Datatable } from 'components';
import dispatchOrderServices from 'services/dispatchOrder';
import { Row, Col, Button } from 'antd';
import { SearchOutlined  } from '@ant-design/icons';
import { DataTableFilterOption, FieldType } from 'components/Datatable/filter';
import { Column } from 'types/dataTable';
import { Link} from 'react-router-dom';
import { Moment } from 'moment-timezone';
import {  toDateTimeString } from 'Utils/dateUtils';
import { DriverSchedule, scheduleStatusMap } from 'types/driverSchedule';
import { Dispatch } from 'types/dispatchOrder';

const DispatchList: React.FC = () => {

  const [key, setKey] = useState<number>(1);
  const [showCreateModel, setShowCreateModel] = useState<boolean>(false);


  const columns: Column[] = [
    {
      title: 'Driver',
      dataIndex: 'driverSchedule',
      sorter: true,
      sortKey : 'driverSchedule.driver.firstName',
      render: (schedule: DriverSchedule) => `${schedule.driver.firstName} ${schedule.driver.lastName}`
    },
    {
        title: 'Dispatch Time',
        dataIndex: 'dispatchDateTime',
        sorter: true,
        render: (dispatchDateTime: Moment) => toDateTimeString(dispatchDateTime)
    },    
    {
      title: 'Truck',
      dataIndex: 'driverSchedule',
      sorter: true,
      render: (schedule: DriverSchedule) => `${schedule.truck.numberPlate}`      
    },    
    {
      title: 'Trailer',
      dataIndex: 'driverSchedule',
      sorter: true,
      render: (schedule: DriverSchedule) => `${schedule.trailer.numberPlate}`      
    },
    {
      title: 'Miles',
      dataIndex: 'miles',
      sorter: true
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
      render: (_: any, record: Dispatch) => {
        return (
          <React.Fragment>
            <Link to={`/dispatch/dispatches/${record.id}`}>
              <Button icon={<SearchOutlined />}/>
            </Link>
          </React.Fragment>
        );
      },
    },
  ];

  const filterOptions: DataTableFilterOption[] = [
    {
      title: 'Driver First Name',
      field: 'Driver.`FirstName`',
      fieldType: FieldType.STRING,
    },
    {
      title: 'Driver Last Name',
      field: 'Driver.LastName',
      fieldType: FieldType.STRING,
    },
    {
        title: 'Start Date',
        field: 'StartTime',
        fieldType: FieldType.DATETIME,
    },
    {
      title: 'End Date',
      field: 'EndTime',
      fieldType: FieldType.DATETIME,
    },
    {
      title: 'Status',
      field: 'Status',
      fieldType: FieldType.OPTION,
      fieldOptions: scheduleStatusMap
    }
  ];

  const toogleModel = () => {
    setShowCreateModel(!showCreateModel);
    if(!showCreateModel){
        setKey(key + 1);
    }
  };

  const addNewUser = () => {
    toogleModel();
  };  


  return (
    <div>
      <React.Fragment>
        <Datatable
          key={key}
          searchApi={dispatchOrderServices.search}
          title ={"Dispatch Lists"}
          filters={filterOptions}
          columns={columns}
          actions={
            <Row>
              <Col md={20}>
              <Button type="primary" onClick={() => addNewUser()}>
                  Add new
                </Button>
              </Col>
            </Row>
          }
          rowKey="id"
        />
      </React.Fragment>
    </div>
  );
};

export default DispatchList;
/*

*/