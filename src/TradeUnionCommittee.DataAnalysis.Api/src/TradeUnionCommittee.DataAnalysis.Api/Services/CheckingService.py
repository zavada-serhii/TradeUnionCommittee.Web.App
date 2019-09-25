import numpy as np
import scipy as sp
import pandas as pd
import seaborn as sns

from scipy import stats
from sklearn.cluster import KMeans
from sklearn import decomposition

from io import StringIO

#------------------------------------------------------------------------------
# 5.1
# Return C#/.NET type => ''
#------------------------------------------------------------------------------
def checking_relevance_wellness_trips_task1(input_csv):
    csv = pd.read_csv(StringIO(input_csv))
    return "service checking_relevance_wellness_trips_task1"