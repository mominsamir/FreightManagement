import fetchApi, { ApiResponse } from 'Utils/fetchApi';
import { PaginatedSearchResult, SearchParams } from 'types/dataTable';
import { DriverSchedule, DriverScheduleCreate, DriverScheduleList, jsonToDriverSchedule, jsonToDriverScheduleList, UpdateDriverScheduleCheckList } from 'types/driverSchedule';

const BASE_URL = '/api/DriverSchedule'; 

const find = async (id: number): Promise<DriverSchedule> => {
    let apiResp: any = await fetchApi.get(`${BASE_URL}/${id}`);
    return jsonToDriverSchedule(apiResp.model);
};

const search = async function (searchParams: SearchParams): Promise<PaginatedSearchResult<DriverScheduleList>> {
    let response: any = await fetchApi.post(`${BASE_URL}/search`, searchParams);
    return {
      recordsTotal: response.totalCount as number,
      draw: 1,
      recordsFiltered: response.totalCount as number,
      data: response.items.map((bt: any) => jsonToDriverScheduleList(bt)),
    };
  };


const create = async (schedule: DriverScheduleCreate): Promise<ApiResponse> => {
  let resp = await fetchApi.post(`${BASE_URL}/`, schedule);
  return resp as ApiResponse;
};

const update = async (id: number, schedule: DriverSchedule): Promise<ApiResponse> => {
    let resp = await fetchApi.put(`${BASE_URL}/${id}`, schedule);
    return resp as ApiResponse;
};

const updateCheckList = async (id: number, schedule: UpdateDriverScheduleCheckList): Promise<ApiResponse> => {
  let resp = await fetchApi.put(`${BASE_URL}/${id}/checklist`, schedule);
  return resp as ApiResponse;
};


const driverScheduleService = {
    find,
    search,
    create,
    update,
    updateCheckList
}

export default driverScheduleService;