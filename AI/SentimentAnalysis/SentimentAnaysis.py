import imp
import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import nltk
#nltk.download('vader_lexicon')
from nltk.sentiment import SentimentIntensityAnalyzer


def sentiment_Score(sentence) : 
    s_obj = SentimentIntensityAnalyzer()

    sentiment_value = s_obj.polarity_scores(sentence)

    # print("Overall Sentiment:  ", sentiment_value)
    # print("sentence was rated as ", sentiment_value['neg']*100, "% Negative")
    # print("sentence was rated as ", sentiment_value['neu']*100, "% Neutral")
    # print("sentence was rated as ", sentiment_value['pos']*100, "% Positive")

    # print("Overall  Sentiment : ", end = " ")

    # if sentiment_value['compound'] >= 0.05 :
    #      print("Positive")
    
    # elif sentiment_value['compound'] <= -0.05 :
    #     print("Negative")
    
    # else:
    #     print("Neutral")