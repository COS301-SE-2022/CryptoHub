
import requests
from csv import DictReader




def get_rating(Name):
    with open('RatingAndFollows.csv', 'r') as f:
        reader = DictReader(f)
        for row in reader:
            if row['coinName'] == Name:
                return float(row['rating'])

def get_following(Name):
    with open('RatingAndFollows.csv', 'r') as f:
        reader = DictReader(f)
        for row in reader:
            if row['coinName'] == Name:
                return float(row['followers'])




def Equation(coinName, x, w1, w2, w3, w4):
    a = 0
    b = 0
    c = get_rating(coinName)
    d = get_following(coinName)
    fx = x*(a*w1 + b*w2 + c*w3 + d*w4)
    return fx



x =  requests.get('https://api.coincap.io/v2/assets')
content = x.json().get('data')

