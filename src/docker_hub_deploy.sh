#!/bin/bash

sudo docker build -t sergeyzavada/tradeunioncommittee_data_analysis_api:latest -f Dockerfile.data-analysis-api .
sudo docker build -t sergeyzavada/tradeunioncommittee_api:latest -f Dockerfile.api .
sudo docker build -t sergeyzavada/tradeunioncommittee_razor_web_gui:latest -f Dockerfile.razor-web .
sudo docker build -t sergeyzavada/tradeunioncommittee_react_web_gui:latest -f Dockerfile.react-web .

docker login

docker push sergeyzavada/tradeunioncommittee_data_analysis_api:latest
docker push sergeyzavada/tradeunioncommittee_api:latest
docker push sergeyzavada/tradeunioncommittee_razor_web_gui:latest
docker push sergeyzavada/tradeunioncommittee_react_web_gui:latest
