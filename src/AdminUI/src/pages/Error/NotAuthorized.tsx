import React from 'react';
import { Result, Button } from 'antd';
import { useHistory } from 'react-router-dom';

const NotAuthorized: React.FC = () => {
  let history = useHistory();
  return (
    <Result
      status="403"
      title="403"
      style={{
        background: 'none',
        maxWidth: '500px',
        margin: '0 auto',
      }}
      subTitle="Sorry. You are not authorized to perform this action."
      extra={
        <Button type="primary" onClick={() => history.push('/')}>
          Back to home
        </Button>
      }
    />
  );
};

export default NotAuthorized;
