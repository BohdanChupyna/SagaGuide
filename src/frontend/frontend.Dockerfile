# pull official base image
FROM node:alpine

# set working directory
WORKDIR /usr/src/gmspacefrontend


# install app dependencies
COPY package.json ./
COPY package-lock.json ./
RUN npm install --silent

# add app
COPY . ./

# start app
CMD ["npm", "start"]