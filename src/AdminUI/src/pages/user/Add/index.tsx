import React from 'react';
import { Modal } from 'antd';
import { Messages } from 'components';
import UserModel from '../Model';
import { User } from 'types/user';

interface Props {
  user?: User;
  mode: 'EDIT' | 'ADD';
  cancel: () => void;
}

const CreateUpdateUser: React.FC<Props> = ({ user, mode, cancel }: Props) => {

  return (
    <Modal
      visible={true}
      title={mode === 'ADD'? 'Add User' : 'Edit User' }
      onCancel={cancel}
      width="80%"
      footer={null}
      maskClosable={false}>
      <Messages />
      <UserModel user={user} mode={mode} onOk={cancel} onCancel={cancel} />
    </Modal>
  );
};

export default CreateUpdateUser;