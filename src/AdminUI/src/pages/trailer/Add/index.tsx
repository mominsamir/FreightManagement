import React from 'react';
import { Modal } from 'antd';
import { Messages } from 'components';
import TrailerModel from '../Model';
import { Trailer } from 'types/vehicle';

interface Props {
  trailer?: Trailer;
  mode: 'EDIT' | 'ADD';
  cancel: () => void;
}

const CreateUpdateTrailer: React.FC<Props> = ({ trailer, mode, cancel }: Props) => {

  return (
    <Modal
      visible={true}
      title={mode === 'ADD'? 'Add New Trailer' : 'Edit Trailer' }
      onCancel={cancel}
      width="80%"
      footer={null}
      maskClosable={false}>
      <Messages />
      <TrailerModel trailer={trailer} mode={mode} onOk={cancel} onCancel={cancel} />
    </Modal>
  );
};

export default CreateUpdateTrailer;