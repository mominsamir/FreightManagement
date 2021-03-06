import React from 'react';
import { Spin } from 'antd';
import { LoadingOutlined } from '@ant-design/icons';
import styles from './globalSpinner.module.less';

const GlobalSpinner: React.FC = () => {
  const antIcon = <LoadingOutlined spin />;
  return (
    <div className={styles.globalSpinner}>
      <Spin className={styles.icon} indicator={antIcon} />
      <p className={styles.loadingText}>Processing...</p>
    </div>
  );
};

export default GlobalSpinner;
