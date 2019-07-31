from controllers import app
from flask import Flask, request, jsonify, json
import services.analyticalService as service

@app.route('/', methods=['GET'])
@app.route('/api/analytical/healtcheck', methods=['GET'])
def healt_check():
    return "Trade Union Committee Data Analysis API => Works", 200, {'ContentType':'application/json'} 

@app.route('/api/analytical/post', methods=['POST'])
def test_post():
    input_json = request.get_json(force=True)
    return service.test(input_json)