from Controllers import app
from flask import Flask, request, jsonify, json
import Services.DeterminingService as service

#------------------------------------------------------------------------------
# 2.1
# Return C#/.NET type => ''
#------------------------------------------------------------------------------
@app.route('/api/Determining/ProbablePastime/Task1', methods=['POST'])
def determining_probable_pastime_task1():
    input_json = request.get_json(force=True)
    return service.multi_coeff(input_json)

#------------------------------------------------------------------------------
# 2.2 - 2.4
# Return C#/.NET type => ''
#------------------------------------------------------------------------------
@app.route('/api/Determining/ProbablePastime/Task2', methods=['POST'])
def determining_probable_pastime_task2():
    input_json = request.get_json(force=True)
    return service.determining_probable_pastime_task2(input_json)

#------------------------------------------------------------------------------------------------------------------------------------------------------------

#------------------------------------------------------------------------------
# 3.1
# Return C#/.NET type => ''
#------------------------------------------------------------------------------
@app.route('/api/Determining/UnpopularPastime/Task1', methods=['POST'])
def determining_unpopular_pastime_task1():
    input_json = request.get_json(force=True)
    return service.determining_unpopular_pastime_task1(input_json)