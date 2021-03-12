import React from 'react';
import { Modal } from 'antd';
import { Messages } from 'components';
import { Rack } from 'types/rack';
import RackModel from '../Model';

interface Props {
  rack?: Rack;
  mode: 'EDIT' | 'ADD';
  cancel: () => void;
}

const CreateUpdateRackModel: React.FC<Props> = ({ rack, mode, cancel }: Props) => {

  return (
    <Modal
      visible={true}
      title={mode === 'ADD'? 'Add Fuel Product' : 'Edit Fuel Product' }
      onCancel={cancel}
      width="80%"
      footer={null}
      maskClosable={false}>
      <Messages />
      <RackModel rack={rack} mode={mode} onOk={cancel} onCancel={cancel} />
    </Modal>
  );
};

export default CreateUpdateRackModel;