#------------------------------
# Backup
#------------------------------

# FROM python:3.7.5

# WORKDIR /app

# ENV FLASK_APP ./runserver.py
# ENV FLASK_RUN_HOST 0.0.0.0

# COPY ./TradeUnionCommittee.DataAnalysis.Api/src/TradeUnionCommittee.DataAnalysis.Api ./
# RUN pip install -r "./requirements.txt"

# CMD ["flask", "run"]


#------------------------------

FROM python:3.7.5

WORKDIR /app

RUN pip install Flask uWSGI

COPY ./TradeUnionCommittee.DataAnalysis.Api/src/TradeUnionCommittee.DataAnalysis.Api/Controllers ./Controllers
COPY ./TradeUnionCommittee.DataAnalysis.Api/src/TradeUnionCommittee.DataAnalysis.Api/Models ./Models
COPY ./TradeUnionCommittee.DataAnalysis.Api/src/TradeUnionCommittee.DataAnalysis.Api/Services ./Services
COPY ./TradeUnionCommittee.DataAnalysis.Api/src/TradeUnionCommittee.DataAnalysis.Api/requirements.txt ./
COPY ./TradeUnionCommittee.DataAnalysis.Api/src/TradeUnionCommittee.DataAnalysis.Api/runserver.py .

RUN pip install --no-cache-dir -r requirements.txt

CMD ["uwsgi", "--http", "0.0.0.0:5000", "--wsgi-file", "/app/runserver.py", "--callable", "app", "--master",  "--processes", "10"]