class MultiFactor:
    RegressionModel = []
    Standardization = []
    SignificanceTest = []
    ConfidenceInterval = []

    def __init__(self,RegressionModel,Standardization,SignificanceTest,ConfidenceInterval):
        self.RegressionModel = RegressionModel
        self.Standardization = Standardization
        self.SignificanceTest = SignificanceTest
        self.ConfidenceInterval = ConfidenceInterval