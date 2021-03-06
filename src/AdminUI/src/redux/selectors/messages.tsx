import { RootState } from 'redux/rootReducer';
import { MessagesState } from 'redux/slices/messages';

const getMessages = (store: RootState): MessagesState => store.messages;
const getErrorMessages = (store: RootState) => getMessages(store).error;
const getSuccessMessages = (store: RootState) => getMessages(store).success;
const getInfoMessages = (store: RootState) => getMessages(store).info;
const getWarnMessages = (store: RootState) => getMessages(store).warn;

const selectors = { getMessages, getErrorMessages, getSuccessMessages, getInfoMessages, getWarnMessages };
export default selectors;
