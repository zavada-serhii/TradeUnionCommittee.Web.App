# Docker Image which is used as foundation to create
# a custom Docker Image with this Dockerfile
FROM node:lts
# A directory within the virtualized Docker environment
# Becomes more relevant when using Docker Compose later
WORKDIR /app
# Copies package.json and package-lock.json to Docker environment
COPY ./TradeUnionCommittee.React.Web.GUI/package*.json ./
# Installs all node packages
RUN npm install --production --silent
# Copies everything over to Docker environment
COPY ./TradeUnionCommittee.React.Web.GUI ./
# Uses port which is used by the actual application
EXPOSE 3000
# Finally runs the application
CMD npm start