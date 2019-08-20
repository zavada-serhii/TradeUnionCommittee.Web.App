from controllers import app
from flask import Flask, request, jsonify, json
import services.testService as service

@app.route('/', methods=['GET'])
@app.route('/api/test/healtcheck', methods=['GET'])
def healt_check():
    return "Trade Union Committee Data Analysis API => Works", 200, {'ContentType':'application/json'} 

@app.route('/api/test/post', methods=['POST'])
def test_post():
    input_json = request.get_json(force=True)
    return service.test(input_json)