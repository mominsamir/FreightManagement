import React from 'react';
import { Datatable } from 'components';
import locationServices from 'services/location';
import { Row, Col, Button, Space } from 'antd';
import { SearchOutlined, PlusOutlined, EditOutlined   } from '@ant-design/icons';
import { DataTableFilterOption, FieldType } from 'components/Datatable/filter';
import { Column } from 'types/dataTable';
import { Link } from 'react-router-dom';

const LocationList: React.FC = () => {


  const columns: Column[] = [
    {
      title: 'Name',
      dataIndex: 'name',
      sorter: true
    },
    {
        title: 'Email',
        dataIndex: 'email',
        sorter: true,
    },    
    {
      title: 'Street',
      dataIndex: 'street',
      sorter: true,
    },    
    {
      title: 'City',
      dataIndex: 'city',
      sorter: true,
    },    
    {
      title: 'State',
      dataIndex: 'state',
      sorter: true,
    },    
    {
      title: 'ZipCode',
      dataIndex: 'zipCode',
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
      dataIndex: 'id',
      sorter: false,
      render: (id:  number) => {
        return (
          <React.Fragment>
            <Space>
              <Link to={`/dispatch/locations/${id}`}>
                <Button icon={<SearchOutlined />}/>
              </Link>
              <Link to={`/dispatch/locations/edit/${id}`}>
                <Button icon={<EditOutlined twoToneColor="#eb2f96"/>}/>
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

  return (
    <div>
      <React.Fragment>
        <Datatable
          searchApi={locationServices.search}
          title ={"Location List"}
          filters={filterOptions}
          columns={columns}
          actions={
            <Row>
              <Col md={20}>
                <Link to={`/dispatch/locations/add`}>
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

export default LocationList;
