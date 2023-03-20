


FROM node:12.7-alpine AS nodebuilder
WORKDIR /usr/src/app
COPY /src/Clients/AngularWebApp/WebApp/ClientApp/package.json /usr/src/app


RUN npm install
COPY src/Clients/AngularWebApp/WebApp/ClientApp/. /usr/src/app
RUN npm run build


FROM nginx:alpine
COPY --from=nodebuilder /usr/src/app/dist /usr/share/nginx/html
COPY ./src/Clients/AngularWebApp/WebApp/nginx.conf /etc/nginx/nginx.conf


