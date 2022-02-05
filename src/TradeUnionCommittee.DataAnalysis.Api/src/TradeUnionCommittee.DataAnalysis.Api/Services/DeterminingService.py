import numpy as np
import scipy as sp
import pandas as pd
import seaborn as sns

from scipy import stats
from sklearn.cluster import KMeans
from sklearn import decomposition
from json_tricks import dump, dumps, load, loads, strip_comments

from io import StringIO
from Models.MultiFactorModel import MultiFactor

#------------------------------------------------------------------------------
# 2.1
#------------------------------------------------------------------------------
def multi_coeff(input_csv):

    csv = pd.read_csv(StringIO(input_csv))

    Y = csv['Y']
    X1 = csv['X1']
    X2 = csv['X2']

    corr_coef = np.corrcoef(Y, X1)
    corr_Y_X1 = corr_coef[0][1]
   
    corr_coef = np.corrcoef(Y, X2)
    corr_Y_X2 = corr_coef[0][1]
   
    corr_coef = np.corrcoef(X1, X2)
    corr_X1_X2 = corr_coef[0][1]
   
    result = np.sqrt((corr_Y_X1**2 + corr_Y_X2**2 - 2 * corr_Y_X1 * corr_Y_X2 * corr_X1_X2)/(1 - corr_X1_X2**2))

    return dumps(result, primitives=True)

#------------------------------------------------------------------------------
# 2.4 - 2.6
#------------------------------------------------------------------------------
def determining_probable_pastime_task2(input_csv):

    csv = pd.read_csv(StringIO(input_csv))

    Y = csv['Y']   # Travel Count
    X1 = csv['X1'] # Wellness Count
    X2 = csv['X2'] # Tour Count
    X3 = csv['X3'] # MaterialAidONU MaterialAid_ONU
    X4 = csv['X4'] # AwardONU Award_ONU
    X5 = csv['X5'] # Children Count
    X6 = csv['X6'] # GrandChildren Count

    beta = regression_model(Y, X1, X2, X3, X4, X5, X6)
    beta_st = standartization(beta, Y, X1, X2, X3, X4, X5, X6)
    test = significance_test(beta_st)
    interval = confidence_interval(beta_st, test)

    return dumps(MultiFactor(beta, beta_st, test, interval), primitives=True)

#------------------------------------------------------------------------------
# 2.4
#------------------------------------------------------------------------------
def regression_model(Y, X1, X2, X3, X4, X5, X6):

    ones = np.ones(len(Y))
    X = np.column_stack((ones, X1, X2, X3, X4, X5, X6))
    XT = np.transpose(X)
    XT_X = np.dot(XT, X)
    inv_XT_X = np.linalg.inv(XT_X)
    X_beta = np.dot(inv_XT_X, XT)
    beta = np.dot(X_beta, Y)

    Y_b = beta[0]*ones + beta[1]*X1 + beta[2]*X2 + beta[3]*X3 + beta[4]*X4 + beta[5]*X5 + beta[6]*X6
    beta = np.array([beta[1], beta[2], beta[3], beta[4], beta[5], beta[6]])
    U = np.empty(len(Y), float)

    j = 0
    while j < len(Y):
        U[j] = Y[j] - Y_b[j]
        j += 1

    i = 0
    sum_uu = 0
    while i < len(Y):
        sum_uu += U[i] * U[i]
        i += 1

    i = 0
    sum_uy = 0
    while i < len(Y):
        if (Y[i] == 0):
            c = 10000000
        else:
            c = Y[i]
        sum_uy += np.abs(U[i]) / c
        i += 1

    dispersion_sq = sum_uu / 827
    MSE = (1 / len(Y)) * sum_uu
    MAPE = (100 / len(Y)) * sum_uy
    M_U = (1 / len(Y)) * np.sum(U)
    return beta

#------------------------------------------------------------------------------
# 2.5
#------------------------------------------------------------------------------
def standartization(beta, Y, X1, X2, X3, X4, X5, X6):

    def sum_T(R):
        i = 0
        sum_r = 0
        while i < len(Y):
            sum_r += R[i]
            i += 1
        return sum_r / len(Y)

    def st_deviation(R):
        R_ = sum_T(R)
        i = 0
        sum_diff_r = 0
        while i < len(Y):
            sum_diff_r += (R[i] - R_) ** 2
            i += 1
        return np.sqrt(sum_diff_r / (len(Y) - 1))

    st_deviation_y = st_deviation(Y)
    st_deviation_x = np.array([st_deviation(X1), st_deviation(X2), st_deviation(X3), st_deviation(X4), st_deviation(X5), st_deviation(X6)])
    beta_st = np.zeros(len(beta))

    i = 0
    while i < len(beta):
        beta_st[i] = beta[i] * (st_deviation_x[i] / st_deviation_y)
        i += 1

    return beta_st

#------------------------------------------------------------------------------
# 2.6
#------------------------------------------------------------------------------
def significance_test(beta):

    c = beta[0]

    sum_b = 0
    for i in range(len(beta)):
        sum_b += beta[i]

    beta_0 = 0
    beta_ = sum_b / len(beta)

    # Пусть альфа = 0.1, тогда t_kr = t(0.05, 824)
    t_kr = 1.962850624

    # Тест на значимость парного коэфф
    t_st = (c - beta_0) / np.sqrt(((c - beta_) ** 2) / 5)

    #if (np.abs(t_st) > t_kr):
        #print('Коэффициент регрессии значимо отличается от ', beta_0, ' с вероятностью 90%')
    #else:
        #print('Коэффициент регрессии значимо не отличается от ', beta_0, ' с вероятностью 90%')
    return [t_kr, t_st, beta_]

def confidence_interval(beta_st, test):

    b = beta_st[0]
    t_kr = test[0]
    beta_ = test[2]

    low = b - t_kr * np.sqrt(((b - beta_) ** 2) / 5)
    up = b + t_kr * np.sqrt(((b - beta_) ** 2) / 5)
    return [low, up]

#------------------------------------------------------------------------------
# 2.7
#------------------------------------------------------------------------------
def pca(input_json):

    json = input_json

    csv = pd.read_csv(StringIO(json['Csv'])) 
    countComponents = json['CountComponents']
    
    pca = decomposition.PCA(n_components=countComponents)
    csv = pca.fit(csv).transform(csv)
    
    return dumps(csv, primitives=True)

#------------------------------------------------------------------------------------------------------------------------------------------------------------

#------------------------------------------------------------------------------
# 3.1
#------------------------------------------------------------------------------
def determining_unpopular_pastime_task1(input_csv):
    csv = pd.read_csv(StringIO(input_csv))
    return "service determining_unpopular_pastime_task1"