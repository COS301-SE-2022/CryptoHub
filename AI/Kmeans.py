
import numpy as np
from numpy.linalg import norm

class Kmeans:


    def Initialize_Kmeans(self, number_of_Clusters, Random_State = 999, max_Iterations = 100):
        self.number_of_Clusters = number_of_Clusters
        self.Random_State = Random_State
        self.max_Iterations = max_Iterations

