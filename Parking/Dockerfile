FROM node:14.15.4-alpine3.11 As builder

WORKDIR /usr/src/app

COPY package.json package-lock.json ./

RUN npm install

COPY . .

RUN npm run build --prod

FROM nginx:1.19.6-alpine

EXPOSE 80

COPY --from=builder /usr/src/app/dist/Parking/ /usr/share/nginx/html
