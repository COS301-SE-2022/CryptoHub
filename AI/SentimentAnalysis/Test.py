from tkinter import SE
from vaderSentiment.vaderSentiment import SentimentIntensityAnalyzer

def sentiment_Score(sentence) : 
    s_obj = SentimentIntensityAnalyzer()

    sentiment_value = s_obj.polarity_scores(sentence)

    print("Overall Sentiment:  ", sentiment_value)