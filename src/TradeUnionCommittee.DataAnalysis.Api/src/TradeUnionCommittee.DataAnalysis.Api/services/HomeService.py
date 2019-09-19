import json

import numpy as np
import pandas as pd

from io import StringIO
from Models.UserModel import User 


def test_json(input_json):
    listEmployee = []
    for employee in input_json:
        listEmployee.append(User(employee['Id'], "Hello, " + employee['FullName'], employee['Email']))
    result = np.array(listEmployee).tolist()
    return json.dumps(result, default=lambda x: x.__dict__)

def test_csv(input_csv):
    sio = StringIO(input_csv)
    data = pd.read_csv(sio)

    ids = data['Id']
    fullNames = data['FullName']
    emails = data['Email']

    return "Done"