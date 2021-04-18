import React, { useState, useEffect } from 'react';
import { Datatable } from 'components';
import { Row, Col, Button } from 'antd';
import { EditFilled  } from '@ant-design/icons';
import { DataTableFilterOption, FieldType } from 'components/Datatable/filter';
import { User } from 'types/user';
import { Column } from 'types/dataTable';
import { useHistory } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { AppDispatch } from 'redux/store';
import { handleErrors } from 'Utils/errorHandler';
import CreateUpdateRackModel from '../Add';
import rackService from 'services/rack';
import { Rack } from 'types/rack';

const RackList: React.FC = () => {

  const [key, setKey] = useState<number>(1);
  const [rack, setRack] = useState<Rack>();
  const [mode, setMode] = useState<'ADD'|'EDIT'>('ADD');
  const [editedId, setEditId] = useState<number>(0);
  const [showCreateModel, setShowCreateModel] = useState<boolean>(false);
  const history = useHistory();
  const dispatch = useDispatch<AppDispatch>();  


  const columns: Column[] = [
    {
      title: 'Name',
      dataIndex: 'name',
      sorter: true
    },
    {
        title: 'IRS Code',
        dataIndex: 'irsCode',
        sorter: true,
    },    
    {
      title: 'Street',
      dataIndex: 'street',
      sortKey: 'address.street',
      sorter: true,
    },    
    {
      title: 'City',
      dataIndex: 'city',
      sortKey: 'address.city',
      sorter: true,
    },    
    {
      title: 'State',
      dataIndex: 'state',
      sortKey: 'address.state',
      sorter: true,
    },    
    {
      title: 'ZipCode',
      dataIndex: 'zipCode',
      sortKey: 'address.zipCode',
      sorter: true,
    },    
    {
      title: 'Active',
      dataIndex: 'isActive',
      sorter: true,
      render: ((s:boolean) => s ? 'Yes': 'No')
    },
    {
      title: 'Actions',
      dataIndex: 'uid',
      sorter: false,
      render: (_: any, record: User) => {
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
      field: 'name',
      fieldType: FieldType.STRING,
    },
    {
        title: 'Grade',
        field: 'grade',
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
            var user = await rackService.find(editedId);
            setRack(user);
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
    setRack(undefined);
    setEditId(0);
    toogleModel();
  };  


  return (
    <div>
      <React.Fragment>
        <Datatable
          key={key}
          searchApi={rackService.search}
          title ={"Rack List"}
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
        <CreateUpdateRackModel 
            rack={rack}
            mode={mode} 
            cancel={toogleModel}>
        </CreateUpdateRackModel>
      }      
    </div>
  );
};

export default RackList;
