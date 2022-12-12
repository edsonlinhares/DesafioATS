FROM node:19.2-alpine3.15 as base

WORKDIR /app
ENV PATH="./node_modules/.bin:$PATH"

COPY package.json ./

RUN npm install -g npm@9.2.0

RUN npm install --force

COPY ./ .

RUN npm run build

FROM nginx:1.19.6-alpine
COPY --from=base /app/dist/desafio-ats /usr/share/nginx/html

COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
