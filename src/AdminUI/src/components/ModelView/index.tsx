import React from 'react';
import {  Col,  PageHeader,  Row } from 'antd';
import styles from './index.module.less';

interface Props {
    title: string,
    subTitle?: string,
    extra?: React.ReactNode,
    children: React.ReactNode
}

const ModelView: React.FC<Props> = ({ title,subTitle, extra, children }: Props) => (
    <Row gutter={8} className={styles.model}>
        <Col span={24} className={styles.header}>
            <PageHeader
                title={title}
                subTitle={subTitle}
                extra={extra}
            />
        </Col>
        <Col span={24} style={{padding:'8px'}}>
            {children}
        </Col>
    </Row>
);

export default ModelView;