
import requests

def get_supply(coinid):
    x = requests.get('http://api.coincap.io/v2/assets/'+coinid)
    content = x.json().get('data')
    return content.get('supply')

def get_marketCapUsd(coinid):
    x = requests.get('http://api.coincap.io/v2/assets/'+coinid)
    content = x.json().get('data')
    return content.get('marketCapUsd')

def Equation(coinid, x, rating , following):
    a = get_supply(coinid)
    b = get_marketCapUsd(coinid)
    c = rating
    d = following
    fx = a(x)**3 + b(x)**2 + c(x) + d(x)

x =  requests.get('https://api.coincap.io/v2/assets')
content = x.json().get('data')


for i in range (1):
    print(content[i].get('marketCapUsd'))