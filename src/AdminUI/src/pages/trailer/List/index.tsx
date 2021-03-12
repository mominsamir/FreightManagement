import React, { useState, useEffect } from 'react';
import { Datatable } from 'components';
import trailerServices from 'services/trailer';
import { Row, Col, Button } from 'antd';
import { EditFilled  } from '@ant-design/icons';
import { DataTableFilterOption, FieldType } from 'components/Datatable/filter';
import { Column } from 'types/dataTable';
import { useHistory } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { AppDispatch } from 'redux/store';
import { handleErrors } from 'Utils/errorHandler';
import CreateUpdateTrailer from '../Add';
import { Trailer, vehicleStatusMap } from 'types/vehicle';

const TrailerList: React.FC = () => {

  const [key, setKey] = useState<number>(1);
  const [trailer, setTrailer] = useState<Trailer>();
  const [mode, setMode] = useState<'ADD'|'EDIT'>('ADD');
  const [editedUserId, setEditUserId] = useState<number>(0);
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
      title: 'Capacity',
      dataIndex: 'capacity',
      sorter: true,
    },    
    {
      title: 'Compartment',
      dataIndex: 'compartment',
      sorter: true,
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
      render: (_: any, record: Trailer) => {
        return (
          <React.Fragment>
            <Button
              icon={<EditFilled/>}
              onClick={() => {
                setEditUserId(record.id);
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
        title: 'Capacity',
        field: 'capacity',
        fieldType: FieldType.STRING,
    },
    {
        title: 'Compartment',
        field: 'compartment',
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
        if(editedUserId !== 0){
            var trailer = await trailerServices.find(editedUserId);
            setTrailer(trailer);
            setMode('EDIT');
            toogleModel();
        }
      } catch (error) {
        handleErrors(error, history, dispatch);
      }
    })();
  }, [dispatch, history,editedUserId]);


  const addNewUser = () => {
    setMode('ADD');
    setTrailer(undefined);
    setEditUserId(0);
    toogleModel();
  };  


  return (
    <div>
      <React.Fragment>
        <Datatable
          key={key}
          searchApi={trailerServices.search}
          title ={"Trailer List"}
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
        <CreateUpdateTrailer 
            trailer={trailer}
            mode={mode} 
            cancel={toogleModel}>
        </CreateUpdateTrailer>
      }      
    </div>
  );
};

export default TrailerList;
