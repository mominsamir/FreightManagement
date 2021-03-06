import React from 'react';
import { Button, Col, Row,Form, Space } from 'antd';
import { FormListFieldData } from 'antd/lib/form/FormList';
import { FilterData } from 'types/dataTable';
import { toDateString, toDateTimeString } from 'Utils/dateUtils';
import filterComponentOptions from './filterOption/index';


export enum FieldType {
    STRING,
    OPTION,
    NUMBER,
    MONEY,
    DATE,
    DATETIME,
    BOOLEAN
}

export interface Props {
    filterOptions:DataTableFilterOption[]
    onFilter: (filter:FilterData[]) => void;
    onClear: () => void;
  }

export interface DataTableFilterOption {
    title?: string;
    field: string;
    fieldType: FieldType;
    filterData?: FilterData
    fieldOptions?: Record<string,string>;
    fieldKey?: number;
    options?: any,
    value?: any
    value1?: any
}

interface FilterFormData{
    filterOptions: DataTableFilterOption[]
}


const DataTableFilter: React.FC<Props> = ({filterOptions, onFilter, onClear}:Props) => {

    const [form] = Form.useForm<FilterFormData>();

    const {BooleanFilter, NumberFilter, DateFilter, StringFilter, OptionFilter, DateTimeFilter } = filterComponentOptions;

    const validateFilter = (values:FilterFormData) =>{
        let arr = Array<FilterData>();
        values.filterOptions.forEach((m:DataTableFilterOption,i:number)=>{
            if(m.value){
                if(m.fieldType===FieldType.DATE){
                    if(m.options === 'BETWEEN'){
                        const strDt = m.value[0];
                        const endDt = m.value[1];
                        arr.push({
                            name: m.field,
                            value: toDateString(strDt)+"|"+ toDateString(endDt),
                            operator: m.options
                        });
                    }else{
                        let val = toDateString(m.value);
                        if(val)
                        arr.push({
                            name: m.field,
                            value: val,
                            operator: m.options
                        });
                    }
                }else if(m.fieldType===FieldType.DATETIME){
                    if(m.options === 'BETWEEN'){
                        const strDt = m.value[0];
                        const endDt = m.value[1];
                        arr.push({
                            name: m.field,
                            value: toDateTimeString(strDt)+"|"+ toDateTimeString(endDt),
                            operator: m.options
                        });
                    }else{
                        let val = toDateTimeString(m.value);
                        if(val)
                        arr.push({
                            name: m.field,
                            value: val,
                            operator: m.options
                        });
                    }
                } else if(m.fieldType===FieldType.NUMBER || m.fieldType===FieldType.MONEY){
                    if(m.options === 'BETWEEN'){
                        arr.push({
                            name: m.field,
                            value: m.value+","+ m.value1,
                            operator: m.options
                        });
                    }else{
                        arr.push({
                            name: m.field,
                            value: m.value+"",
                            operator: m.options
                        });
                    }
                }else{
                    arr.push({
                        name: m.field,
                        value: m.value,
                        operator: m.options
                    })
                }

            } else{
                if(m.options==='IS_EMPTY' || m.options==='NOT_EMPTY'){
                    arr.push({
                        name: m.field,
                        value: "",
                        operator: m.options
                    })
                }
            }     
        })

        if (arr.length !== 0){
            onFilter(arr)
        }
    }

    const getRows = (fields:FormListFieldData[]) => {
        return fields.map((field:FormListFieldData, index:number) =>  {
            const prop = filterOptions[field.key];

            switch (prop.fieldType) {
                case FieldType.BOOLEAN:
                    return <Col key={index} span={4}>
                            <BooleanFilter key={index} title={prop.title} field={prop.field} fieldKey={field.fieldKey} fieldType={prop.fieldType}/>
                        </Col>
                case FieldType.NUMBER:
                    return <Col key={index} span={8}>
                            <NumberFilter key={index} title={prop.title} field={prop.field} fieldKey={field.fieldKey} fieldType={prop.fieldType}/>
                        </Col>
                case FieldType.MONEY:
                    return <Col key={index} span={8}>
                            <NumberFilter key={index} title={prop.title} field={prop.field} fieldKey={field.fieldKey} fieldType={prop.fieldType}/>
                        </Col>
                case FieldType.DATE:
                    return <Col key={index} span={8}>
                            <DateFilter key={index} title={prop.title} field={prop.field} fieldKey={field.fieldKey} fieldType={prop.fieldType}/>
                        </Col>
                case FieldType.DATETIME:
                    return <Col key={index} span={8}>
                            <DateTimeFilter key={index} title={prop.title} field={prop.field} fieldKey={field.fieldKey} fieldType={prop.fieldType}/>
                        </Col>
                case FieldType.OPTION:
                    return <Col key={index} span={8}>
                            <OptionFilter key={index} title={prop.title} field={prop.field} fieldKey={field.fieldKey} fieldType={prop.fieldType}
                                fieldOptions={prop.fieldOptions}/>
                        </Col>                        
            default:
                case FieldType.STRING:
                    return <Col key={index} span={8}>
                            <StringFilter key={index} title={prop.title} field={prop.field} fieldKey={field.fieldKey} fieldType={prop.fieldType}/>
                        </Col>
            }
        });
    }     

    const clearForm = () =>{
        form.resetFields();
        onClear();
    }

    return (
        <Row gutter={8}>
            <Col span={24}>
                <Form form={form} title="Filter Data" name="filter-form"  onFinish={validateFilter} initialValues={{filterOptions:filterOptions}}>
                    <Form.List key="filterOptions" name="filterOptions">
                        {fields => {
                            return (
                                <Row gutter={8}>
                                    {getRows(fields)}
                                </Row>
                            )
                        }}                
                    </Form.List>
                    <Row gutter={8} style={{padding:8,display:'block', textAlign:'right'}}>
                        <Col span={24}>
                            <Space>
                                <Button type="primary" htmlType="submit">Filter</Button>
                                <Button type="dashed" htmlType="reset" onClick={clearForm}>Clear</Button>
                            </Space>
                        </Col>
                    </Row>    
                </Form>
            </Col>
        </Row>
    )
}


export default DataTableFilter;