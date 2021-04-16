import React, { useState } from 'react';
import { Datatable } from 'components';
import driverScheduleServices from 'services/driverSchedule';
import { Row, Col, Button } from 'antd';
import { SearchOutlined  } from '@ant-design/icons';
import { DataTableFilterOption, FieldType } from 'components/Datatable/filter';
import { User } from 'types/user';
import { Column } from 'types/dataTable';
import { Link} from 'react-router-dom';
import { Moment } from 'moment-timezone';
import {  toDateTimeString } from 'Utils/dateUtils';
import { Trailer, Truck } from 'types/vehicle';
import { DriverSchedule, scheduleStatusMap } from 'types/driverSchedule';
import CreateSchedule from '../Add';

const DriverScheduleList: React.FC = () => {

  const [key, setKey] = useState<number>(1);
  const [showCreateModel, setShowCreateModel] = useState<boolean>(false);


  const columns: Column[] = [
    {
      title: 'Driver',
      dataIndex: 'driver',
      sorter: true,
      render: (driver: User) => `${driver.firstName} ${driver.lastName}`
    },
    {
        title: 'Start Date',
        dataIndex: 'startTime',
        sorter: true,
        render: (startTime: Moment) => toDateTimeString(startTime)
    },
    {
        title: 'End Date',
        dataIndex: 'endTime',
        sorter: true,
        render: (endTime: Moment) => toDateTimeString(endTime)
    },    
    {
      title: 'Truck',
      dataIndex: 'truck',
      sorter: true,
      render: (truck: Truck) => `${truck.numberPlate}`      
    },    
    {
      title: 'Trailer',
      dataIndex: 'trailer',
      sorter: true,
      render: (trailer: Trailer) => `${trailer.numberPlate}`      
    },
    {
      title: 'Status',
      dataIndex: 'status',
      sorter: true
    },
    {
      title: 'Actions',
      dataIndex: 'uid',
      sorter: false,
      render: (_: any, record: DriverSchedule) => {
        return (
          <React.Fragment>
            <Link to={`/dispatch/schedules/${record.id}`}>
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
      field: 'Driver.FirstName',
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
          searchApi={driverScheduleServices.search}
          title ={"Driver Schedules"}
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
      {showCreateModel &&
        <CreateSchedule 
          cancel={toogleModel}>
        </CreateSchedule>
      }      
    </div>
  );
};

export default DriverScheduleList;
/*

*/