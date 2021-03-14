import { ColumnType } from "antd/lib/table/interface";

export type SortOrder = 'ascend' | 'descend';
export type FilterOperator =
  | 'EQUAL'
  | 'NOT_EQUAL'
  | 'CONTAIN'
  | 'DOES_NOT_CONTAIN'
  | 'STARTS_WITH'
  | 'ENDS_WITH'
  | 'GREATER_THAN'
  | 'LESS_THAN'
  | 'GREATER_THAN_OR_EQUAL'
  | 'LESS_THAN_OR_EQUAL'
  | 'BETWEEN'
  | 'IN'
  | 'NOT_IN'
  | 'IS_EMPTY'
  | 'NOT_EMPTY';

export interface SearchParams {
  page: number;
  pageSize: number;
  sortData: SortColumn[];
  filterData: FilterData[];
}

export interface FilterData {
  name: string;
  value: string;
  operator?: FilterOperator;
}

export interface SortColumn {
  column: string;
  sortOrder: SortOrder;
}

export interface PaginatedSearchResult<T> {
  recordsTotal: number;
  draw: number;
  recordsFiltered: number;
  data: T[];
}

export interface Column extends ColumnType<any> {
  sortKey?: string;
  defaultSortOrder?: SortOrder;
  dataIndex: string;
  title?: string;
}
