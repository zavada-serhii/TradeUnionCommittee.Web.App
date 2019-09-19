from flask import Flask
app = Flask(__name__)

import Controllers.HomeController
import Controllers.ForecastingController
import Controllers.DeterminingController
import Controllers.OptimizationController
import Controllers.CheckingController