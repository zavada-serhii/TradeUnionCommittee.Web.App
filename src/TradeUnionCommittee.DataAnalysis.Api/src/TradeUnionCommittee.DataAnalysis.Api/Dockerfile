FROM python:3.8.0

WORKDIR /app

ENV FLASK_APP TradeUnionCommittee.DataAnalysis.Api/src/TradeUnionCommittee.DataAnalysis.Api/runserver.py
ENV FLASK_RUN_HOST 0.0.0.0

COPY ["TradeUnionCommittee.DataAnalysis.Api/src/TradeUnionCommittee.DataAnalysis.Api/requirements.txt", "TradeUnionCommittee.DataAnalysis.Api/src/TradeUnionCommittee.DataAnalysis.Api/"]
RUN pip install -r "TradeUnionCommittee.DataAnalysis.Api/src/TradeUnionCommittee.DataAnalysis.Api/requirements.txt"

COPY . .
CMD ["flask", "run"]