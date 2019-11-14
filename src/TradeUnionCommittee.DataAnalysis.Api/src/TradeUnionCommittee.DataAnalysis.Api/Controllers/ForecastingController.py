from Controllers import app
from flask import Flask, request, jsonify, json
import Services.ForecastingService as service

#------------------------------------------------------------------------------
# 1.1
#------------------------------------------------------------------------------
@app.route('/api/Forecasting/ActualingTrips/CorrelationAnalysis', methods=['POST'])
def forecasting_actualing_trips_task1():
    input_json = request.get_json(force=True)
    return service.correlation_analysis(input_json)

#------------------------------------------------------------------------------
# 1.2
#------------------------------------------------------------------------------
@app.route('/api/Forecasting/ActualingTrips/CheckingSignificanceCoefficients', methods=['POST'])
def forecasting_actualing_trips_task3():
    input_json = request.get_json(force=True)
    return service.checking_significance_coefficients(input_json)

#------------------------------------------------------------------------------
# 1.3
#------------------------------------------------------------------------------
@app.route('/api/Forecasting/ActualingTrips/ClusterAnalysis', methods=['POST'])
def forecasting_actualing_trips_task4():
    input_json = request.get_json(force=True)
    return service.cluster_analysis(input_json)