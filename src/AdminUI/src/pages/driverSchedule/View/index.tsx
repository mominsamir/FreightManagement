import React, { useEffect, useState } from 'react';
import { Button, Checkbox, Col, Descriptions, Modal, Row, Table} from 'antd';
import { Messages } from 'components';
import { Link, useHistory} from 'react-router-dom';
import {useParams} from 'react-router';
import { useDispatch } from 'react-redux';
import { AppDispatch } from 'redux/store';
import { DriverSchedule, DriverScheduleCheckList, UpdateDriverScheduleCheckList } from 'types/driverSchedule';
import  driverScheduleService  from 'services/driverSchedule';
import { handleErrors } from 'Utils/errorHandler';
import { toDateTimeString } from 'Utils/dateUtils';
import { Column } from 'types/dataTable';
import ModelView from 'components/ModelView';
import { PlusOutlined, EditOutlined, TableOutlined } from '@ant-design/icons';
import CreateSchedule from '../Add';

interface ViewScheduleProps {
  id: string;  
}

const { Item } = Descriptions;    


const ViewSchedule: React.FC<ViewScheduleProps> = () => {

    const[schedule, setSchedule] = useState<DriverSchedule>();
    const[realod, setReload] = useState<boolean>(true);
    const[showCreateModel, setShowCreateModel] = useState<boolean>(false);
    const [applyTo, setApplyTo] = useState<Map<number, DriverScheduleCheckList>>(new Map());
    const props  = useParams<ViewScheduleProps>();
    const history = useHistory();
    const dispatch = useDispatch<AppDispatch>();  
  

    useEffect(() => {
        (async () => {
          try {
                let id = parseInt(props.id);
                var ds = await driverScheduleService.find(id);
                setSchedule(ds);
          } catch (error) {
            handleErrors(error, history, dispatch);
          }
        })();
      }, [dispatch, history,props,realod]);

    const  checkBoxChecked = (checked: boolean, item: DriverScheduleCheckList) => {
      let current = new Map(applyTo);
      if (checked) {
        item.isChecked = true;
        current.set(item.id, item);
      } else {
        current.delete(item.id);
      }
      setApplyTo(current);
    }

    const onCheckListSinged =() => {
      if (!schedule) {
        Modal.error({
          content: 'Please select a store and a date range.',
        });
        return;
      }      
      (async () => {
        try {
            var obj: UpdateDriverScheduleCheckList = { 
              id: schedule?.id,
              checkListItems:  Array.from(applyTo.values())
            } 
            await driverScheduleService.updateCheckList(schedule === undefined ? 0 : schedule?.id,obj);
            applyTo.clear();
            setReload(!realod);
        } catch (error) {
          handleErrors(error, history, dispatch);
        }
      })();
    }

    const columns: Column[]= [
        {
          title: 'Item',
          dataIndex: 'note',
          key: 'note'
        },
        {
          title: 'Checked',
          key: 'isChecked',
          dataIndex: 'isChecked',
          render: (isChecked: boolean, row: DriverScheduleCheckList) => (
            <>
              {!isChecked?
                <Checkbox defaultChecked={isChecked} onChange={(e)=> checkBoxChecked( e.target.checked, row)}></Checkbox>
                : <span>Yes</span>
              }
            </>
          ),
        }
      ];      

      const buttons = [
        <Button icon={<PlusOutlined />} onClick={()=>toogleModel()}>Add new</Button>,
        <Link to={`/dispatch/schedules`}>
            <Button icon={<TableOutlined />}>All</Button>
        </Link>
      ]      

    const toogleModel = () => {
        setShowCreateModel(!showCreateModel);
    };

    return (
      <ModelView title="Driver Schedule" extra={buttons}>
        <Row gutter={8}>
            <Col span={24}>
                <Messages />
            </Col>
            <Col span={24}>
                <Descriptions bordered>
                    <Item label="Driver">{schedule?.driver.firstName} {schedule?.driver.lastName}</Item>
                    <Item label="Truck">{schedule?.truck.numberPlate}</Item>
                    <Item label="Truck Status">{schedule?.truck.status}</Item>
                    <Item label="Trailer">{schedule?.trailer.numberPlate}</Item>
                    <Item label="Trailer Status">{schedule?.trailer.status}</Item>                    
                    <Item label="Start Time">{toDateTimeString(schedule?.startTime)}</Item>
                    <Item label="End Time">{toDateTimeString(schedule?.endTime)}</Item>
                    <Item label="Schedule Status">{schedule?.status}</Item>
                </Descriptions>
            </Col>
            <Col span={24}>
                <Table 
                  title={()=> (<h4>Check List</h4> )}
                  columns={columns} 
                  rowKey={row=> row.id}
                  pagination={false}
                  dataSource={schedule?.checkList}
                  footer={() => (
                    <div  style={{float: 'right'}}>
                      <Button type="primary" onClick={()=> onCheckListSinged()}>Complete Checklist</Button>
                    </div>
                  )}>
                </Table>              
            </Col>  
            {showCreateModel &&
                (<Col>
                  <CreateSchedule 
                      cancel={toogleModel}>
                  </CreateSchedule>
                </Col>)}                

        </Row>
      </ModelView>        
    );
};




export default ViewSchedule;

