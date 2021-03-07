FROM node:lts

RUN useradd -ms /bin/bash webuser

USER webuser
ADD package.json /tmp/
ADD yarn.lock /tmp/

WORKDIR /tmp
RUN yarn install
RUN mkdir -p /home/webuser/app

COPY Docker/files /home/webuser/app/
RUN cp -R /tmp/node_modules /home/webuser/app/node_modules

USER root
WORKDIR /home/webuser/app

EXPOSE 9001
CMD exec yarn start
