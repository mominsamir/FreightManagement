import { createSlice } from '@reduxjs/toolkit';

export interface ApiRequestState {
  outstandingRequests: number;
}

export const initialStore: ApiRequestState = {
  outstandingRequests: 0,
};

const slice = createSlice({
  name: 'apiRequests',
  initialState: initialStore,
  reducers: {
    start(state) {
      state.outstandingRequests++;
    },
    complete(state) {
      state.outstandingRequests--;
    },
  },
});

export const { start, complete } = slice.actions;
export default slice.reducer;
