from Controllers import app
import os
from flask import Flask, request, send_from_directory, make_response, jsonify, json
import Services.HomeService as service

@app.route('/', methods=['GET'])
def index():
    return "<html><head><title>Trade Union Committee – Data Analysis Service</title></head><body></br></br></br></br></br><center><h1>Trade Union Committee – Data Analysis Service – Works</h1></center></body></html>"

@app.route('/favicon.ico', methods=['GET'])
def favicon():
    root_path = os.path.abspath(app.root_path + "/../")
    favicon_path = os.path.join(root_path, 'static')
    return send_from_directory(favicon_path, 'favicon.ico', mimetype='image/x-icon')

@app.route('/health/live', methods=['GET'])
def healt_check():
    response = make_response('Healthy', 200)
    response.mimetype = "text/plain"
    return response

@app.route('/api/Home/PostJson', methods=['POST'])
def test_post_json():
    input_json = request.get_json(force=True)
    return service.test_json(input_json)

@app.route('/api/Home/PostCsv', methods=['POST'])
def test_post_csv():
    input_csv = request.get_json(force=True)
    return service.test_csv(input_csv)