import imp
import json
from types import SimpleNamespace
import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import nltk
import requests
#nltk.download('vader_lexicon')
from nltk.sentiment import SentimentIntensityAnalyzer
from Post import Post 
import CoinTags
 
'''
GET POSTS VIA TAGS #BITCOIN
'''




sia = SentimentIntensityAnalyzer()


x = sia.polarity_scores('#bitcoin The market is so bad, I don’t know how to feel')




print(x)


