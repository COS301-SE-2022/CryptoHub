import requests

x = requests.get('https://api.coincap.io/v2/assets/bitcoin/history?interval=d1&start=1325368800000&end=1660412319938')

print(x.text)