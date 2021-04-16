import React, { useEffect, useState } from 'react';
import {  Button, Col,  Row } from 'antd';
import { Messages } from 'components';
import { Link, useHistory, useParams } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { handleErrors } from 'Utils/errorHandler';
import { AppDispatch } from 'redux/store';
import locationService from 'services/location';
import { Location } from 'types/location';
import { LocationModel } from '../Add';
import ModelView from 'components/ModelView';
import { SearchOutlined, PlusOutlined, TableOutlined    } from '@ant-design/icons';


interface EditLocationProps {
    id: string;  
}

const UpdateLocation: React.FC = () => {
    const[location, setLocation] = useState<Location>();
    const props  = useParams<EditLocationProps>();
    const history = useHistory();
    const dispatch = useDispatch<AppDispatch>();  

    useEffect(() => {
        (async () => {
            try {
                let id = parseInt(props.id);
                var ds = await locationService.find(id);
                setLocation(ds);
            } catch (error) {
                handleErrors(error, history, dispatch);
            }
        })();
    }, [dispatch, history,props]);

    const buttons = [
        <Link to={`/dispatch/locations/add`}>
        <Button icon={<PlusOutlined />}>Add new</Button>
        </Link>,
        <Link to={`/dispatch/locations/${props.id}`}>
            <Button icon={<SearchOutlined />}>View</Button>
        </Link>,
        <Link to={`/dispatch/locations`}>
            <Button icon={<TableOutlined />}>All</Button>
        </Link>
      ]
    
    return (
        <ModelView title="Update Location" extra={buttons}>
            <Row gutter={8} >
                <Col span={24}>
                    <Messages />
                    {location && 
                        <LocationModel location={location} mode="EDIT" />
                    }
                </Col>
            </Row>
        </ModelView>
    );
}


export default UpdateLocation;