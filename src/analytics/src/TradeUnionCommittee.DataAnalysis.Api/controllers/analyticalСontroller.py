from controllers import app
from flask import Flask, request, jsonify, json
import services.analyticalService as nnn

@app.route('/', methods=['GET'])
@app.route('/api/analytical/get', methods=['GET'])
def test_get():
    return "Hello i am Trade Union Committee Data Analysis API";

@app.route('/api/analytical/post', methods=['POST'])
def test_post():
    input_json = request.get_json(force=True)
    return nnn.hello(input_json)