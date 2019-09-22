import numpy as np
import scipy as sp
import pandas as pd
import seaborn as sns

from scipy import stats
from sklearn.cluster import KMeans
from sklearn import decomposition

from io import StringIO

def determining_probable_pastime_task1(input_csv):
    sio = StringIO(input_csv) 
    data = pd.read_csv(sio)
    return "service determining_probable_pastime_task1"

#------------------------------------------------------------------------------
# 2.1
# Return C#/.NET => ''
#------------------------------------------------------------------------------
def multi_coeff(input_json):

    X1 = input_json['X1']
    X2 = input_json['X2']
    Y = input_json['Y']

    corr_coef = np.corrcoef(Y, X1)
    corr_Y_X1 = corr_coef[0][1]
   
    corr_coef = np.corrcoef(Y, X2)
    corr_Y_X2 = corr_coef[0][1]
   
    corr_coef = np.corrcoef(X1, X2)
    corr_X1_X2 = corr_coef[0][1]
   
    return np.sqrt((corr_Y_X1**2 + corr_Y_X2**2 - 2 * corr_Y_X1 * corr_Y_X2 * corr_X1_X2)/(1 - corr_X1_X2**2))

#------------------------------------------------------------------------------
# 2.2
# Return C#/.NET => ''
#------------------------------------------------------------------------------
def regression_model(input_json):
    
    X1 = input_json['X1']
    X2 = input_json['X2']
    X3 = input_json['X3']
    X4 = input_json['X4']
    X5 = input_json['X5']
    X6 = input_json['X6']
    Y = input_json['Y']

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
# 2.3
# Return C#/.NET => ''
#------------------------------------------------------------------------------
def standartization(input_json):
    
    X1 = input_json['X1']
    X2 = input_json['X2']
    X3 = input_json['X3']
    X4 = input_json['X4']
    X5 = input_json['X5']
    X6 = input_json['X6']
    Y = input_json['Y']
    beta = input_json['beta']

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
# 2.4
# Return C#/.NET => ''
#------------------------------------------------------------------------------
def significance_test(input_json):

    beta = input_json['Beta']
    c = input_json['C']

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

def confidence_interval(b, t_kr, beta_):

    t_kr = input_json['TCriterion']
    c = input_json['C']
    c = input_json['C']

    low = b - t_kr * np.sqrt(((b - beta_) ** 2) / 5)
    up = b + t_kr * np.sqrt(((b - beta_) ** 2) / 5)
    return [low, up]

#------------------------------------------------------------------------------------------------------------------------------------------------------------

#------------------------------------------------------------------------------
# 3.1
# Return C#/.NET => ''
#------------------------------------------------------------------------------
def determining_unpopular_pastime_task1(input_csv):
    sio = StringIO(input_csv) 
    data = pd.read_csv(sio)
    return "service determining_unpopular_pastime_task1"