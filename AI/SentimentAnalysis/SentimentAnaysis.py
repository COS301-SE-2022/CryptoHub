from typing import List
import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import nltk
#nltk.download('vader_lexicon')
from nltk.sentiment import SentimentIntensityAnalyzer
from Post import Post,ScoredPost
import CoinTags
import requests
import json



def GetCoinPostsByTag():
    lst = []
    for i in  range(CoinTags.tagCount()):
        x = requests.get('http://localhost:7215/api/Post/GetPostsByTag/'+CoinTags.getTag(i))
        posts = json.loads(x.text)
        for p in posts:
            lst.append(Post(p))
    
    return lst

def sentiment_Score(posts:List[Post], lst) : 
    s_obj = SentimentIntensityAnalyzer()

    for p in posts:
        sentiment_value = s_obj.polarity_scores(p.content)
        p.sentimentScore = sentiment_value['compound']
        lst.append(ScoredPost(p))

def GetAverageSentiment(list):
    return sum(list)/ len(list)

def DetermineTheSentiment(x):
    if x['compound'] >= 0.05 and x < 0.07 :
         return("Positive")
    
    elif x['compond'] >= 0.07:
        return("Very Positive")
    
    elif x['compound'] <= -0.05 and x >= -0.07:
        return("Negative")
    
    elif x['compound'] <= -0.07:
        return ("Very Negative")
    
    else:
        return("Neutral")


def main():
    lst = []
    print("go")
    posts = GetCoinPostsByTag()
    sentiment_Score(posts, lst)
    
    scoredposts = json.dumps(lst, default=lambda o: o.__dict__, 
            sort_keys=True)
    
    x = requests.post('http://localhost:7215/api/Post/UpdatePostSentiment',json=scoredposts)

    # SentimentNum = GetAverageSentiment(lst)
    # DetermineTheSentiment(SentimentNum)


if __name__ == "__main__":
    main()
