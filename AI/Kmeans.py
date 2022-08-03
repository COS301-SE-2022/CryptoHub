
import numpy as np
from numpy.linalg import norm

class Kmeans:


    def Initialize_Kmeans(self, number_of_Clusters, Random_State = 999, max_Iterations = 100):
        self.number_of_Clusters = number_of_Clusters
        self.Random_State = Random_State
        self.max_Iterations = max_Iterations

    def Initialize_Centroids(self, X):
       np.random.RandomState(self.Random_State)
       random_index = np.random.permutation(X.shape[0])
       centroids = X[random_index[:self.number_of_Clusters]]
       return centroids

    def Calculate_Centroids(self, X, labels):
        centroids = np.zeros((self.number_of_Clusters, X.shape[1]))
        for i in range(self.number_of_Clusters):
            centroids[i] = np.mean(X[labels == i], axis = 0)
        return centroids

    def Calculate_

