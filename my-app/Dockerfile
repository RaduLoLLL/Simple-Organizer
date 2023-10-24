# Use an official Node.js runtime as the base image
FROM node:18

# Set the working directory in the container
WORKDIR /app

# Copy package.json and package-lock.json to the container
COPY package*.json ./

# Install application dependencies
RUN npm install

# Copy the rest of the application source code to the container
COPY /components ./components
COPY /layouts ./layouts
COPY /middleware ./middleware
COPY /pages ./pages
COPY /server    ./server
COPY /prisma    ./prisma
COPY /plugins   ./plugins
COPY .env       ./
# Expose the port your application runs on
EXPOSE 3000

# Start the application
CMD ["npm", "run", "dev"]
