using RestSharp;
using System.Net;
using System.Threading;

namespace SpecFlow_01.StepDefinitions
{
    [Binding]
    public class Feature1StepDefinitions
    {
        [Given(@"reqres get test api")]
        public void GivenReqresGetTestApi()
        {
            Console.WriteLine("log.given");
        }

        [When(@"I request the get from api")]
        public async Task WhenIRequestTheGetFromApiAsync()
        {
            string endpoint = "https://reqres.in/api";
            var client = new RestClient(endpoint);
            var request = new RestRequest("users/1", (Method)DataFormat.Json);
            var response = await client.GetAsync(request);
            Console.WriteLine("log.response" + response);
        }

        [Then(@"response status should be (.*)")]
        public void ThenResponseStatusShouldBe(int p0)
        {
            //throw new PendingStepException();
            Console.WriteLine("given");
        }
    }
}
