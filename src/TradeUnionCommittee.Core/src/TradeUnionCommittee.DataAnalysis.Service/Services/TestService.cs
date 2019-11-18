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

        #region Run tests

        public Dictionary<string, bool> RunAllTasks()
        {
            var allData = new Dictionary<string, bool>();

            var data = new List<Dictionary<string, bool>>
            {
                TestTasks_3_4_5()
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

        #endregion

        #region Task 3, 4, 5

        public Dictionary<string, bool> TestTasks_3_4_5()
        {
            var random = new Random();
            var testData = new List<object>();
            var result = new Dictionary<string, bool>();
            var taskNumber = 3;

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
                result.Add($"Task {taskNumber} | {action}", response.StatusCode == HttpStatusCode.OK);

                taskNumber++;
            }

            return result;
        }

        #endregion
    }
}