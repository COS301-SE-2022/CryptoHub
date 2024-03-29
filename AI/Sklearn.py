from math import hypot
import pandas as pd
import numpy as np
import seaborn as sns
import matplotlib.pyplot as plt
import sklearn.cluster as cluster

#Read data
df = pd.read_csv('AI/Volatilities.csv')

#Renames data coloumn names
#df.rename(columns={'Annual Income (k$)' : 'Income', 'Spending Score (1-100)' : 'Spending_Score'}, inplace=True)

#view data
#print(df.describe())

#Run kmeans clustering on Income and Spending_Score
kmeans = cluster.KMeans(n_clusters=5, init="k-means++", random_state=42)
kmeans = kmeans.fit(df[['demand', 'annualVolatility']])

#View cluster centroids
print(kmeans.cluster_centers_)

#Adds cluster labels to dataframe
df['Clusters'] = kmeans.labels_
print(df.head())

#Plot clusters
sns.scatterplot(x='demand', y='annualVolatility', hue='Clusters', data=df)
plt.show(block=True)

#Get cluster centers
centers = kmeans.cluster_centers_

#Adds cluster centers to dataframe
df['Distance'] = kmeans.labels_

#Calculate distance from cluster centers
for i in range(len(df)):
    distance = hypot(abs(centers[df.at[i, 'Clusters']][1] - df['demand'][i]), abs(centers[df.at[i, 'Clusters']][0] - df['annualVolatility'][i]))
    df['Distance'][i] = distance

#Sort dataframe by clusters then distance within cluster
df.sort_values(by=['Clusters', 'Distance'], inplace=True)
df.to_csv('AI/coinClusters.csv', index = False)