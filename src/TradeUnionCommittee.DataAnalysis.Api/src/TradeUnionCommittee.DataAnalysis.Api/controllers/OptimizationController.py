from controllers import app
from flask import Flask, request, jsonify, json
import services.OptimizationService as service

@app.route('/api/Optimization/Premiums/Task1', methods=['POST'])
def optimization_premiums_task1():
    input_json = request.get_json(force=True)
    return input_json