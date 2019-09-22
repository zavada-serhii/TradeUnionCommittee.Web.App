using RestSharp;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Net;
using TradeUnionCommittee.DataAnalysis.Service.Interfaces;
using TradeUnionCommittee.DataAnalysis.Service.Models;

namespace TradeUnionCommittee.DataAnalysis.Service.Services
{
    public class TestService : ITestService
    {
        private readonly DataAnalysisClient _client;

        public TestService(DataAnalysisClient client)
        {
            _client = client;
        }

        #region Test Actions

        public bool HealthCheck()
        {
            var request = new RestRequest("api/Home/HealtCheck", Method.GET);
            var response = _client.Execute(request);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public IEnumerable<TestModel> TestPostJson()
        {
            var request = new RestRequest("api/Home/PostJson", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(GetTestData);

            var response = _client.Execute(request);
            return response.StatusCode == HttpStatusCode.OK 
                ? JsonSerializer.DeserializeFromString<List<TestModel>>(response.Content)
                : null;
        }

        public string TestPostCsv()
        {
            var request = new RestRequest("api/Home/PostCsv", Method.POST) { RequestFormat = DataFormat.Json };
            var csv = CsvSerializer.SerializeToString(GetTestData);
            request.AddBody(csv);

            var response = _client.Execute(request);
            return response.StatusCode == HttpStatusCode.OK
                ? response.Content
                : null;
        }

        private IEnumerable<TestModel> GetTestData => new List<TestModel>
        {
            new TestModel
            {
                Id = 1,
                FullName = "Gabriel Logan",
                Email = "gabriel.logan@test.com"
            },
            new TestModel
            {
                Id = 2,
                FullName = "Annabelle Stafford",
                Email = "annabelle.stafford@test.com"
            },
            new TestModel
            {
                Id = 35,
                FullName = "Joseph Rogers",
                Email = "joseph.rogers@test.com"
            },
            new TestModel
            {
                Id = 48,
                FullName = "Everett Wood",
                Email = "everett.wood@test.com"
            },
            new TestModel
            {
                Id = 57,
                FullName = "Vivien Jordan",
                Email = "everett.wood@test.com"
            }
        };

        #endregion

        //--------------------------------------------------------------------------------

        #region Task 1

        public Dictionary<string, bool> TestTask_1()
        {
            var allData = new Dictionary<string, bool>();

            var data = new List<Dictionary<string, bool>>
            {
                TestTask_11_12_13(),
                TestTask_14()
            };

            foreach (var tmp in data)
            {
                foreach (var (key, value) in tmp)
                {
                    allData.Add(key, value);
                }
            }

            return allData;
        }

        private Dictionary<string, bool> TestTask_11_12_13()
        {
            var random = new Random();
            var testData = new List<object>();
            var result = new Dictionary<string, bool>();

            IEnumerable<string> actions = new List<string>
            {
                "api/Forecasting/ActualingTrips/Task1",
                "api/Forecasting/ActualingTrips/Task3"
            };

            for (var i = 0; i < 1000; i++)
            {
                testData.Add(new
                {
                    Age = random.Next(25, 95),
                    Travel_Count = random.Next(0, 10),
                    Wellness_Count = random.Next(0, 10),
                    Tour_Count = random.Next(0, 10)
                });
            }

            foreach (var action in actions)
            {
                var request = new RestRequest(action, Method.POST) { RequestFormat = DataFormat.Json };
                var csv = CsvSerializer.SerializeToString(testData);
                request.AddBody(csv);

                var response = _client.Execute(request);
                result.Add(action, response.StatusCode == HttpStatusCode.OK);
            }

            return result;
        }

        private Dictionary<string, bool> TestTask_14()
        {
            var random = new Random();
            var csvData = new List<object>();
            const string resource = "api/Forecasting/ActualingTrips/Task4";

            for (var i = 0; i < 1000; i++)
            {
                csvData.Add(new
                {
                    X = random.Next(0, 15),
                    Y = random.Next(0, 15)
                });
            }

            var json = JsonSerializer.SerializeToString(new
            {
                Csv = CsvSerializer.SerializeToString(csvData),
                CountCluster = 4
            });

            var request = new RestRequest(resource, Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(json);

            var response = _client.Execute(request);
            return new Dictionary<string, bool> { { resource, response.StatusCode == HttpStatusCode.OK } };
        }

        #endregion

        #region Task 2

        public Dictionary<string, bool> TestTask_2()
        {
            var allData = new Dictionary<string, bool>();

            var data = new List<Dictionary<string, bool>>
            {
                TestTask_21_22_23_24(),
                TestTask_25(),
                TestTask_26()
            };

            foreach (var tmp in data)
            {
                foreach (var (key, value) in tmp)
                {
                    allData.Add(key, value);
                }
            }

            return allData;
        }

        private Dictionary<string, bool> TestTask_21_22_23_24()
        {
            var random = new Random();
            var testData = new List<object>();
            var result = new Dictionary<string, bool>();

            IEnumerable<string> actions = new List<string>
            {
                "api/Determining/ProbablePastime/Task1",
                "api/Determining/ProbablePastime/Task2"
            };

            for (var i = 0; i < 1000; i++)
            {
                testData.Add(new
                {
                    Y = random.Next(0, 10),  //Travel_Count
                    X1 = random.Next(0, 11), //Wellness_Count
                    X2 = random.Next(0, 12), //Tour_Count
                    X3 = random.Next(0, 13), //MaterialAid_ONU
                    X4 = random.Next(0, 14), //Award_ONU
                    X5 = random.Next(0, 15), //Children_Count
                    X6 = random.Next(0, 16)  //GrandChildren_Count
                });
            }

            foreach (var action in actions)
            {
                var request = new RestRequest(action, Method.POST) { RequestFormat = DataFormat.Json };
                var csv = CsvSerializer.SerializeToString(testData);
                request.AddBody(csv);

                var response = _client.Execute(request);
                result.Add(action, response.StatusCode == HttpStatusCode.OK);
            }

            return result;
        }

        private Dictionary<string, bool> TestTask_25()
        {
            var random = new Random();
            var csvData = new List<object>();
            const string resource = "api/Determining/ProbablePastime/Task5";

            for (var i = 0; i < 1000; i++)
            {
                csvData.Add(new
                {
                    X1 = random.Next(0, 11), //Travel_Count
                    X2 = random.Next(0, 12), //Wellness_Count
                    X3 = random.Next(0, 13), //Tour_Count
                    X4 = random.Next(0, 4), //CountChildren
                    X5 = random.Next(0, 3), //CountGrandChildren
                });
            }

            var json = JsonSerializer.SerializeToString(new
            {
                Csv = CsvSerializer.SerializeToString(csvData),
                CountComponents = 2
            });

            var request = new RestRequest(resource, Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(json);

            var response = _client.Execute(request);
            return new Dictionary<string, bool> { { resource, response.StatusCode == HttpStatusCode.OK } };
        }

        private Dictionary<string, bool> TestTask_26()
        {
            var random = new Random();
            var csvData = new List<object>();
            const string resource = "api/Determining/ProbablePastime/Task6";

            for (var i = 0; i < 1000; i++)
            {
                csvData.Add(new
                {
                    X = random.Next(0, 11), //Travel_Count
                    Y = random.Next(0, 4), // Count_Children
                });
            }

            var json = JsonSerializer.SerializeToString(new
            {
                Csv = CsvSerializer.SerializeToString(csvData),
                CountCluster = 3
            });

            var request = new RestRequest(resource, Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(json);

            var response = _client.Execute(request);
            return new Dictionary<string, bool> { { resource, response.StatusCode == HttpStatusCode.OK } };
        }

        #endregion

        #region Task 3, 4, 5

        public Dictionary<string, bool> TestTasks_3_4_5()
        {
            var random = new Random();
            var testData = new List<object>();
            var result = new Dictionary<string, bool>();

            IEnumerable<string> actions = new List<string>
            {
                "api/Determining/UnpopularPastime/Task1",
                "api/Optimization/Premiums/Task1",
                "api/Checking/RelevanceWellnessTrips/Task1",
            };

            for (var i = 0; i < 1000; i++)
            {
                testData.Add(new
                {
                    Age = random.Next(25, 95),
                    Travel_Count = random.Next(0, 10),
                    Wellness_Count = random.Next(0, 10),
                    Tour_Count = random.Next(0, 10)
                });
            }

            foreach (var action in actions)
            {
                var request = new RestRequest(action, Method.POST) { RequestFormat = DataFormat.Json };
                var csv = CsvSerializer.SerializeToString(testData);
                request.AddBody(csv);

                var response = _client.Execute(request);
                result.Add(action, response.StatusCode == HttpStatusCode.OK);
            }

            return result;
        }

        #endregion
    }
}