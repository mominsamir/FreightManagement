import React from 'react';
import { Descriptions } from 'antd';
import styles from './desc.module.less';

interface Props {
  title: string;
  className?: string;
}

const Desc: React.FunctionComponent<Props> = ({ title, children }) => {
  return (
    <Descriptions className={styles.descriptions} size="small" title={title} bordered column={{ sm: 2, xs: 1 }}>
      {children}
    </Descriptions>
  );
};
export const DescItem = Descriptions.Item;

export default Desc;
