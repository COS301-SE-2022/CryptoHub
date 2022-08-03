
from turtle import distance
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

    def Calculate_Distance(self, X, centroids):
        distance = np.zeros((X.shape[0], self.number_of_Clusters))
        for i in range(self.number_of_Clusters):
            distance[:, i] = norm(X - centroids[i], axis = 1)
        return distance

    def determine_closest_cluster(self, distance):
        return np.argmin(distance, axis = 1)


    def Calculate_sse(self, X, labels, centroids):
        distance = np.zeros(X.shape[0])
        for i in range(self.number_of_Clusters):
            distance[labels == i] = norm(X[labels == i] - centroids[i], axis = 1)
        
        return np.sum(np.square(distance))


