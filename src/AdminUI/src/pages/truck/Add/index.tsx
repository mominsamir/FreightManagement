import React from 'react';
import { Modal } from 'antd';
import { Messages } from 'components';
import { Truck } from 'types/vehicle';
import TruckModel from '../Model';

interface Props {
  truck?: Truck;
  mode: 'EDIT' | 'ADD';
  cancel: () => void;
}

const CreateUpdateTruck: React.FC<Props> = ({ truck, mode, cancel }: Props) => {

  return (
    <Modal
      visible={true}
      title={mode === 'ADD'? 'Add New Truck' : 'Edit Truck' }
      onCancel={cancel}
      width="80%"
      footer={null}
      maskClosable={false}>
      <Messages />
      <TruckModel truck={truck} mode={mode} onOk={cancel} onCancel={cancel} />
    </Modal>
  );
};

export default CreateUpdateTruck;