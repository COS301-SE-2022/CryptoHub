FROM python:3

WORKDIR /app

COPY ./req ./req
COPY ./Profanity ./Profanity
COPY ./SentimentAnalysis ./SentimentAnalysis
COPY ./AI_API.py ./

RUN pip install -r req/requirements.txt

CMD [ "python","AI_API.py"]