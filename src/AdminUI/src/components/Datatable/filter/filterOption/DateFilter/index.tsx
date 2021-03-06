import React, { useState } from 'react';
import {  DatePicker, Form, Select } from 'antd';
import { DataTableFilterOption } from '../..';
import { FilterOperator } from 'types/dataTable';



const DateFilter: React.FC<DataTableFilterOption> = props => {

    const {RangePicker} = DatePicker;

    const [opterator, setOperator] = useState<FilterOperator>('EQUAL'); 

    return (
        <div style={{display:'flex'}}>
            <Form.Item 
                style={{width:'100%'}}
                name={[props.fieldKey!!, "options"]} 
                fieldKey={[props.fieldKey!!, "options"]}
                initialValue={opterator}>
                <Select  placeholder="Filter Option" onChange={(val:FilterOperator)=>setOperator(val)}>
                    <Select.Option value="EQUAL">Equal</Select.Option>
                    <Select.Option value="NOT_EQUAL">Not Equal</Select.Option>
                    <Select.Option value="GREATER_THAN">Greater Than</Select.Option>
                    <Select.Option value="GREATER_THAN_OR_EQUAL">Greater Than Or Equal</Select.Option>                    
                    <Select.Option value="LESS_THAN">Less Than</Select.Option>
                    <Select.Option value="LESS_THAN_OR_EQUAL">Less Than Or Equal</Select.Option>
                    <Select.Option value="BETWEEN">Between</Select.Option>                    
                    <Select.Option value="IS_EMPTY">Is Empty</Select.Option>
                    <Select.Option value="NOT_EMPTY">Not Empty</Select.Option>
                </Select>
            </Form.Item>
            <Form.Item name={[props.fieldKey!!, "name"]} fieldKey={[props.fieldKey!!, "name"]} hidden={true} initialValue={props.field}>
                  <input type="hidden"/>
            </Form.Item>
            {opterator === 'BETWEEN' &&  
                <Form.Item
                    style={{width:'100%'}}             
                    name={[props.fieldKey!!, "value"]} fieldKey={[props.fieldKey!!, "value"]} >
                    <RangePicker format={'MM-DD-YYYY'}></RangePicker> 
                </Form.Item>            
            }
            {opterator !== 'BETWEEN' &&  
                <Form.Item
                    style={{width:'100%'}}             
                    name={[props.fieldKey!!, "value"]} fieldKey={[props.fieldKey!!, "value"]} >
                    <DatePicker placeholder={props.title} format={'MM-DD-YYYY'}></DatePicker> 
                </Form.Item>            
            }
        </div>
    )
}


export default DateFilter;