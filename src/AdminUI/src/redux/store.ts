import { configureStore, Middleware } from '@reduxjs/toolkit';
import rootReducer from 'redux/rootReducer';
import { eventBus } from 'context/eventbus';

type RootState = ReturnType<typeof rootReducer>;

const arrayActions: Middleware<{}, RootState> = (store) => (next) => (action) => {
  if (!Array.isArray(action)) return next(action);
  return action.map((a) => store.dispatch(a));
};

const actionPublisher: Middleware<{}, RootState> = (store) => (next) => (action) => {
  let result = next(action);
  eventBus.next(action);
  return result;
};

const store = configureStore({
  reducer: rootReducer,
  middleware: [arrayActions, actionPublisher],
});

if (process.env.NODE_ENV === 'development' && module.hot) {
  module.hot.accept('./rootReducer', () => {
    const newRootReducer = require('./rootReducer').default;
    store.replaceReducer(newRootReducer);
  });
}

export type AppDispatch = typeof store.dispatch;

export default store;
