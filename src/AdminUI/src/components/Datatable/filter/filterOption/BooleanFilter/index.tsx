import { Form, Radio } from 'antd';
import React from 'react';
import { DataTableFilterOption } from '../..';

const BooleanFilter: React.FC<DataTableFilterOption> = props => {

 
  const options = [
    { label: 'Yes', value: true },
    { label: 'No', value: false},
  ];

    return (
      <div style={{display:'flex'}}>
          <Form.Item 
            style={{width:'50%'}}
            name={[props.fieldKey!!, "options"]} 
            fieldKey={[props.fieldKey!!, "options"]} 
            label={props.title}  initialValue='EQUAL'>
            <input type="hidden"/>
          </Form.Item>
          <Form.Item  name={[props.fieldKey!!, "name"]} fieldKey={[props.fieldKey!!, "name"]} hidden={true} initialValue={props.field}>
            <input type="hidden"/>
          </Form.Item>              
          <Form.Item 
            style={{width:'50%'}}
            name={[props.fieldKey!!, "value"]} fieldKey={[props.fieldKey!!, "value"]} >
              <Radio.Group
                  options={options}
                  optionType="button"
                  buttonStyle="solid"
                />
          </Form.Item>
      </div>            
    )
}


export default BooleanFilter;