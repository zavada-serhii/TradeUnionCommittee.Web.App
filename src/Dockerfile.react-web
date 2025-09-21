FROM node:16 AS build
WORKDIR /app
COPY ./TradeUnionCommittee.React.Web.GUI/package*.json ./
RUN npm ci --silent
COPY ./TradeUnionCommittee.React.Web.GUI ./
RUN npm run build

FROM nginx:alpine
WORKDIR /usr/share/nginx/html
COPY --from=build /app/build ./

SHELL ["/bin/sh", "-c"]

RUN cat <<'EOF' > /etc/nginx/conf.d/default.conf
server {
  listen 80;

  root   /usr/share/nginx/html;
  index  index.html;

  location / {
    try_files $uri /index.html;
  }

  location ~* \.(?:ico|css|js|gif|jpe?g|png|woff2?|eot|ttf|svg)$ {
    expires 6M;
    access_log off;
    add_header Cache-Control "public";
  }
}
EOF

RUN cat <<'EOF' > /docker-entrypoint.d/env.sh
#!/bin/sh
echo "window._env_ = {" > /usr/share/nginx/html/env-config.js
echo "  REACT_APP_MAIN_API_URL: \"${REACT_APP_MAIN_API_URL}\"" >> /usr/share/nginx/html/env-config.js
echo "}" >> /usr/share/nginx/html/env-config.js
exec "$@"
EOF

RUN chmod +x /docker-entrypoint.d/env.sh


EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
