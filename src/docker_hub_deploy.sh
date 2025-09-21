#!/bin/bash

sudo docker build -t sergeyzavada/tradeunioncommittee_data_analysis_api:latest -f doc.DataAnalysis.Api.dockerfile .
sudo docker build -t sergeyzavada/tradeunioncommittee_api:latest -f doc.Api.dockerfile .
sudo docker build -t sergeyzavada/tradeunioncommittee_razor_web_gui:latest -f doc.Razor.Web.GUI.dockerfile .
sudo docker build -t sergeyzavada/tradeunioncommittee_react_web_gui:latest -f doc.React.Web.GUI.dockerfile .

docker login

docker push sergeyzavada/tradeunioncommittee_data_analysis_api:latest
docker push sergeyzavada/tradeunioncommittee_api:latest
docker push sergeyzavada/tradeunioncommittee_razor_web_gui:latest
docker push sergeyzavada/tradeunioncommittee_react_web_gui:latest
