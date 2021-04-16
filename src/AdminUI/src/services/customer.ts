import fetchApi, { ApiResponse } from 'Utils/fetchApi';
import { PaginatedSearchResult, SearchParams } from 'types/dataTable';
import { Customer, jsonToCustomer } from 'types/customer';
import { Location } from 'types/location';

const BASE_URL = '/api/Customer'; 

const find = async (id: number): Promise<Customer> => {
    let apiResp: any = await fetchApi.get(`${BASE_URL}/${id}`);
    return jsonToCustomer(apiResp.model);
};

const search = async function (searchParams: SearchParams): Promise<PaginatedSearchResult<Customer>> {
  let response: any = await fetchApi.post(`${BASE_URL}/search`, searchParams);
  return {
    recordsTotal: response.totalCount as number,
    draw: 1,
    recordsFiltered: response.totalCount as number,
    data: response.items.map((bt: any) => jsonToCustomer(bt)),
  };
};

const create = async (customer: Customer): Promise<ApiResponse> => {
  let resp = await fetchApi.post(`${BASE_URL}/`, customer);
  return resp as ApiResponse;
};

const update = async (id: number, customer: Customer): Promise<ApiResponse> => {
    let resp = await fetchApi.put(`${BASE_URL}/${id}`, customer);
    return resp as ApiResponse;
};

const addLocation = async (id: number, location: Location): Promise<ApiResponse> => {
    let resp = await fetchApi.put(`${BASE_URL}/${id}`, location);
    return resp as ApiResponse;
};

const removeLocation = async (id: number, locationId: number): Promise<ApiResponse> => {
    let resp = await fetchApi.put(`${BASE_URL}/${id}/location/${locationId}`, {});
    return resp as ApiResponse;
};

const activateCustomer = async (id: number): Promise<ApiResponse> => {
    let resp = await fetchApi.put(`${BASE_URL}/${id}/activate`, {});
    return resp as ApiResponse;
};

const deactivateCustomer = async (id: number): Promise<ApiResponse> => {
    let resp = await fetchApi.put(`${BASE_URL}/${id}/deactivate`, {});
    return resp as ApiResponse;
};


const fuelProductService = {
    find,
    create,
    update,
    search,
    addLocation,
    removeLocation,
    activateCustomer,
    deactivateCustomer
}

export default fuelProductService;