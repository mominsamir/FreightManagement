import fetchApi, { ApiResponse } from 'Utils/fetchApi';
import { PaginatedSearchResult, SearchParams } from 'types/dataTable';
import { jsonToOrder, Order, OrderModel } from 'types/order';

const BASE_URL = '/api/Order'; 

const find = async (id: number): Promise<Order> => {
    let apiResp: any = await fetchApi.get(`${BASE_URL}/${id}`);
    let order =jsonToOrder(apiResp.model);
    console.log(order); 
    return  order
};

const search = async function (searchParams: SearchParams): Promise<PaginatedSearchResult<Order>> {
  let response: any = await fetchApi.post(`${BASE_URL}/search`, searchParams);
  return {
    recordsTotal: response.totalCount as number,
    draw: 1,
    recordsFiltered: response.totalCount as number,
    data: response.items.map((bt: any) => jsonToOrder(bt)),
  };
};

const create = async (order: Order): Promise<ApiResponse> => {
  let resp = await fetchApi.post(`${BASE_URL}/`, order);
  return resp as ApiResponse;
};

const update = async (id: number, order: OrderModel): Promise<ApiResponse> => {
    let resp = await fetchApi.put(`${BASE_URL}/${id}`, order);
    return resp as ApiResponse;
};

const cancelOrder = async (id: number): Promise<ApiResponse> => {
  let resp = await fetchApi.put(`${BASE_URL}/${id}/cancel`, {});
  return resp as ApiResponse;
};


const fuelProductService = {
    find,
    create,
    update,
    search,
    cancelOrder
}

export default fuelProductService;