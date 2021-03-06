import {  Form, Select } from 'antd';
import React, { useState } from 'react';
import { FilterOperator } from 'types/dataTable';
import { DataTableFilterOption } from '../..';


const OptionFilter: React.FC<DataTableFilterOption> = (props:DataTableFilterOption) => {

    const [opterator, setOperator] = useState<FilterOperator>('IN'); 

    return (
        <div style={{display:'flex'}}>
            <Form.Item
                style={{width:'50%'}}                
                name={[props.fieldKey!!, "options"]} fieldKey={[props.fieldKey!!, "options"]} initialValue={opterator}>
                <Select  placeholder="Filter Option"
                    onChange={(val:FilterOperator)=>setOperator(val)} >
                    <Select.Option value="IN">IN</Select.Option>
                    <Select.Option value="NOT_IN">Not In</Select.Option>
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
                <Select  placeholder={props.title}>
                    {Object.keys(props.fieldOptions||{}).map((key,index) => 
                        <Select.Option key={index} value={key} >{props.fieldOptions?props.fieldOptions[key]: "" }</Select.Option>
                    )}
                </Select>
            </Form.Item>
        </div>
    )
}


export default OptionFilter;