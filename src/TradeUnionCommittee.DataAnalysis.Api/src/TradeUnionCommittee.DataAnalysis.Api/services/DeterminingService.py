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

def determining_unpopular_pastime_task1(input_csv):
    sio = StringIO(input_csv) 
    data = pd.read_csv(sio)
    return "service determining_unpopular_pastime_task1"