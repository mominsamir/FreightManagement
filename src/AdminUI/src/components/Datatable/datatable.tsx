import React, { useState, useEffect } from 'react';
import { Table, Layout,Button, Row, Col, Tooltip, Space } from 'antd';
import { useHistory } from 'react-router-dom';
import { handleErrors } from 'Utils/errorHandler';
import { useDispatch } from 'react-redux';
import { TablePaginationConfig } from 'antd/lib/table';
import { SorterResult } from 'antd/lib/table/interface';
import { SearchParams, PaginatedSearchResult, Column, SortColumn, SortOrder, FilterData } from 'types/dataTable';
import DataTableFilter, { DataTableFilterOption } from './filter';
import { FilterFilled, FileExcelFilled  } from '@ant-design/icons';
import styles from './datatable.module.less';

interface Props {
  columns: Column[];
  filters?: DataTableFilterOption[];
  searchApi: (searchParams: SearchParams) => Promise<PaginatedSearchResult<any>>;
  downloadApi?: (searchParams: SearchParams) => void;
  title?: string;
  actions?: React.ReactNode, 
  rowKey: string;
  defaultFilters?: FilterData[];
  additionalCols?: string[];

}

const DataTable: React.FC<Props> = ({ columns,filters, searchApi, downloadApi, title, actions,  rowKey, defaultFilters, additionalCols }) => {
  const getSortKey = (column: Column): string => (column.sortKey ? column.sortKey : column.dataIndex);
  const getSortOrder = (order: string): SortOrder => (order === 'ascend' ? 'ascend' : 'descend');
  const dispatch = useDispatch();

  const DEF_PAGE_SIZE = 50;
  const defaultSortCol = columns.find((c) => c.defaultSortOrder);
  const [data, setData] = useState<any[]>([]);
  const [filterData, setFilterData] = useState<FilterData[]>(defaultFilters||[]);
  const [showAdanceFilters, setShowAdanceFilters] = useState<boolean>(false);  
  const [sorts, setSorts] = useState<SortColumn | null>(
    defaultSortCol
      ? { column: getSortKey(defaultSortCol), sortOrder: getSortOrder(defaultSortCol.defaultSortOrder || 'descend') }
      : null
  );
  const [loading, setLoading] = useState(true);
  const [page, setPage] = useState(1);
  const [totalRecords, setTotalRecords] = useState(0);
  const history = useHistory();

  const { Header, Content } = Layout;

  useEffect(() => {
    (async () => {
      try {
        setLoading(true);
        let response = await searchApi({
          page: page,
          pageSize: DEF_PAGE_SIZE,
          sortData: sorts ? [sorts] : [],
          filterData: filterData ? filterData : []
        });
        setLoading(false);
        setData(response.data);
        setTotalRecords(response.recordsFiltered);
      } catch (err) {
        handleErrors(err, history, dispatch);
      }
    })();
  }, [filterData, dispatch, history, page, searchApi, sorts, additionalCols]);

  const onChange = ( pagination: TablePaginationConfig, filters: Record<string, any | null>, sorter: SorterResult<any> | SorterResult<any>[]) => {
    if (!Array.isArray(sorter) && sorter.order && sorter.column) {
      let sortKey = getSortKey(sorter.column as Column);
      let sortOrder = getSortOrder(sorter.order);
      setSorts({ column: sortKey, sortOrder: sortOrder });
    } else {
      setSorts(null);
    }
    setPage(pagination.current || 1);
  };

  const onFilter = (filter: FilterData[])=>{
    setFilterData(filter.concat(defaultFilters||[]))
  } 

  const onClear = () => {
    setFilterData(defaultFilters||[]);
  }

  const downloadFile = () =>{
    (async () => {
      try {
        setLoading(true);
        downloadApi ? downloadApi({
          page: page,
          pageSize: DEF_PAGE_SIZE,
          sortData: sorts ? [sorts] : [],
          filterData: filterData ? filterData : []
        }) : 
        setLoading(false);
        setLoading(false);
      } catch (err) {
        handleErrors(err, history, dispatch);
      }
    })();

  }

  return (
    <Layout>
      <Header className={styles.header}>
        <Row gutter={8} >
          <Col span={4} className={styles.title}><span>{title}</span></Col>
          <Col span={20} className={styles.action}>
            <Space size="small" direction="horizontal">
              {actions}
              {filters &&
                <Tooltip title="Adavnce Filter">
                  <Button type="primary" icon={<FilterFilled />} onClick={()=>setShowAdanceFilters(!showAdanceFilters)}/>
                </Tooltip>
              }
              {downloadApi && 
                  <Tooltip title="Download">
                    <Button type="primary" icon={<FileExcelFilled />} onClick={()=>downloadFile()}/>
                  </Tooltip>
              }
            </Space>              
          </Col>          
        </Row>
      </Header>
      {(filters && showAdanceFilters) &&
        <Row gutter={8} className={styles.filters}>
          <Col span={24} >
            <DataTableFilter key="data_Filters" filterOptions={filters} onFilter={onFilter} onClear={onClear}></DataTableFilter>
          </Col>
        </Row>
      }
      <Content>
        <Table
          loading={loading}
          columns={columns}
          dataSource={data}
          rowKey={(record) => record[rowKey]}
          onChange={onChange}
          pagination={{
            pageSize: DEF_PAGE_SIZE,
            total: totalRecords,
            showTotal: (total, range) => `${range[0]}-${range[1]} of ${total} items`,
          }}
        />        
      </Content>
    </Layout>
  );
};


export default DataTable;
