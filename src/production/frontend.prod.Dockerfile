FROM node:21-alpine AS builder
ENV NODE_ENV production

# Add a work directory
WORKDIR /app

# Cache and Install dependencies
COPY ../frontend/package.json .
COPY ../frontend/package-lock.json .
COPY ../frontend/tsconfig.json .
RUN npm install

# Copy app files
COPY ../frontend/ .

# Build the app
RUN npm run build

# Bundle static assets with nginx
FROM nginx:1.25.3-alpine as production
ENV NODE_ENV production

# Copy built assets from builder
COPY --from=builder /app/build /usr/share/nginx/html
COPY ../production/nginx.conf /etc/nginx/conf.d/default.conf

#RUN rm /etc/nginx/conf.d/default.conf
# Add your nginx.conf
#COPY ../production/nginx.conf /etc/nginx/nginx.conf 
#/etc/nginx/conf.d/default.conf

# Expose port
EXPOSE 80
EXPOSE 443

# Start nginx
CMD ["nginx", "-g", "daemon off;"]
