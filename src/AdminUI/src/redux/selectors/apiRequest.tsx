import { RootState } from 'redux/rootReducer';
import { ApiRequestState } from 'redux/slices/apiRequest';

const getApiRequest = (state: RootState): ApiRequestState => state.apiRequest;
const areRequestsPending = (state: RootState): boolean => getApiRequest(state).outstandingRequests !== 0;

const selectors = { areRequestsPending };

export default selectors;
