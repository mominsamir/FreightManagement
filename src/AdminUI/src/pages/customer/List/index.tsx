import React, { useState } from 'react';
import { Datatable } from 'components';
import customerServices from 'services/customer';
import { Row, Col, Button, Space } from 'antd';
import { SearchOutlined,EditOutlined, PlusOutlined  } from '@ant-design/icons';
import { DataTableFilterOption, FieldType } from 'components/Datatable/filter';
import { Column } from 'types/dataTable';
import { Link } from 'react-router-dom';
import { Customer } from 'types/customer';
import CreateCustomer from '../Add';

const CustomerList: React.FC = () => {

  const [key, setKey] = useState<number>(1);
  const [showCreateModel, setShowCreateModel] = useState<boolean>(false);


  const columns: Column[] = [
    {
      title: 'Name',
      dataIndex: 'name',
      sorter: true
    },
    {
        title: 'Email',
        dataIndex: 'email',
        sorter: false,
    },    
    {
      title: 'Street',
      dataIndex: 'street',
      sorter: false,
    },    
    {
      title: 'City',
      dataIndex: 'city',
      sorter: false
    },    
    {
      title: 'State',
      dataIndex: 'state',
      sorter: false
    },    
    {
      title: 'ZipCode',
      dataIndex: 'zipCode',
      sorter: false
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
      render: (_: any, record: Customer) => {
        return (
          <React.Fragment>
            <Space>
              <Link to={`/dispatch/customers/${record.id}`}>
                <Button icon={<SearchOutlined />}/>
              </Link>
              <Link to={`/dispatch/customers/edit/${record.id}`}>
                <Button icon={<EditOutlined />}/>
              </Link>
            </Space>            
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
        title: 'Email',
        field: 'email',
        fieldType: FieldType.STRING,
    },
    {
        title: 'Street',
        field: 'street',
        fieldType: FieldType.STRING,
    },
    {
        title: 'City',
        field: 'city',
        fieldType: FieldType.STRING,
    },
    {
        title: 'State',
        field: 'state',
        fieldType: FieldType.STRING,
    },
    {
        title: 'ZipCode',
        field: 'zipCode',
        fieldType: FieldType.STRING,
    },
    {
      title: 'Is Active',
      field: 'isActive',
      fieldType: FieldType.BOOLEAN
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
          searchApi={customerServices.search}
          title ={"Customer List"}
          filters={filterOptions}
          columns={columns}
          actions={
            <Row>
              <Col md={20}>
              <Button type="primary" onClick={() => addNewUser()} icon={<PlusOutlined/>}>
                  Add new
                </Button>
              </Col>
            </Row>
          }
          rowKey="id"
        />
      </React.Fragment>
      {showCreateModel &&
        <CreateCustomer 
            customer={undefined}
            cancel={toogleModel}>
        </CreateCustomer>
      }          
     </div>
  );
};

export default CustomerList;
