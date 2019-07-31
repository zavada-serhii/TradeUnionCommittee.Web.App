FROM python:3.6

WORKDIR /app

ENV FLASK_APP analytics/src/TradeUnionCommittee.DataAnalysis.Api/runserver.py
ENV FLASK_RUN_HOST 0.0.0.0

COPY ["analytics/src/TradeUnionCommittee.DataAnalysis.Api/requirements.txt", "analytics/src/TradeUnionCommittee.DataAnalysis.Api/"]
RUN pip install -r "analytics/src/TradeUnionCommittee.DataAnalysis.Api/requirements.txt"

COPY . .
CMD ["flask", "run"]