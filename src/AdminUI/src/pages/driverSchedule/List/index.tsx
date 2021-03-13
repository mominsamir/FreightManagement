import React, { useState, useEffect } from 'react';
import { Datatable } from 'components';
import driverScheduleServices from 'services/driverSchedule';
import { Row, Col, Button } from 'antd';
import { EditFilled  } from '@ant-design/icons';
import { DataTableFilterOption, FieldType } from 'components/Datatable/filter';
import { User } from 'types/user';
import { Column } from 'types/dataTable';
import _ from 'lodash';
import { useHistory } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { AppDispatch } from 'redux/store';
import { handleErrors } from 'Utils/errorHandler';
import { Moment } from 'moment-timezone';
import {  toDateTimeString } from 'Utils/dateUtils';
import { Trailer, Truck } from 'types/vehicle';
import { DriverSchedule } from 'types/driverSchedule';
import CreateSchedule from '../Add';

const DriverScheduleList: React.FC = () => {

  const [key, setKey] = useState<number>(1);
  const [schedule, setSchedule] = useState<DriverSchedule>();
  const [mode, setMode] = useState<'ADD'|'EDIT'>('ADD');
  const [editedId, setEditId] = useState<number>(0);
  const [showCreateModel, setShowCreateModel] = useState<boolean>(false);
  const history = useHistory();
  const dispatch = useDispatch<AppDispatch>();  


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
            <Button
              icon={<EditFilled/>}
              onClick={() => {
                setEditId(record.id);
              }}
            />
          </React.Fragment>
        );
      },
    },
  ];

  const filterOptions: DataTableFilterOption[] = [
    {
      title: 'Bank Name',
      field: 'bankName',
      fieldType: FieldType.STRING,
    },
    {
        title: 'Account Name',
        field: 'name',
        fieldType: FieldType.STRING,
    },
    {
      title: 'Routing #',
      field: 'routingNumber',
      fieldType: FieldType.STRING,
    },
    {
      title: 'Account #',
      field: 'accountNumber',
      fieldType: FieldType.STRING,
    }
  ];

  const toogleModel = () => {
    setShowCreateModel(!showCreateModel);
    if(!showCreateModel){
        setKey(key + 1);
    }
  };

  useEffect(() => {
    (async () => {
      try {
        if(editedId !== 0){
            var ds = await driverScheduleServices.find(editedId);
            setSchedule(ds);
            setMode('EDIT');
            toogleModel();
        }
      } catch (error) {
        handleErrors(error, history, dispatch);
      }
    })();
  }, [dispatch, history,editedId]);


  const addNewUser = () => {
    setMode('ADD');
    setSchedule(undefined);
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