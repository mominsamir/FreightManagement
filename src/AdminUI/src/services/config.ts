import { ChangePassword, ConfigState } from 'types/config';
import { UserIdendity } from 'types/user';
import fetchApi, { ApiResponse } from 'Utils/fetchApi';
import Cookies from 'js-cookie';


const login = async (user :UserIdendity): Promise<ApiResponse> => {
  let resp: any = await fetchApi.post('/api/Authentication/login', user);
  Cookies.set('TOKEN',`Bearer  ${resp.token}`);    
  return resp as ApiResponse;
};

const logout = async (): Promise<void> => {
  await fetchApi.post('/api/v1/logout',{});
};

const changePassword = async (password: ChangePassword): Promise<ApiResponse> => {
  return await fetchApi.post('/api/Authentication/change-password',password) as ApiResponse;
};

const loadConfig = async (): Promise<ConfigState> => {
  let apiResp = await fetchApi.get('/api/Authentication/user-configration/');
  return apiResp as ConfigState;
};


const configService = {
  loadConfig,
  login,
  logout,
  changePassword
}

export default configService;