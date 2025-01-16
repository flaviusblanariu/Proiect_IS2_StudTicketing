@echo off
REM Build the Docker image
docker build -t studticketing .

REM Run the container
docker run -d -p 8080:80 --name studticketing-container studticketing

echo Docker container is running. Access the application at http://localhost:8080
