import React, { useEffect, useState } from 'react';
import { Button, Checkbox, Col, Descriptions, Modal, Row, Table} from 'antd';
import { Messages } from 'components';
import { useHistory} from 'react-router-dom';
import {useParams} from 'react-router';
import { useDispatch } from 'react-redux';
import { AppDispatch } from 'redux/store';
import { DriverSchedule, DriverScheduleCheckList, UpdateDriverScheduleCheckList } from 'types/driverSchedule';
import  driverScheduleService  from 'services/driverSchedule';
import { handleErrors } from 'Utils/errorHandler';
import { toDateTimeString } from 'Utils/dateUtils';
import { Column } from 'types/dataTable';

interface ViewScheduleProps {
  id: string;  
}

const { Item } = Descriptions;    


const ViewSchedule: React.FC<ViewScheduleProps> = () => {

    const[schedule, setSchedule] = useState<DriverSchedule>();
    const[realod, setReload] = useState<boolean>(true);
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

    return (
        <Row gutter={8}>
            <Col span={24}>
                <Messages />
            </Col>
            <Col span={24}>
                <Descriptions title="Driver Schedule" bordered>
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
                  title={()=> 'Check List'}
                  columns={columns} 
                  rowKey="id"
                  pagination={false}
                  dataSource={schedule?.checkList}
                  footer={() => (
                    <div  style={{float: 'right'}}>
                      <Button type="primary" onClick={()=> onCheckListSinged()}>Complete Checklist</Button>
                    </div>
                  )}>
                </Table>              
            </Col>            
        </Row>
    );
};




export default ViewSchedule;

