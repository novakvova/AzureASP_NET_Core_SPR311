@echo off

cd "WebWorker"

echo Building Docker image api...
docker build -t azure-spr311-api . 

echo Tagging Docker image api...
docker tag azure-spr311-api:latest novakvova/azure-spr311-api:latest

echo Pushing Docker image api to repository...
docker push novakvova/azure-spr311-api:latest

echo Done ---api---!
pause

