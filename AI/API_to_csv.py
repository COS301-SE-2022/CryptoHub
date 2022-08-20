from email import header
from fileinput import filename
import json
import os
from types import SimpleNamespace
import requests
import pandas as pd

# x = requests.get('https://api.coincap.io/v2/assets/bitcoin/history?interval=d1&start=1325368800000&end=1660412319938')

# print(x.text)

# x = requests.get('http://api.coincap.io/v2/assets')

# x = x.text

# z = json.loads(x,object_hook=lambda d: SimpleNamespace(**d))

# for i in range(100):
#     print("'"+z.data[i].id+"'"+",")

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


for i in range (100):
    filename = coinid[i] + ".csv"
    x = requests.get('https://api.coincap.io/v2/assets/'+ coinid[i]+ '/history?interval=d1&start=1325368800000&end=1660412319938')
    content = x.json().get('data')
    # with open(filename, 'w') as f:
    #     json.dump(content, f)

    json_str =  '{"Courses":{"r1":"Spark"},"Fee":{"r1":"25000"},"Duration":{"r1":"50 Days"}}'

    df = pd.read_json(json.dumps(content))

    #headers = ['priceUsd','time','date']
    df.to_csv(os.path.join('CSVs',filename), encoding = 'utf-8',sep=",")






