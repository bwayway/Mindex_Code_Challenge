using challenge.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using code_challenge.Tests.Integration.Extensions;
using System.Net;
using System.Net.Http;
using code_challenge.Tests.Integration.Helpers;
using System.Text;

namespace code_challenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestServerStartup>()
                .UseEnvironment("Development"));

            _httpClient = _testServer.CreateClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void PostAndGetCompensationByEmployeeId_Ok()
        {
            //Arrange
            Compensation compensationRequest = new Compensation()
            {
                //Employee ID for John Lennon
                EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                Salary = "100000",
                EffectiveDate = "02/24/1960"
            };

            //Post request
            //Act
            //jsonify the compensation object
            var requestContent = new JsonSerialization().ToJson(compensationRequest);

            //Post the compensation to the databse
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var postResponse = postRequestTask.Result;


            //Assert
            //Confirm the request was created 
            Assert.AreEqual(HttpStatusCode.Created, postResponse.StatusCode);



            //Get Request
            //Act
            //Retrieve the compensation from the database via a Get request
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{compensationRequest.EmployeeId}");
            var getResponse = getRequestTask.Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode);
            //deserialize the compensation request
            var compensationResponse = getResponse.DeserializeContent<Compensation>();

            //Make sure all the fields Posted, match the fields retrieved from the database (minus compensationID as that is generated)
            Assert.AreEqual(compensationRequest.EmployeeId, compensationResponse.EmployeeId);
            Assert.AreEqual(compensationRequest.Salary, compensationResponse.Salary);
            Assert.AreEqual(compensationRequest.EffectiveDate, compensationResponse.EffectiveDate);
        }


        // Task 2 - Test what happens if the employee Id is not found in the compensation database
        [TestMethod]
        public void GetCompensationByEmployeeId_NotFound()
        {
            //Arrange
            string employeeId = "000000000-way3-4526-00aa-902384092384f";

            //Act
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
            var response = getRequestTask.Result;

            //Assert
            //Confirm that a "Not Found" return code is received
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}