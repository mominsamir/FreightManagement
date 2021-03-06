import { Layout,Card, Col, Row, Statistic, PageHeader, DatePicker } from 'antd';
import React, {  useState } from 'react';
import { ShoppingCartOutlined, CreditCardOutlined, BookOutlined } from '@ant-design/icons';
import styles from './Homepage.module.less';
import moment, {Moment} from 'moment';

const App: React.FC = () => {
  
  const { RangePicker } = DatePicker;
  const dateFormat = 'MM-DD-YYYY';

  const [startDate, setStartDate] = useState<Moment>(moment().startOf('month'));
  const [endDate,   setEndDate] = useState<Moment>(moment().endOf('month'));


  const dateTimeChanged = (values:any,str:[string, string]): void =>{
    setStartDate(values[0]);
    setEndDate(values[1]);
  }

  

  return (
    <Layout className={styles.main}>
        <PageHeader
          key="homePageHeader"
          className="site-page-header"
          title="Dashboard"
          extra={[
            <RangePicker
              key="dashboardPicker"
              allowEmpty={[false,false]}
              defaultValue={[startDate, endDate]}
              format={dateFormat}
              clearIcon={false}
              onChange={dateTimeChanged}>
            </RangePicker>  ,
          ]}
        />
      <Row gutter={6} style={{padding:8}}>
        <Col span={16}>
          <Row gutter={6}>
            <Col span={6}>
              <Card>
                <Layout style={{padding:8}}>
                  <Statistic title="Order Count"  value={0}  prefix={<ShoppingCartOutlined />} />
                </Layout>
              </Card>  
            </Col>
            <Col span={6}>
              <Card>
                <Layout style={{padding:8}}>
                  <Statistic title="Quantity Ordered" value={0}  prefix={<ShoppingCartOutlined />}/>
                </Layout>
              </Card>
            </Col>
            <Col span={6}>
              <Card>
                <Layout style={{padding:8}}>  
                  <Statistic title="Credit Cards" value={0} precision={2} prefix={<CreditCardOutlined />} />
                </Layout>  
              </Card>  
            </Col>
            <Col span={6}>
              <Card>
                <Layout style={{padding:8}}>
                  <Statistic title="Total Invoices" value={0} precision={2} prefix={<BookOutlined />} />
                </Layout>
              </Card>
            </Col>          
          </Row>
          <Row gutter={6}>
            <Col span={24}>
            </Col>
          </Row>
        </Col>
        <Col span={8}>

        </Col>
      </Row>
    </Layout>
  );
};

export default App;
