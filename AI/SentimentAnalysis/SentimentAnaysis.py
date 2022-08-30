import imp
import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import nltk
#nltk.download('vader_lexicon')
from nltk.sentiment import SentimentIntensityAnalyzer


def sentiment_Score(posts, list) : 
    s_obj = SentimentIntensityAnalyzer()
    
    for i in range(len(posts)):
        sentiment_value = s_obj.polarity_scores(posts)
        x = sentiment_value['compound']
        list.append()

def GetAverageSentiment(list):
    return sum(list)/ len(list)

def DetermineTheSentiment(x):
    if x['compound'] >= 0.05 :
         return("Positive")
    
    elif x['compound'] <= -0.05 :
        return("Negative")
    
    else:
        return("Neutral")


def Main(posts):
    list = []
    sentiment_Score(posts, list)
    SentimentNum = GetAverageSentiment(list)
    SentimentValue = DetermineTheSentiment(SentimentNum)


