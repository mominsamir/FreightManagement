import React from 'react';
import ReactDOM from 'react-dom';
import './index.less';
import reportWebVitals from './reportWebVitals';
import { BrowserRouter as Router } from 'react-router-dom';
import { Provider, useSelector } from 'react-redux';
import apiRequestSelector from 'redux/selectors/apiRequest';
import { GlobalSpinner } from 'components';
import store from 'redux/store';

const SpinnerWrapper: React.FC = () => {
  const areRequestsPending = useSelector(apiRequestSelector.areRequestsPending);
  return <React.Fragment>{areRequestsPending && <GlobalSpinner />}</React.Fragment>;
};

const render = () => {
  const App = require('./app/App').default;

  ReactDOM.render(
    <Provider store={store}>
      <Router basename="/">
        <SpinnerWrapper />
        <App />
      </Router>
    </Provider>,
    document.getElementById('root')
  );
};

render();

if (process.env.NODE_ENV === 'development' && module.hot) {
  module.hot.accept('./app/App', render);
}

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
