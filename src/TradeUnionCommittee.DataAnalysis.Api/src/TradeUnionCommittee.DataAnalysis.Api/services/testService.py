import json
import numpy as np
from models.testModel import Test 

def test(input_json):
    listEmployee = []
    for employee in input_json:
        listEmployee.append(Test(employee['Id'], "Hello, " + employee['FullName'], employee['Email']))
    result = np.array(listEmployee).tolist()
    return json.dumps(result, default=lambda x: x.__dict__)
