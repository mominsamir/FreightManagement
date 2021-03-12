import React, { useState, useEffect } from 'react';
import { Datatable } from 'components';
import truckServices from 'services/truck';
import { Row, Col, Button } from 'antd';
import { EditFilled  } from '@ant-design/icons';
import { DataTableFilterOption, FieldType } from 'components/Datatable/filter';
import { Column } from 'types/dataTable';
import { useHistory } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { AppDispatch } from 'redux/store';
import { handleErrors } from 'Utils/errorHandler';
import { vehicleStatusMap } from 'types/vehicle';
import { Truck } from 'types/vehicle';
import CreateUpdateTruck from '../Add';
import { Moment } from 'moment-timezone';
import { toDateString } from 'Utils/dateUtils';

const TruckList: React.FC = () => {

  const [key, setKey] = useState<number>(1);
  const [truck, setTruck] = useState<Truck>();
  const [mode, setMode] = useState<'ADD'|'EDIT'>('ADD');
  const [editedId, setEditId] = useState<number>(0);
  const [showCreateModel, setShowCreateModel] = useState<boolean>(false);
  const history = useHistory();
  const dispatch = useDispatch<AppDispatch>();  

  

  const columns: Column[] = [
    {
      title: 'Number',
      dataIndex: 'numberPlate',
      sorter: true
    },
    {
        title: 'VIN',
        dataIndex: 'vin',
        sorter: true
    },    
    {
      title: 'Maintance Date',
      dataIndex: 'nextMaintanceDate',
      sorter: true,
      render: (date:Moment) => toDateString(date)
    },    
    {
      title: 'Status',
      dataIndex: 'status',
      sorter: true,
    },
    {
      title: 'Actions',
      dataIndex: 'uid',
      sorter: false,
      render: (_: any, record: Truck) => {
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
      title: 'Name',
      field: 'numberPlate',
      fieldType: FieldType.STRING,
    },
    {
        title: 'Grade',
        field: 'vin',
        fieldType: FieldType.STRING,
    },
    {
        title: 'Status',
        field: 'status',
        fieldType: FieldType.OPTION,
        fieldOptions:  vehicleStatusMap 

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
            var trailer = await truckServices.find(editedId);
            setTruck(trailer);
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
    setTruck(undefined);
    setEditId(0);
    toogleModel();
  };  


  return (
    <div>
      <React.Fragment>
        <Datatable
          key={key}
          searchApi={truckServices.search}
          title ={"Truck List"}
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
        <CreateUpdateTruck 
            truck={truck}
            mode={mode} 
            cancel={toogleModel}>
        </CreateUpdateTruck>
      }      
    </div>
  );
};

export default TruckList;
