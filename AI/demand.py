
import requests

x =  requests.get('https://api.coincap.io/v2/assets')
content = x.json().get('data')

for i in range (2):
    print(content[i].get('marketCapUsd'))