import React, { useState, useEffect } from 'react';
import { Datatable } from 'components';
import userServices from 'services/user';
import { Row, Col, Button } from 'antd';
import { EditFilled  } from '@ant-design/icons';
import { DataTableFilterOption, FieldType } from 'components/Datatable/filter';
import { User } from 'types/user';
import { Column } from 'types/dataTable';
import { useHistory } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { AppDispatch } from 'redux/store';
import { handleErrors } from 'Utils/errorHandler';
import CreateUpdateUser from '../Add';

const UserList: React.FC = () => {

  const [key, setKey] = useState<number>(1);
  const [user, setUser] = useState<User>();
  const [mode, setMode] = useState<'ADD'|'EDIT'>('ADD');
  const [editedUserId, setEditUserId] = useState<number>(0);
  const [showCreateModel, setShowCreateModel] = useState<boolean>(false);
  const history = useHistory();
  const dispatch = useDispatch<AppDispatch>();  


  const columns: Column[] = [
    {
      title: 'First Name',
      dataIndex: 'firstName',
      sorter: true
    },
    {
        title: 'Last Name',
        dataIndex: 'lastName',
        sorter: true,
        render: (_,record: User) => `${record.lastName}`
    },    
    {
      title: 'Email',
      dataIndex: 'email',
      sorter: true,
    },
    {
      title: 'Role',
      dataIndex: 'role',
      sorter: true,
    },
    {
      title: 'Active',
      dataIndex: 'isActive',
      sorter: true,
      render: (isActive: boolean) => `${isActive ? 'Yes':'No'}` 
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
      title: 'First Name',
      field: 'FirstName',
      fieldType: FieldType.STRING,
    },
    {
        title: 'Last Name',
        field: 'LastName',
        fieldType: FieldType.STRING,
    },
    {
      title: 'Email',
      field: 'Email',
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
            var user = await userServices.find(editedUserId);
            setUser(user);
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
    setUser(undefined);
    toogleModel();
  };  


  return (
    <div>
      <React.Fragment>
        <Datatable
          key={key}
          searchApi={userServices.search}
          title ={"Application User"}
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
        <CreateUpdateUser 
          user={user} 
          mode={mode} 
          cancel={toogleModel}>
        </CreateUpdateUser>
      }      
    </div>
  );
};

export default UserList;
