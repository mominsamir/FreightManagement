import { displayValidationErrors, getGlobalErrors } from './validationHelper';
import { addError } from 'redux/slices/messages';
import { FormInstance } from 'antd/lib/form';
import { ApiError } from 'Utils/fetchApi';
import { AppDispatch } from 'redux/store';
import { History } from 'history';

const handleError = (err: ApiError) => {
  console.error(err);
  switch (err.status) {
    case 401:
      return '/login';
    case 403:
      return '/not-authorized';
    case 404:
      return '/page-not-found';
    case 400:
      return null;
    default:
      return '/unknown-error';
  }
};

const handleErrorAndRedirect = (err: ApiError, history: History) => {
  let url = handleError(err);
  if (url) {
    history.push(url);
  }
  return err.status;
};

const handleFormSubmitError = (form: FormInstance, err: ApiError, history: History) => {
  let status = handleErrorAndRedirect(err, history);
  if (status === 400 && err.response && typeof err.response !== 'string') {
    return displayValidationErrors(form, err.response);
  } else {
    return [];
  }
};

const handleErrors = (err: ApiError, history: History, dispatch: AppDispatch, form?: FormInstance) => {
  let globalErrors;
      if (form) {
        globalErrors = handleFormSubmitError(form, err, history);
      } else {
        let status = handleErrorAndRedirect(err, history);
        if (status === 400) {
          globalErrors = getGlobalErrors(err);
        }
      }

  if (globalErrors && globalErrors.length > 0) dispatch(addError(globalErrors));
};

export { handleErrors };
