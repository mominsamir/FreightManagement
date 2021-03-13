import fetchApi, { ApiResponse } from 'Utils/fetchApi';
import { jsonToVendor, Vendor } from 'types/vendor';
import { PaginatedSearchResult, SearchParams } from 'types/dataTable';

const BASE_URL = '/api/Vendor'; 

const find = async (id: number): Promise<Vendor> => {
    let apiResp = await fetchApi.get(`${BASE_URL}/${id}`);
    return jsonToVendor(apiResp);
};
  
const search = async function (searchParams: SearchParams): Promise<PaginatedSearchResult<Vendor>> {
  let response: any = await fetchApi.post(`${BASE_URL}/search`, searchParams);
  return {
    recordsTotal: response.totalCount as number,
    draw: 1,
    recordsFiltered: response.totalCount as number,
    data: response.items.map((bt: any) => jsonToVendor(bt)),
  };
};


const create = async (vendor: Vendor): Promise<ApiResponse> => {
  let resp = await fetchApi.post(`${BASE_URL}/`, vendor);
  return resp as ApiResponse;
};

const update = async (id: number, vendor: Vendor): Promise<ApiResponse> => {
    let resp = await fetchApi.put(`${BASE_URL}/${id}`, vendor);
    return resp as ApiResponse;
  };


const vendorService = {
    find,
    search,
    create,
    update
}

export default vendorService;