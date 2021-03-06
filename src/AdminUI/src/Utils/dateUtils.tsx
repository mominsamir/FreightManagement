import moment from 'moment-timezone';
import _ from 'lodash';
import { Moment } from 'moment-timezone';

const toDateString = (d?: Moment) => (!_.isNil(d) ? moment.tz(d, 'America/Toronto').format('MM-DD-YYYY') : d);
const toDateTimeString = (d?: Moment, format = 'MM-DD-YYYY HH:mm:ss') =>
  !_.isNil(d) ? moment.tz(d, 'America/Toronto').format(format) : d;
const toTimeString = (d?: Moment, format = 'HH:mm') => (!_.isNil(d) ? moment.tz(d, 'America/Toronto').format(format) : d);
const toMomentDate = (str?: string) => (str && str.length > 0 ? moment(str, 'MM-DD-YYYY') : null);
const toMomentDateTime = (str?: string) => (str && str.length > 0 ? moment(str, 'MM-DD-YYYY HH:mm:ss') : null);
const toMomentTime = (str?: string) => (str && str.length > 0 ? moment(str, 'HH:mm') : null);
const strToTimeStr = (str?: string) => {
  let time = toMomentTime(str);
  return time && time.format('hh:mm a');
};

export { toDateString, toDateTimeString, toTimeString, toMomentDateTime, toMomentDate, toMomentTime, strToTimeStr };
