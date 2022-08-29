import imp
import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import nltk
#nltk.download('vader_lexicon')
from nltk.sentiment import SentimentIntensityAnalyzer
 

sia = SentimentIntensityAnalyzer()


x = sia.polarity_scores('The market is so bad, I don’t know how to feel')

print(x)