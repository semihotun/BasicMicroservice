

FROM node:15.4.0 AS nodebuilder
WORKDIR /usr/src/app
COPY /src/Clients/AngularWebApp/WebApp/ClientApp/package.json /usr/src/app


RUN npm install
COPY src/Clients/AngularWebApp/WebApp/ClientApp/. /usr/src/app
RUN npm run build --prod


FROM nginx:alpine
COPY --from=nodebuilder /usr/src/app/dist/ClientApp /usr/share/nginx/html
COPY ./src/Clients/AngularWebApp/WebApp/nginx.conf /etc/nginx/nginx.conf


