import React from 'react';
import moment from 'moment';
import { Desc, DescItem } from 'components';
import { toDateTimeString } from 'Utils/dateUtils';
import styles from './audit.module.less';
import { User } from 'types/user';

interface Props {
  createdBy?: User;
  updatedBy?: User;
  createdOn: moment.Moment;
  updatedOn: moment.Moment;
}


const AuditData: React.FC<Props> = ({ createdOn, createdBy, updatedOn, updatedBy }) => {
  return (
    <Desc title="Audit Information" className={styles.auditData}>
      <DescItem label="Created By">{createdBy && `${createdBy.firstName} ${createdBy.lastName}`}</DescItem>
      <DescItem label="Created On">{toDateTimeString(createdOn)}</DescItem>
      <DescItem label="Updated By">{updatedBy && `${updatedBy.firstName} ${updatedBy.lastName}`}</DescItem>
      <DescItem label="Updated On">{toDateTimeString(updatedOn)}</DescItem>
    </Desc>
  );
};

export default AuditData;
