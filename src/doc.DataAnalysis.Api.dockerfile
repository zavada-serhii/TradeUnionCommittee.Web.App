FROM python:3.7.5

WORKDIR /app

ENV FLASK_APP ./runserver.py
ENV FLASK_RUN_HOST 0.0.0.0

COPY ./TradeUnionCommittee.DataAnalysis.Api/src/TradeUnionCommittee.DataAnalysis.Api ./
RUN pip install -r "./requirements.txt"

CMD ["flask", "run"]