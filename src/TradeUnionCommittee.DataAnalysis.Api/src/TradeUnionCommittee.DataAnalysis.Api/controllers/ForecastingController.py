from controllers import app
from flask import Flask, request, jsonify, json
import services.ForecastingService as service

#------------------------------------------------------------------------------
# 1.1 - 1.2
# Return C#/.NET => 'List<List<double>>'
#------------------------------------------------------------------------------
@app.route('/api/Forecasting/ActualingTrips/Task1', methods=['POST'])
def forecasting_actualing_trips_task1():
    input_json = request.get_json(force=True)
    return service.correlation_analysis(input_json)

#------------------------------------------------------------------------------
# 1.3
# Return C#/.NET type => 'String'
#------------------------------------------------------------------------------
@app.route('/api/Forecasting/ActualingTrips/Task3', methods=['POST'])
def forecasting_actualing_trips_task3():
    input_json = request.get_json(force=True)
    return service.checking_significance_coefficients(input_json)

#------------------------------------------------------------------------------
# 1.4
# Return C#/.NET type => 'String'
#------------------------------------------------------------------------------
@app.route('/api/Forecasting/ActualingTrips/Task4', methods=['POST'])
def forecasting_actualing_trips_task4():
    input_json = request.get_json(force=True)
    return service.cluster_analysis(input_json)