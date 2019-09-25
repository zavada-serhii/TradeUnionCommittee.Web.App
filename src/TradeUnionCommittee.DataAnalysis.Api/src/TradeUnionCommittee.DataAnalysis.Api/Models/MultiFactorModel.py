class MultiFactor:
    RegressionModel = []
    Standartization = []
    SignificanceTest = []
    ConfidenceInterval = []

    def __init__(self,RegressionModel,Standartization,SignificanceTest,ConfidenceInterval):
        self.RegressionModel = RegressionModel
        self.Standartization = Standartization
        self.SignificanceTest = SignificanceTest
        self.ConfidenceInterval = ConfidenceInterval