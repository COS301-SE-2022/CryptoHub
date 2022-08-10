import pandas as pd
import numpy as np
import seaborn as sns
import matplotlib.pyplot as plt
import sklearn.cluster as cluster

df = pd.read_csv('D:/UP/2022/Semester_1/COS301/Development/tuks/CryptoHub/AI/Mall_Customers.csv')

df.rename(columns={'Annual Income (k$)' : 'Income', 'Spending Score (1-100)' : 'Spending_Score'}, inplace=True)

print(df.describe())

# sns.pairplot(df[['Age', 'Income', 'Spending_Score']])
# plt.show(block=True)

kmeans = cluster.KMeans(n_clusters=5, init="k-means++", random_state=42)
kmeans = kmeans.fit(df[['Income', 'Spending_Score']])

print(kmeans.cluster_centers_)

df['Clusters'] = kmeans.labels_
print(df.head())

df.to_csv('AI/mallLusters.csv', index = False)

sns.scatterplot(x='Spending_Score', y='Income', hue='Clusters', data=df)
plt.show(block=True)