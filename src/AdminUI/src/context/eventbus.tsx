import React from 'react';
import { Subject } from 'rxjs';
import { filter } from 'rxjs/operators';
import { PayloadAction } from '@reduxjs/toolkit';

export const eventBus = new Subject<PayloadAction<any>>();

export const EventBusContext = React.createContext(eventBus);

const eventBusSubscribe = (actionType: string, callback: any) => {
  let filteredSource = eventBus.pipe(filter((ev: PayloadAction<any>) => ev.type === actionType));
  return filteredSource.subscribe(callback);
};

export const withEventBus = function (Component: React.FC<any>) {
  return class extends React.Component {
    render() {
      return (
        <EventBusContext.Consumer>
          {(context) => <Component eventBusSubscribe={eventBusSubscribe} {...this.props} />}
        </EventBusContext.Consumer>
      );
    }
  };
};
