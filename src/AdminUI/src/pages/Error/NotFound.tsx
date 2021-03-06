import React from 'react';
import { Result, Button } from 'antd';
import { useHistory } from 'react-router-dom';

const NotFound: React.FC = () => {
  let history = useHistory();
  return (
    <Result
      status="404"
      title="404"
      style={{
        background: 'none',
        maxWidth: '500px',
        margin: '0 auto',
      }}
      subTitle="Sorry, the page your are looking for cannot be found."
      extra={
        <Button type="primary" onClick={() => history.push('/')}>
          Back to home
        </Button>
      }
    />
  );
};

export default NotFound;
