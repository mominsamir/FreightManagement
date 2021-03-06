import { combineReducers } from '@reduxjs/toolkit';
import messagesReducer from 'redux/slices/messages';
import apiRequestReducer from 'redux/slices/apiRequest';

const rootReducer = combineReducers({
  messages: messagesReducer,
  apiRequest: apiRequestReducer
});

export type RootState = ReturnType<typeof rootReducer>;

export default rootReducer;
