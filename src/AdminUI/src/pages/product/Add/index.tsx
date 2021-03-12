import React from 'react';
import { Modal } from 'antd';
import { Messages } from 'components';
import { FuelProduct } from 'types/product';
import FuelProductModel from '../Model';

interface Props {
  fuelProduct?: FuelProduct;
  mode: 'EDIT' | 'ADD';
  cancel: () => void;
}

const CreateUpdateFuelProduct: React.FC<Props> = ({ fuelProduct, mode, cancel }: Props) => {

  return (
    <Modal
      visible={true}
      title={mode === 'ADD'? 'Add Fuel Product' : 'Edit Fuel Product' }
      onCancel={cancel}
      width="80%"
      footer={null}
      maskClosable={false}>
      <Messages />
      <FuelProductModel fuelProduct={fuelProduct} mode={mode} onOk={cancel} onCancel={cancel} />
    </Modal>
  );
};

export default CreateUpdateFuelProduct;