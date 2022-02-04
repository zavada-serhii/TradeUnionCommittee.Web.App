using System.Text.Json;
using TradeUnionCommittee.DataAnalysis.Service.Contracts;
using TradeUnionCommittee.DataAnalysis.Service.Exceptions;
using TradeUnionCommittee.DataAnalysis.Service.Models;

namespace TradeUnionCommittee.DataAnalysis.Service.Services
{
    public class TestService : ITestService
    {
        private readonly HttpClient _dataAnalysisClient;

        public TestService(IHttpClientFactory clientFactory)
        {
            _dataAnalysisClient = clientFactory.CreateClient("DataAnalysis");
        }

        public async Task<IEnumerable<TestResponseModel>> PostJson()
        {
            HttpRequestMessage requestMessage = new()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_dataAnalysisClient.BaseAddress, "api/Home/PostJson"),
                Content = new StringContent(JsonSerializer.Serialize(TestData))
            };

            HttpResponseMessage response = await _dataAnalysisClient.SendAsync(requestMessage, CancellationToken.None);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<TestResponseModel>>(responseString);
            }
            else
            {
                var responseString = await response.Content.ReadAsStringAsync();
                throw new AnalysisServiceUnavailableException(responseString);
            }
        }

        public async Task<string> PostCsv()
        {
            var body = JsonSerializer.Serialize(ServiceStack.Text.CsvSerializer.SerializeToString(TestData));

            HttpRequestMessage requestMessage = new()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_dataAnalysisClient.BaseAddress, "api/Home/PostCsv"),
                Content = new StringContent(body)
            };

            HttpResponseMessage response = await _dataAnalysisClient.SendAsync(requestMessage, CancellationToken.None);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                var responseString = await response.Content.ReadAsStringAsync();
                throw new AnalysisServiceUnavailableException(responseString);
            }
        }

        private static IEnumerable<TestModel> TestData => new List<TestModel>
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
    }
}