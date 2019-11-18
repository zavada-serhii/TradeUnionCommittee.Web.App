class Significance:

    FirstCriterion = ''
    SecondCriterion = ''
    TCriteria = 0,
    TStatistics = 0
    Differs = False

    def __init__(self,FirstCriterion,SecondCriterion,TCriteria,TStatistics,Differs):
        self.FirstCriterion = FirstCriterion
        self.SecondCriterion = SecondCriterion
        self.TCriteria = TCriteria
        self.TStatistics = TStatistics
        self.Differs = Differs