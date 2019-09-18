from controllers import app
from flask import Flask, request, jsonify, json
import services.HomeService as service

@app.route('/', methods=['GET'])
@app.route('/api/home/healtcheck', methods=['GET'])
def healt_check():
    return "Trade Union Committee Data Analysis API => Works", 200, {'ContentType':'application/json'} 

@app.route('/api/home/postjson', methods=['POST'])
def test_post_json():
    input_json = request.get_json(force=True)
    return service.test_json(input_json)

@app.route('/api/home/postcsv', methods=['POST'])
def test_post_csv():
    input_csv = request.get_json(force=True)
    return service.test_csv(input_csv)