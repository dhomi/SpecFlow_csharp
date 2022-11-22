using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace SpecFlow_01.StepDefinitions
{
    [Binding]
    public class Feature1StepDefinitions
    {
        public string? Id { get; private set; }
        public HttpStatusCode respStatus { get; private set; }

        [Given(@"reqres get test api")]
        public void GivenReqresGetTestApi()
        {
            Console.WriteLine("log.given");
        }

        [When(@"I request the get from api")]
        public async Task WhenIRequestTheGetFromApiAsync()
        {
            try
            {
                double time;
                var endpoint = "https://reqres.in/api";
                var client = new RestClient(endpoint);
                var request = new RestRequest("users/1", Method.Get);
                request.Timeout = 3000;
                request.RequestFormat = DataFormat.Json;
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

                Stopwatch sw = new Stopwatch();
                sw.Start();
                var response = await client.ExecuteGetAsync(request);
                sw.Stop();
                time = sw.Elapsed.TotalSeconds;
                Console.WriteLine("Elapsed={0}", time);

                dynamic resp = Newtonsoft.Json.Linq.JObject.Parse(response.Content);
                dynamic body = response.StatusCode;
                Id = resp.data.id;
                respStatus = body;

                Console.WriteLine("log.resp => " + resp);
                Console.WriteLine("log.respStatus => " + respStatus);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exeption: " + e);
            }
        }

        [Then(@"response status should be (.*)")]
        public void ThenResponseStatusShouldBe(string p0)
        {
            Console.WriteLine("Response shtatus \n");
            Console.WriteLine("Id = " + Id);
            Console.WriteLine("RespStatus = " + respStatus);
            Assert.AreEqual("1", Id);
            var statusString = respStatus.ToString();
            Assert.AreEqual(statusString, p0);
        }

        private interface IRestResponse
        {
        }
    }
}
