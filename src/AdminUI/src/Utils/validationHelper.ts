import { FormInstance } from 'antd/lib/form';
import { FieldData } from 'rc-field-form/lib/interface';
import { ApiError } from 'Utils/fetchApi';

export interface ValidationError {
  error: number;
  validationErrors: number;
  errorMessages: Record<string, any>;
}

const GLOBAL_ERRORS = 'errors';

const displayValidationErrors = (form: FormInstance, payload: ValidationError): Array<string> => {
  let errors = flattenObject(payload.validationErrors);
  let fields: Array<FieldData> = [];
  Object.keys(errors).forEach((key) => {
    let obj: FieldData = {
      name: key,
      value: form.getFieldValue(key),
      errors: errors[key],
    };
    fields.push(obj);
  });
  if (fields.length > 0) form.setFields(fields);
  return payload.errorMessages[GLOBAL_ERRORS];
};

const getGlobalErrors = (err: ApiError): Array<string> => {
  if (!err.response || typeof err.response === 'string') {
    return [];
  } else {
    let globalErrors = err.response.errorMessages[GLOBAL_ERRORS];
    return Array.isArray(globalErrors) ? globalErrors : [];
  }
};

const flattenObject = (obj: any) => {
  var toReturn: any = {};

  for (var i in obj) {
    if (!obj.hasOwnProperty(i)) continue;

    if (typeof obj[i] == 'object' && obj[i] !== null && !Array.isArray(obj[i])) {
      var flatObject = flattenObject(obj[i]);
      for (var x in flatObject) {
        if (!flatObject.hasOwnProperty(x)) continue;

        toReturn[i + '.' + x] = flatObject[x];
      }
    } else {
      toReturn[i] = obj[i];
    }
  }
  return toReturn;
};

export { displayValidationErrors, getGlobalErrors };
