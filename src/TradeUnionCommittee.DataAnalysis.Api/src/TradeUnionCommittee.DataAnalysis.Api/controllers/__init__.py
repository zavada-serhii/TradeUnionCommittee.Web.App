from flask import Flask
app = Flask(__name__)

import controllers.HomeController
import controllers.ForecastingController
import controllers.DeterminingController
import controllers.OptimizationController
import controllers.CheckingController