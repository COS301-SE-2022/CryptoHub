
import requests
from csv import DictReader


def get_supply(coinid):
    x = requests.get('http://api.coincap.io/v2/assets/'+coinid)
    content = x.json().get('data')
    return float(content.get('supply'))

def get_marketCapUsd(coinid):
    x = requests.get('http://api.coincap.io/v2/assets/'+coinid)
    content = x.json().get('data')
    return float(content.get('marketCapUsd'))

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
    a = get_supply(coinName)
    b = get_marketCapUsd(coinName)
    c = get_rating(coinName)
    d = get_following(coinName)
    fx = x*(a*w1 + b*w2 + c*w3 + d*w4)
    return fx



x =  requests.get('https://api.coincap.io/v2/assets')
content = x.json().get('data')

