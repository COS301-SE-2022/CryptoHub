
import requests

def get_supply(coinid):
    x = requests.get('http://api.coincap.io/v2/assets/'+coinid)
    content = x.json().get('data')
    return content.get('supply')


x =  requests.get('https://api.coincap.io/v2/assets')
content = x.json().get('data')


for i in range (1):
    print(content[i].get('marketCapUsd'))