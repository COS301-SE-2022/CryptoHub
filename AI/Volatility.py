import math
import pandas as pd
import demand as d

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

dailyVolatility = []
annualVolatility = []
# follow = []
# rating = []
demand = []

for i in range (len(coinid)):
    df = pd.read_csv('CSVs/'+ coinid[i] +'.csv')

    price = df['priceUsd'].tolist()
    volatility = math.sqrt(sum(price)/len(price))
    annual = volatility * math.sqrt(252)

    dailyVolatility.append(volatility)
    annualVolatility.append(annual)

    #test for get follow/rating
    # print(d.get_marketCapUsd(coinid[i]))
    # follow.append(d.get_following(coinid[i]))1
    # rating.append(d.get_rating(coinid[i]))

    demand.append(d.Equation(coinid[i], 1))

data = [coinid, dailyVolatility, annualVolatility]
vf = pd.DataFrame(coinid, columns=['coinId'])
vf['dailyVolatility'] = dailyVolatility
vf['annualVolatility'] = annualVolatility
# vf['coidFollowers'] = follow
# vf['coinRating'] = rating
vf['demand'] = demand
vf.to_csv('Volatilities.csv', index = False)