using challenge.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using code_challenge.Tests.Integration.Extensions;
using System.Net;
using System.Net.Http;

namespace code_challenge.Tests.Integration
{
    [TestClass]
    public class ReportingStructureControllerTests
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

        //Task 1 Test when the employee does have direct reports
        [TestMethod]
        public void GetDirectReportsWithDirectReports_Returns_Ok()
        {
            //Arrange
            //Employee ID for Ringo Starr
            string employeeId = "03aa1462-ffa9-4978-901b-7c001562cf6f";

            //Act
            var getRequestTask = _httpClient.GetAsync($"api/reporting-structure/{employeeId}");
            var response = getRequestTask.Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var directReports = response.DeserializeContent<ReportingStructure>();
            //Based on EmployeeSeedData.json, Ring Starr should have 2 direct reports
            Assert.AreEqual(2, directReports.NumberOfReports);
        }

        public void GetDirectReportsWithNestedDirectReports_Returns_Ok()
        {
            //Arrange
            //Employee ID for Ringo Starr
            string employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";

            //Act
            var getRequestTask = _httpClient.GetAsync($"api/reporting-structure/{employeeId}");
            var response = getRequestTask.Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var directReports = response.DeserializeContent<ReportingStructure>();
            //Based on EmployeeSeedData.json, Ring Starr should have 2 direct reports
            Assert.AreEqual(4, directReports.NumberOfReports);
        }


        //Task 1 Test when employee does NOT have direct reports
        [TestMethod]
        public void GetDirectReportsWithoutDirectReports_Returns_Ok()
        {
            //Arrange
            //Employee ID For Paul McCartney
            string employeeId = "b7839309-3348-463b-a7e3-5de1c168beb3";

            //Act
            var getRequestTask = _httpClient.GetAsync($"api/reporting-structure/{employeeId}");
            var response = getRequestTask.Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var directReports = response.DeserializeContent<ReportingStructure>();
            //Based on EmployeeSeedData.json, Paul McCartney should have 0 direct reports
            Assert.AreEqual(0, directReports.NumberOfReports);

        }

        //Task 1 Test When employee does not exist in the database
        [TestMethod]
        public void GetDirectReports_Returns_NotFound()
        {
            //Arrange
            //random employee ID
            string employeeId = "000000000-way3-4526-00aa-902384092384f";

            //Act
            var getRequestTask = _httpClient.GetAsync($"api/reporting-structure/{employeeId}");
            var response = getRequestTask.Result;

            //Assert
            //Confirm that a "Not Found" return code is received
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
