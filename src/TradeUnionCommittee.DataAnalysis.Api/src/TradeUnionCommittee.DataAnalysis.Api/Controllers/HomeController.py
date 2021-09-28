from Controllers import app
from flask import Flask, request, jsonify, json
import Services.HomeService as service

@app.route('/', methods=['GET'])
@app.route('/api/Home/HealtCheck', methods=['GET'])
def healt_check():
    return "<html><head><title>Trade Union Committee – Data Analysis Service</title></head><body></br></br></br></br></br><center><h1>Trade Union Committee – Data Analysis Service – Works</h1></center></body></html>" , 200, {'ContentType':'application/json'} 

@app.route('/api/Home/PostJson', methods=['POST'])
def test_post_json():
    input_json = request.get_json(force=True)
    return service.test_json(input_json)

@app.route('/api/Home/PostCsv', methods=['POST'])
def test_post_csv():
    input_csv = request.get_json(force=True)
    return service.test_csv(input_csv)