import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { Messages } from 'types/messages';

export interface MessagesState {
  error: string[];
  info: string[];
  warn: string[];
  success: string[];
}


export const initialStore: MessagesState = {
  error: [],
  success: [],
  warn: [],
  info: [],
};

const slice = createSlice({
  name: 'messages',
  initialState: initialStore,
  reducers: {
    add(state, action: PayloadAction<Messages>) {
      state[action.payload.messageType] = action.payload.messages;
    },
    reset(state) {
      return initialStore;
    },
  },
});

export const addSuccess = (messages: string[]) =>
  slice.actions.add({
    messages: messages,
    messageType: 'success',
  } as Messages);

export const addError = (messages: string[]) =>
  slice.actions.add({
    messages: messages,
    messageType: 'error',
  } as Messages);

export const addWarn = (messages: string[]) =>
  slice.actions.add({
    messages: messages,
    messageType: 'warn',
  } as Messages);

export const addInfo = (messages: string[]) =>
  slice.actions.add({
    messages: messages,
    messageType: 'info',
  } as Messages);

export const { reset } = slice.actions;

export default slice.reducer;
