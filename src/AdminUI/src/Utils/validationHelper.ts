import { FormInstance } from 'antd/lib/form';
import { FieldData } from 'rc-field-form/lib/interface';
import { ApiError } from 'Utils/fetchApi';


export interface ValidationError {
  title:  string;
  status: number;
  errors: Record<string, string[]>;
}

const GLOBAL_ERRORS = "Errors";

const displayValidationErrors = (form: FormInstance, payload: ValidationError): Array<string> => {
  let errors = flattenObject(payload.errors);
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
  return payload.errors[GLOBAL_ERRORS];
};

const getGlobalErrors = (err: ApiError): Array<string> => {
  if (!err.response || typeof err.response === 'string') {
    return [];
  } else {
    let globalErrors = err.response[GLOBAL_ERRORS];
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
