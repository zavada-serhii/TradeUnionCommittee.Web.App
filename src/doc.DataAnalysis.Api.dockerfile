# =====================
# Stage 1: build deps
# =====================
FROM python:3.7-slim AS builder

RUN apt-get update && apt-get install -y --no-install-recommends \
    build-essential gcc g++ \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /app

COPY ./TradeUnionCommittee.DataAnalysis.Api/src/TradeUnionCommittee.DataAnalysis.Api/requirements.txt ./
RUN pip install --no-cache-dir --prefix=/install -r requirements.txt

# =====================
# Stage 2: runtime
# =====================
FROM python:3.7-slim

WORKDIR /app

COPY --from=builder /install /usr/local

COPY ./TradeUnionCommittee.DataAnalysis.Api/src/TradeUnionCommittee.DataAnalysis.Api ./

RUN useradd -m flaskuser
USER flaskuser

EXPOSE 5000

CMD ["gunicorn", "-b", "0.0.0.0:5000", "runserver:app"]
