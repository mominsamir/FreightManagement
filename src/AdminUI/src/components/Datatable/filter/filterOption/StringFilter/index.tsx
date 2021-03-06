import {  Form, Input,  Select } from 'antd';
import React, { useState } from 'react';
import { FilterOperator } from 'types/dataTable';
import { DataTableFilterOption } from '../..';


const StringFilter: React.FC<DataTableFilterOption> = props => {

    const [opterator, setOperator] = useState<FilterOperator>('CONTAIN'); 

    return (
        <div style={{display:'flex'}}>
            <Form.Item
                style={{width:'50%'}}                
                name={[props.fieldKey!!, "options"]} fieldKey={[props.fieldKey!!, "options"]} initialValue={opterator}>
                <Select  placeholder="Filter Option"
                    onChange={(val:FilterOperator)=>setOperator(val)} >
                    <Select.Option value="EQUAL">Equal</Select.Option>
                    <Select.Option value="NOT_EQUAL">Not Equal</Select.Option>
                    <Select.Option value="CONTAIN">Contain</Select.Option>
                    <Select.Option value="DOES_NOT_CONTAIN">Does not Contains</Select.Option>                    
                    <Select.Option value="STARTS_WITH">Start With</Select.Option>
                    <Select.Option value="ENDS_WITH">Ends With</Select.Option>
                    <Select.Option value="IS_EMPTY">Is Empty</Select.Option>
                    <Select.Option value="NOT_EMPTY">Not Empty</Select.Option>
                </Select>
              </Form.Item>
              <Form.Item name={[props.fieldKey!!, "name"]} fieldKey={[props.fieldKey!!, "name"]} hidden={true} initialValue={props.field}>
                  <input type="hidden"/>
              </Form.Item>              
            <Form.Item
                style={{width:'50%'}}
                name={[props.fieldKey!!, "value"]}  fieldKey={[props.fieldKey!!, "value"]}>
                <Input placeholder={props.title}></Input>
            </Form.Item>
        </div>
    )
}


export default StringFilter;