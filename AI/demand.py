
import requests
from csv import DictReader


def get_supply(coinid):
    x = requests.get('http://api.coincap.io/v2/assets/'+coinid)
    content = x.json().get('data')
    return content.get('supply')

def get_marketCapUsd(coinid):
    x = requests.get('http://api.coincap.io/v2/assets/'+coinid)
    content = x.json().get('data')
    return content.get('marketCapUsd')

def get_rating(coinName):
    with open('AI/ratings.csv', 'r') as f:
        reader = DictReader(f)
        for row in reader:
            if row['coinName'] == coinName:
                return row['rating']




def Equation(coinid,coinName, x, rating , following):
    a = get_supply(coinid)
    b = get_marketCapUsd(coinid)
    c = rating
    d = following
    fx = a(x)**3 + b(x)**2 + c(x) + d(x)
    return fx



x =  requests.get('https://api.coincap.io/v2/assets')
content = x.json().get('data')

