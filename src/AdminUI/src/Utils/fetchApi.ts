import Cookies from 'js-cookie';
import store from 'redux/store';
import * as apiRequest from 'redux/slices/apiRequest';
import { ValidationError } from 'Utils/validationHelper';
import download from 'downloadjs';

export type ApiResponse = Record<any, any> | Array<any>;

const fetchGet = function (url: string) {
  store.dispatch(apiRequest.start());
  return fetch(url, {
    method: 'GET',
    cache: 'no-cache',
    headers: {
      'Authorization': Cookies.get('TOKEN') || '',
    }
  })
    .then((res) => handleErrors(res))
    .then((res) => fetchResponseHandler(res));
};

const fetchPost = function (url: string, payload: object) {
  store.dispatch(apiRequest.start());
  return fetch(url, {
    method: 'POST',
    cache: 'no-cache',
    headers: {
      'Content-Type': 'application/json',      
      'Authorization': Cookies.get('TOKEN') || '',
    },
    body: JSON.stringify(payload),
  })
    .then((res) => handleErrors(res))
    .then((res) => fetchResponseHandler(res));
};

const fetchPostFormData = function (url: string, payload: FormData) {
  store.dispatch(apiRequest.start());
  fetch(url, {
    method: 'POST',
    cache: 'no-cache',
    headers: {
      'Authorization': Cookies.get('TOKEN') || '',
    },
    body: payload,
  })
    .then((res) => handleErrors(res))
    .then((res) => fetchResponseHandler(res));
   
};

const fetchPut = function (url: string, payload: object) {
  store.dispatch(apiRequest.start());
  return fetch(url, {
    method: 'PUT',
    cache: 'no-cache',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': Cookies.get('TOKEN') || '',
    },
    body: JSON.stringify(payload),
  })
    .then((res) => handleErrors(res))
    .then((res) => fetchResponseHandler(res));
};

const fetchDelete = function (url: string, payload: object) {
  store.dispatch(apiRequest.start());
  return fetch(url, {
    method: 'DELETE',
    cache: 'no-cache',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': Cookies.get('TOKEN') || '',
    },
    body: JSON.stringify(payload),
  })
    .then((res) => handleErrors(res))
    .then((res) => fetchResponseHandler(res));
};

const fetchPostDownload = async function (url: string, payload: object) {
  store.dispatch(apiRequest.start());
  let res = await fetch(url, {
    method: 'POST',
    cache: 'no-cache',
    headers: {
      'Content-Type': 'application/json',      
      'Authorization': Cookies.get('TOKEN') || '',
      'responseType': 'blob' 
    },
    body: JSON.stringify(payload),
   });
   let fileName = res.headers.get('filename')?.toString();
   let fileType = res.headers.get('Content-Type')?.toString();
 
   let blob = await res.blob();
   store.dispatch(apiRequest.complete());
   download(blob, fileName, fileType);
};

const fileXLSDownload = async function (url: string,  payload: object) {
  store.dispatch(apiRequest.start());
  let res = await fetch(url, {
    method: 'POST',
    cache: 'no-cache',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': Cookies.get('TOKEN') || '',
    },
    body: JSON.stringify(payload),
  });
  let fileName = res.headers.get('filename')?.toString();
  let fileType = res.headers.get('Content-Type')?.toString();

  let blob = await res.blob();
  store.dispatch(apiRequest.complete());
  download(blob, fileName, fileType);
};

const handleErrors = function (res: Response) {
  store.dispatch(apiRequest.complete());
  if (res.status === 401) {
    throw new ApiError(res.status, res.statusText);
  }

  if (res.status !== 200 && res.status !== 201)  {
    return res.json().then((resp) => {
      console.log(resp);
      throw new ApiError(res.status, resp);
    });
  } else {
    return res.text().then(function(text) {
      return text ? JSON.parse(text) : {}
    })    
  }
};

export class ApiError extends Error {
  status: number;
  response?: string | ValidationError;

  constructor(status: number, response: string) {
    super('ApiError');
    this.status = status;
    this.response = response;
  }
}

const fetchResponseHandler = function (res: ApiResponse) {
  return res;
};

const methods = { get: fetchGet, 
  post: fetchPost, 
  put: fetchPut, 
  delete: fetchDelete, 
  fileUpload: fetchPostFormData, 
  download: fetchPostDownload,
  fileXLSDownload:fileXLSDownload };
export default methods;
