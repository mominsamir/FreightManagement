import React from 'react';
import { useSelector } from 'react-redux';
import { Alert } from 'antd';
import messagesSelector from 'redux/selectors/messages';
import styles from './messages.module.less';

interface MessagesProps {}
const Messages: React.FC<MessagesProps> = () => {
  const errorMessages = useSelector(messagesSelector.getErrorMessages);
  const infoMessages = useSelector(messagesSelector.getInfoMessages);
  const successMessages = useSelector(messagesSelector.getSuccessMessages);
  const warnMessages = useSelector(messagesSelector.getWarnMessages);

  return (
    <div className={styles.messages}>
      <DisplayMessages type="error" key="error" title="Error" messages={errorMessages} />
      <DisplayMessages type="info" key="info" title="Info" messages={infoMessages} />
      <DisplayMessages type="success" key="success" title="Success" messages={successMessages} />
      <DisplayMessages type="warning" key="warning" title="Warning" messages={warnMessages} />
    </div>
  );
};

export default Messages;

type MessageType = 'error' | 'info' | 'success' | 'warning';

interface DisplayMessageProps {
  type: MessageType;
  title: string;
  messages: Array<string>;
}
const DisplayMessages: React.FC<DisplayMessageProps> = ({ type, messages, title }) => {
  if (messages.length === 0) {
    return <div></div>;
  }
  const dispMessages = (
    <ul className={styles.messageList}>
      {messages.map((m, idx) => (
        <li key={idx}>{m}</li>
      ))}
    </ul>
  );
  return <Alert banner type={type} closable showIcon message={title} description={dispMessages} className={styles.displayMessage} />;
};
