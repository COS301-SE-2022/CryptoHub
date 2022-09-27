from typing import List
import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import nltk
#nltk.download('vader_lexicon')
from nltk.sentiment import SentimentIntensityAnalyzer
import SentimentAnalysis.CoinTags as CoinTags
from SentimentAnalysis.Post import Post,ScoredPost
import requests
import json



def GetCoinPostsByTag():
    lst = []
    for i in  range(CoinTags.tagCount()):
        x = requests.get('http://176.58.110.152:7215/api/Post/GetPostsByTag/'+CoinTags.getTag(i))
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

    scoredposts = json.loads(scoredposts)
    
    print(scoredposts)

    x = requests.patch('http://176.58.110.152:7215/api/Post/UpdatePostSentiment',json=scoredposts)
    return "done"

    


if __name__ == "__main__":
    main()
