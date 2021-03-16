import fetchApi, { ApiResponse } from 'Utils/fetchApi';
import { Trailer } from 'types/vehicle';
import { PaginatedSearchResult, SearchParams } from 'types/dataTable';

const BASE_URL = '/api/Trailer'; 

const find = async (id: number): Promise<Trailer> => {
    let apiResp: any = await fetchApi.get(`${BASE_URL}/${id}`);
    return apiResp.model as Trailer;
};

const search = async function (searchParams: SearchParams): Promise<PaginatedSearchResult<Trailer>> {
    let response: any = await fetchApi.post(`${BASE_URL}/search`, searchParams);
    return {
      recordsTotal: response.totalCount as number,
      draw: 1,
      recordsFiltered: response.totalCount as number,
      data: response.items.map((bt: Trailer) => bt),
    };
};

const create = async (triler: Trailer): Promise<ApiResponse> => {
  let resp = await fetchApi.post(`${BASE_URL}/`, triler);
  return resp as ApiResponse;
};

const findTrailerByNumber = async (name: string): Promise<Trailer[]> => {
    let apiResp: any = await fetchApi.get(`${BASE_URL}/${name}/number`);
    return apiResp.items.map((driver:Trailer) => driver);
};

const update = async (id: number, trailer: Trailer): Promise<ApiResponse> => {
    let resp = await fetchApi.put(`${BASE_URL}/${id}`, trailer);
    return resp as ApiResponse;
};

const markUnderMaintance = async (id: number): Promise<ApiResponse> => {
    let resp = await fetchApi.put(`${BASE_URL}/${id}/under_maintanace`, {});
    return resp as ApiResponse;
};

const markOutOfService = async (id: number): Promise<ApiResponse> => {
    let resp = await fetchApi.put(`${BASE_URL}/${id}/out_of_service`, {});
    return resp as ApiResponse;
};

const markActive = async (id: number): Promise<ApiResponse> => {
    let resp = await fetchApi.put(`${BASE_URL}/${id}/activate`, {});
    return resp as ApiResponse;
};

const vehicleService = {
    find,    
    search,
    create,
    update,
    markUnderMaintance,
    markOutOfService,
    markActive,
    findTrailerByNumber
}

export default vehicleService;