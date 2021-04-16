import fetchApi, { ApiResponse } from 'Utils/fetchApi';
import { jsonToTruck, Truck } from 'types/vehicle';
import { PaginatedSearchResult, SearchParams } from 'types/dataTable';

const BASE_URL = '/api/Truck'; 

const find = async (id: number): Promise<Truck> => {
    let apiResp: any = await fetchApi.get(`${BASE_URL}/${id}`);
    return jsonToTruck(apiResp.model);
};

const search = async function (searchParams: SearchParams): Promise<PaginatedSearchResult<Truck>> {
    let response: any = await fetchApi.post(`${BASE_URL}/search`, searchParams);
    return {
      recordsTotal: response.totalCount as number,
      draw: 1,
      recordsFiltered: response.totalCount as number,
      data: response.items.map((bt: Truck) => bt),
    };
  };

const findTruckByNumber = async (name: string): Promise<Truck[]> => {
    let apiResp: any = await fetchApi.get(`${BASE_URL}/${name}/number`);
    return apiResp.items.map((driver:Truck) => driver);
};


const create = async (truck: Truck): Promise<ApiResponse> => {
  let resp = await fetchApi.post(`${BASE_URL}/`, truck);
  return resp as ApiResponse;
};

const update = async (id: number, truck: Truck): Promise<ApiResponse> => {
    let resp = await fetchApi.put(`${BASE_URL}/${id}`, truck);
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


const truckService = {
    find,
    search,
    create,
    update,
    markUnderMaintance,
    markOutOfService,
    markActive,
    findTruckByNumber
}

export default truckService;