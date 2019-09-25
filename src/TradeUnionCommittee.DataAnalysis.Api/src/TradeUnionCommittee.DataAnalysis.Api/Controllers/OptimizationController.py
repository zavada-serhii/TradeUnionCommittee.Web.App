from Controllers import app
from flask import Flask, request, jsonify, json
import Services.OptimizationService as service

#------------------------------------------------------------------------------
# 4.1
# Return C#/.NET type => ''
#------------------------------------------------------------------------------
@app.route('/api/Optimization/Premiums/Task1', methods=['POST'])
def optimization_premiums_task1():
    input_json = request.get_json(force=True)
    return service.optimization_premiums_task1(input_json)