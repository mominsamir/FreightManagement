import fetchApi, { ApiResponse } from 'Utils/fetchApi';
import { FuelProduct } from 'types/product';
import { PaginatedSearchResult, SearchParams } from 'types/dataTable';

const BASE_URL = '/api/FuelProduct'; 

const find = async (id: number): Promise<FuelProduct> => {
    let apiResp: any = await fetchApi.get(`${BASE_URL}/${id}`);
    return apiResp.model as FuelProduct;
};


const search = async function (searchParams: SearchParams): Promise<PaginatedSearchResult<FuelProduct>> {
  let response: any = await fetchApi.post(`${BASE_URL}/search`, searchParams);
  return {
    recordsTotal: response.totalCount as number,
    draw: 1,
    recordsFiltered: response.totalCount as number,
    data: response.items.map((bt: FuelProduct) => bt),
  };
};

const create = async (fuelProduct: FuelProduct): Promise<ApiResponse> => {
  let resp = await fetchApi.post(`${BASE_URL}/`, fuelProduct);
  return resp as ApiResponse;
};

const update = async (id: number, fuelProduct: FuelProduct): Promise<ApiResponse> => {
    let resp = await fetchApi.put(`${BASE_URL}/${id}`, fuelProduct);
    return resp as ApiResponse;
  };


const fuelProductService = {
    find,
    create,
    update,
    search
}

export default fuelProductService;