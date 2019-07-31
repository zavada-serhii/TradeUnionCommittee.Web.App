import json
import numpy as np
from models.employee import Employee 

def hello(input_json):
    listEmployee = []
    for employee in input_json:
        listEmployee.append(Employee(employee['Id'],employee['FullName'],employee['Email']))
    result = np.array(listEmployee).tolist()
    return json.dumps(result, default=lambda x: x.__dict__)
