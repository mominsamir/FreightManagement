import fetchApi, { ApiResponse } from 'Utils/fetchApi';
import { SearchParams, PaginatedSearchResult } from 'types/dataTable';
import { ChangePassword, jsonToUser,  User } from 'types/user';

const BASE_URL = '/api/User';

const search = async function (searchParams: SearchParams): Promise<PaginatedSearchResult<User>> {
  let response: any = await fetchApi.post(`${BASE_URL}/search`, searchParams);
  return {
    recordsTotal: response.totalCount as number,
    draw: 1,
    recordsFiltered: response.totalCount as number,
    data: response.items.map((bt: any) => jsonToUser(bt)),
  };
};

const find = async (id: number): Promise<User> => {
  let apiResp: any = await fetchApi.get(`${BASE_URL}/${id}`);
  return apiResp.model as User;
};

const findDriverByName = async (name: string): Promise<User[]> => {
  let apiResp: any = await fetchApi.get(`${BASE_URL}/${name}/drivers`);
  return apiResp.items.map((driver:User) => driver);
};


const create = async (vendor: User): Promise<ApiResponse> => {
let resp = await fetchApi.post(`${BASE_URL}/`, vendor);
return resp as ApiResponse;
};

const update = async (id: number, vendor: User): Promise<ApiResponse> => {
  let resp = await fetchApi.put(`${BASE_URL}/${id}`, vendor);
  return resp as ApiResponse;
};


const markActive = async (id: number): Promise<ApiResponse> => {
  let resp = await fetchApi.put(`${BASE_URL}/${id}/activate`, {});
  return resp as ApiResponse;
};

const markInActive = async (id: number): Promise<ApiResponse> => {
  let resp = await fetchApi.put(`${BASE_URL}/${id}/inactivate`, {});
  return resp as ApiResponse;
};

const changePassword = async (id: number, changePassword: ChangePassword): Promise<ApiResponse> => {
  let resp = await fetchApi.put(`${BASE_URL}/${id}/resetpassword`, changePassword);
  return resp as ApiResponse;
};


const userService = {
    search,
    find,
    create,
    update,
    markActive,
    markInActive,
    changePassword,
    findDriverByName
}

export default userService;