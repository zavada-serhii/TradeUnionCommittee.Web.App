from Controllers import app
from flask import Flask, request, jsonify, json
import Services.ForecastingService as forecastingService
import Services.DeterminingService as determiningService

#------------------------------------------------------------------------------
# 2.1
# Return C#/.NET type => ''
#------------------------------------------------------------------------------
@app.route('/api/Determining/ProbablePastime/MultiCorrelationCoefficient', methods=['POST'])
def determining_probable_pastime_task1():
    input_json = request.get_json(force=True)
    return determiningService.multi_coeff(input_json)

#------------------------------------------------------------------------------
# 2.2 - 2.4
# Return C#/.NET type => ''
#------------------------------------------------------------------------------
@app.route('/api/Determining/ProbablePastime/MultiFactorModel', methods=['POST'])
def determining_probable_pastime_task2():
    input_json = request.get_json(force=True)
    return determiningService.determining_probable_pastime_task2(input_json)

#------------------------------------------------------------------------------
# 2.5
# Return C#/.NET type => ''
#------------------------------------------------------------------------------
@app.route('/api/Determining/ProbablePastime/PrincipalComponentAnalysis', methods=['POST'])
def determining_probable_pastime_task5():
    input_json = request.get_json(force=True)
    return determiningService.pca(input_json)

#------------------------------------------------------------------------------------------------------------------------------------------------------------

#------------------------------------------------------------------------------
# 3.1
# Return C#/.NET type => ''
#------------------------------------------------------------------------------
@app.route('/api/Determining/UnpopularPastime/Task1', methods=['POST'])
def determining_unpopular_pastime_task1():
    input_json = request.get_json(force=True)
    return determiningService.determining_unpopular_pastime_task1(input_json)