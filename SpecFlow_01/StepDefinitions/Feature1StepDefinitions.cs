using Newtonsoft.Json;
using RestSharp;
using System.Text.Json;

namespace SpecFlow_01.StepDefinitions
{
    [Binding]
    public class Feature1StepDefinitions
    {
        public string? Id { get; private set; }

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
                var endpoint = "https://reqres.in/api";
                var client = new RestClient(endpoint);
                var request = new RestRequest("users/1", Method.Get);
                request.Timeout = 3000;
                request.RequestFormat = DataFormat.Json;
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                var response = await client.ExecuteGetAsync(request);

                dynamic resp = Newtonsoft.Json.Linq.JObject.Parse(response.Content);
                Id = resp.data.id;
                
                Console.WriteLine("log.response => " + response.Content);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exeption: " + e);
            }
        }

        [Then(@"response status should be (.*)")]
        public void ThenResponseStatusShouldBe(int p0)
        {
            //throw new PendingStepException();
            Console.WriteLine("given\n");
            Console.WriteLine("Id = " + Id);
        }

        private interface IRestResponse
        {
        }
    }
}
