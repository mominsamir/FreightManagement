
type AggregateResponseData = Record<string,any>; 

export interface AggregateResponse {
    length: number;
    data:   AggregateResponseData[];
} 


export enum AggregateOperator {
    AVG="AVG", COUNT="COUNT", MIN="MIN", MAX="MAX", SELECT="SELECT", SUM="SUM"
}

export enum FieldSort {
    asc="asc", desc="desc"
}

export enum FilterOperator {
    EQUAL="EQUAL", 
    NOT_EQUAL="NOT_EQUAL", 
    LIKE="LIKE", 
    NOT_LIKE="NOT_LIKE", 
    CONTAIN="CONTAIN", 
    DOES_NOT_CONTAIN='DOES_NOT_CONTAIN', 
    STARTS_WITH='STARTS_WITH', 
    ENDS_WITH="ENDS_WITH", 
    GREATER_THAN='GREATER_THAN', 
    LESS_THAN="LESS_THAN", 
    GREATER_THAN_OR_EQUAL="GREATER_THAN_OR_EQUAL", 
    LESS_THAN_OR_EQUAL="LESS_THAN_OR_EQUAL", 
    BETWEEN="BETWEEN", 
    IN='IN', 
    NOT_IN="NOT_IN", 
    IS_EMPTY="IS_EMPTY", 
    NOT_EMPTY="NOT_EMPTY"
}

export interface AggregateRequest {
    fields:     Field[];
    filterData: FilterData[];
}

export interface Field {
    name:       string;
    operator:   AggregateOperator;
    sortOrder?: FieldSort;
}

export interface FilterData {
    name:     string;
    value:    string;
    operator: FilterOperator;
}
