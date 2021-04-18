import fetchApi, { ApiResponse } from 'Utils/fetchApi';
import { jsonToRack, Rack } from 'types/rack';
import { PaginatedSearchResult, SearchParams } from 'types/dataTable';
import { FuelProduct } from 'types/product';

const BASE_URL = '/api/Rack'; 

const find = async (id: number): Promise<Rack> => {
    let apiResp: any = await fetchApi.get(`${BASE_URL}/${id}`);
    return jsonToRack(apiResp.model);
};

const search = async function (searchParams: SearchParams): Promise<PaginatedSearchResult<FuelProduct>> {
  let response: any = await fetchApi.post(`${BASE_URL}/search`, searchParams);
  return {
    recordsTotal: response.totalCount as number,
    draw: 1,
    recordsFiltered: response.totalCount as number,
    data: response.items.map((bt: Rack) => jsonToRack(bt)),
  };
};
  

const create = async (rack: Rack): Promise<ApiResponse> => {
  let resp = await fetchApi.post(`${BASE_URL}/`, rack);
  return resp as ApiResponse;
};

const update = async (id: number, rack: Rack): Promise<ApiResponse> => {
    let resp = await fetchApi.put(`${BASE_URL}/${id}`, rack);
    return resp as ApiResponse;
  };


const rackService = {
    find,
    create,
    update,
    search
}

export default rackService;