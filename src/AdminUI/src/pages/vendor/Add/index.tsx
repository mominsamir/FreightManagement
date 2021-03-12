import React from 'react';
import { Modal } from 'antd';
import { Messages } from 'components';
import { Vendor } from 'types/vendor';
import VendorModel from '../Model';

interface Props {
  vendor?: Vendor;
  mode: 'EDIT' | 'ADD';
  cancel: () => void;
}

const CreateUpdateVendorModel: React.FC<Props> = ({ vendor, mode, cancel }: Props) => {

  return (
    <Modal
      visible={true}
      title={mode === 'ADD'? 'Add New Vendor' : 'Edit Vendor' }
      onCancel={cancel}
      width="80%"
      footer={null}
      maskClosable={false}>
      <Messages />
      <VendorModel vendor={vendor} mode={mode} onOk={cancel} onCancel={cancel} />
    </Modal>
  );
};

export default CreateUpdateVendorModel;