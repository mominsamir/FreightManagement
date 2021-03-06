import React from 'react';
import { Result, Button } from 'antd';
import { useHistory } from 'react-router-dom';

const UnknownError: React.FC = () => {
  let history = useHistory();
  return (
    <Result
      status="500"
      title="500"
      style={{
        background: 'none',
        maxWidth: '500px',
        margin: '0 auto',
      }}
      subTitle="Sorry. Some error occured while processing your request. If you continue to face problem, kindly contact
      support person and inform him about the request you are trying to process."
      extra={
        <Button type="primary" onClick={() => history.push('/')}>
          Back to home
        </Button>
      }
    />
  );
};

export default UnknownError;