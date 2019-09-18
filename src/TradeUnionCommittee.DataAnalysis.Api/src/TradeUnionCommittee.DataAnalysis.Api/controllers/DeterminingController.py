from controllers import app
from flask import Flask, request, jsonify, json
import services.DeterminingService as service

@app.route('/api/Determining/ProbablePastime/Task1', methods=['POST'])
def determining_probable_pastime_task1():
    input_json = request.get_json(force=True)
    return input_json

@app.route('/api/Determining/UnpopularPastime/Task1', methods=['POST'])
def determining_unpopular_pastime_task1():
    input_json = request.get_json(force=True)
    return input_json