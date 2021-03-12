import React, { useState, useEffect } from 'react';
import { Datatable } from 'components';
import productServices from 'services/product';
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
import CreateUpdateFuelProduct from '../Add';
import { FuelProduct } from 'types/product';

const UserList: React.FC = () => {

  const [key, setKey] = useState<number>(1);
  const [product, setProduct] = useState<FuelProduct>();
  const [mode, setMode] = useState<'ADD'|'EDIT'>('ADD');
  const [editedUserId, setEditUserId] = useState<number>(0);
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
        title: 'Fuel Grade',
        dataIndex: 'grade',
        sorter: true,
        render: (_,record: FuelProduct) => `${record.grade}`
    },    
    {
      title: 'UOM',
      dataIndex: 'uom',
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
        if(editedUserId !== 0){
            var user = await productServices.find(editedUserId);
            setProduct(user);
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
    setProduct(undefined);
    setEditUserId(0);
    toogleModel();
  };  


  return (
    <div>
      <React.Fragment>
        <Datatable
          key={key}
          searchApi={productServices.search}
          title ={"Fuel Product List"}
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
        <CreateUpdateFuelProduct 
            fuelProduct={product}
            mode={mode} 
            cancel={toogleModel}>
        </CreateUpdateFuelProduct>
      }      
    </div>
  );
};

export default UserList;
