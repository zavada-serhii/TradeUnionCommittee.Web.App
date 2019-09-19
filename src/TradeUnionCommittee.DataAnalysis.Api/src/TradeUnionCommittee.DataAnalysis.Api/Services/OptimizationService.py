import numpy as np
import scipy as sp
import pandas as pd
import seaborn as sns

from scipy import stats
from sklearn.cluster import KMeans
from sklearn import decomposition

from io import StringIO

#------------------------------------------------------------------------------
# 4.1
# Return C#/.NET => ''
#------------------------------------------------------------------------------
def optimization_premiums_task1(input_csv):
    sio = StringIO(input_csv) 
    data = pd.read_csv(sio)
    return "service optimization_premiums_task1"