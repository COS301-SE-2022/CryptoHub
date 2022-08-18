import json
from logging.handlers import RotatingFileHandler
import math
import os
from unicodedata import name
import pandas as pd
import random

coinid=[
'bitcoin',
'ethereum',
'tether',
'binance-coin',
'usd-coin',
'cardano',
'binance-usd',
'xrp',
'solana',
'polkadot',
'dogecoin',
'avalanche',
'polygon',
'multi-collateral-dai',
'terra-luna',
'shiba-inu',
'uniswap',
'tron',
'wrapped-bitcoin',
'ethereum-classic',
'unus-sed-leo',
'litecoin',
'near-protocol',
'chainlink',
'ftx-token',
'crypto-com-coin',
'stellar',
'flow',
'monero',
'cosmos',
'bitcoin-cash',
'bitcoin-bep2',
'algorand',
'vechain',
'filecoin',
'internet-computer',
'decentraland',
'tezos',
'the-sandbox',
'axie-infinity',
'theta',
'aave',
'quant',
'elrond-egld',
'frax',
'eos',
'okb',
'trueusd',
'bitcoin-sv',
'hedera-hashgraph',
'zcash',
'helium',
'kucoin-token',
'maker',
'fantom',
'iota',
'the-graph',
'thorchain',
'chiliz',
'paxos-standard',
'lido-dao',
'klaytn',
'ecash',
'celsius',
'neo',
'huobi-token',
'curve-dao-token',
'pancakeswap',
'basic-attention-token',
'stacks',
'waves',
'loopring',
'enjin-coin',
'zilliqa',
'dash',
'pax-gold',
'mina',
'kava',
'nexo',
'bitcoin-gold',
'celo',
'kusama',
'decred',
'1inch',
'arweave',
'gnosis-gno',
'nem',
'oasis-network',
'convex-finance',
'trust-wallet-token',
'compound',
'synthetix-network-token',
'gala',
'holo',
'qtum',
'xinfin-network',
'ankr',
'yearn-finance',
'ravencoin',
'kadena'
]

# dailyVolatility = []
# annualVolatility = []

# for i in range (len(coinid)):
#     df = pd.read_csv('AI/CSVs/'+ coinid[i] +'.csv')

#     price = df['priceUsd'].tolist()
#     volatility = math.sqrt(sum(price)/len(price))
#     annual = volatility * math.sqrt(252)

#     dailyVolatility.append(volatility)
#     annualVolatility.append(annual)

# data = [coinid, dailyVolatility, annualVolatility]
# vf = pd.DataFrame(coinid, columns=['coinId'])
# vf['dailyVolatility'] = dailyVolatility
# vf['annualVolatility'] = annualVolatility
# vf.to_csv('AI/Volatilities.csv', index = False)
class Coin:
    coinName=""
    rating=0
    followers=0


lst = [];

for i in coinid:
    c = Coin();
    c.coinName=i
    c.rating=random.randrange(1,6)
    c.followers=random.randrange(0,100)
    lst.append(c)

df = pd.read_json(json.dumps([ob.__dict__ for ob in lst]))

    #headers = ['priceUsd','time','date']
df.to_csv("RatingAndFollows.csv", encoding = 'utf-8',sep=";")






