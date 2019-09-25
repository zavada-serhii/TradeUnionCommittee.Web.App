import numpy as np
import scipy as sp
import pandas as pd
import seaborn as sns

from scipy import stats
from sklearn.cluster import KMeans
from sklearn import decomposition
from json_tricks import dump, dumps, load, loads, strip_comments

from io import StringIO
from Models.ClusterModel import Cluster

#------------------------------------------------------------------------------
# 1.1 - 1.2
# Return C#/.NET type => ''
#------------------------------------------------------------------------------
def correlation_analysis(input_csv):

    csv = pd.read_csv(StringIO(input_csv))

    correlation = csv.corr()
    return str(np.array(correlation).tolist())

#------------------------------------------------------------------------------
# 1.3
# Return C#/.NET type => ''
#------------------------------------------------------------------------------
def checking_significance_coefficients(input_csv):

    csv = pd.read_csv(StringIO(input_csv))
    columns = list(csv.head(0))

    result = ''
    corr_matr = csv.corr()
    matr = np.zeros((len(corr_matr), len(corr_matr)))

    for i in range(len(csv.corr()) - 1):
        j = 0
        while(j < len(csv.corr())):
            if(j < i or j == i):
                j = i + 1
            matr[i][j] = corr_matr[columns[i]][columns[j]]
            result += ''
            result += f'Для парного коэффициента: {columns[i]} - {columns[j]} тест на значимость: {test_for_paired(matr[i][j], csv)}\n'
            j += 1
    return result

def test_for_paired(c, data):
    result = ''
    n = len(data)
    t_st = np.abs(c)*np.sqrt((n-2)/(1-c*c))
    # Пусть альфа = 0.2, тогда t_kr = t(0.1, 50)
    t_kr = 1.6759
    if(t_st > t_kr):
        result += f't_st = {t_st}; t_kr = {t_kr} t_st > t_k => Парный коэффициент корреляции значимо отличается от 0 с вероятностью 80%'
    else:
        result += f't_st = {t_st}; t_kr = {t_kr} t_st < t_k => Парный коэффициент корреляции значимо не отличается от 0 с вероятностью 80%'
    return result

#------------------------------------------------------------------------------
# 1.4
# Return C#/.NET type => ''
#------------------------------------------------------------------------------
def cluster_analysis(input_json):

    json = loads(input_json, preserve_order=True)

    csv = pd.read_csv(StringIO(json['Csv'])) 
    countCluster = json['CountCluster']

    raw1 = csv['X']
    raw2 = csv['Y']

    x = [[] for i in range(countCluster)]
    y = [[] for i in range(countCluster)]

    X = np.array(list(zip(raw1, raw2)))
    kmeans = KMeans(n_clusters=countCluster, random_state=0).fit(X)

    centers = kmeans.cluster_centers_
    Y = X[:, 1]
    X = X[:, 0]
    for i in range(len(Y)):
        distances = np.zeros(countCluster)
        for j in range(countCluster):
            distances[j] = dist(X[i], centers[j][0], Y[i], centers[j][1])
        indx = np.argmin(distances)
        x[indx].append(X[i])
        y[indx].append(Y[i])

    return dumps(Cluster(x,y,centers), primitives=True)

def dist(x1, x2, y1, y2, ax=1):
    return np.sqrt((x2 - x1)**2 + (y2 - y1)**2)