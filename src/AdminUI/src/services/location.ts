import fetchApi, { ApiResponse } from 'Utils/fetchApi';
import { Location, LocationTank } from 'types/location';
import { PaginatedSearchResult, SearchParams } from 'types/dataTable';
import { jsonToLocation } from 'types/location';

const BASE_URL = '/api/Location'; 

const find = async (id: number): Promise<Location> => {
    let apiResp: any = await fetchApi.get(`${BASE_URL}/${id}`);
    return jsonToLocation(apiResp.model);
};


const search = async function (searchParams: SearchParams): Promise<PaginatedSearchResult<Location>> {
  let response: any = await fetchApi.post(`${BASE_URL}/search`, searchParams);
  return {
    recordsTotal: response.totalCount as number,
    draw: 1,
    recordsFiltered: response.totalCount as number,
    data: response.items.map((bt: Location) => jsonToLocation(bt)),
  };
};

const create = async (location: Location): Promise<ApiResponse> => {
  let resp = await fetchApi.post(`${BASE_URL}/`, location);
  return resp as ApiResponse;
};

const update = async (id: number, location: Location): Promise<ApiResponse> => {
    let resp = await fetchApi.put(`${BASE_URL}/${id}`, location);
    return resp as ApiResponse;
};

const addTank = async (id: number, tank: LocationTank): Promise<ApiResponse> => {
    let resp = await fetchApi.put(`${BASE_URL}/${id}`, tank);
    return resp as ApiResponse;
};

const removeTank = async (id: number, tankId: number): Promise<boolean> => {
    await fetchApi.delete(`${BASE_URL}/${id}/tank/${tankId}`, {});
    return true;
};

const activateLocation = async (id: number): Promise<ApiResponse> => {
    let resp = await fetchApi.put(`${BASE_URL}/${id}/activate`, {});
    return resp as ApiResponse;
};

const deactivateLocation = async (id: number): Promise<ApiResponse> => {
    let resp = await fetchApi.put(`${BASE_URL}/${id}/deactivate`, {});
    return resp as ApiResponse;
};


const locationService = {
    find,
    create,
    update,
    search,
    addTank,
    removeTank,
    activateLocation,
    deactivateLocation
}

export default locationService;