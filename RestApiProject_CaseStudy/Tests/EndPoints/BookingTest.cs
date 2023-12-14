using Newtonsoft.Json;
using RestApiProject_CaseStudy.Models;
using RestApiProject_CaseStudy.Utilities;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestApiProject_CaseStudy.Tests.EndPoints
{
    [TestFixture]
    internal class BookingTest:CoreCodes
    {
        [Test, Order(1)]
        public void GetSingleBooking()
        {
            test = extent.CreateTest("Get single Booking Test");
            Log.Information("Get Single Booking Test started");
            var request = new RestRequest("booking/34", Method.Get);
            request.AddHeader("Accept", "application/json");

            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
               
                Log.Information($"API response: {response.Content}");
                var bookingDetails = JsonConvert.DeserializeObject<Booking>(response.Content);

                Assert.NotNull(bookingDetails);
                Log.Information("Returned booking Information");
                Assert.That(bookingDetails.FirstName,Is.Not.Empty);
                Log.Information("FirstName is not empty");
                Assert.IsNotEmpty(bookingDetails.LastName.ToString());
                Log.Information("LastName is not empty");
                Log.Information("Get single booking test passed");
                test.Pass("Test Passed all asserts");
            }
            catch (AssertionException ex)
            {
                test.Fail("Get single booking test failed");
                Log.Information($"{ex.Message}");
                Log.Information("Get single user test failed");
            }

        }
        [Test, Order(2)]
        public void CreateUser()
        {
            test = extent.CreateTest("Create booking Test");
            Log.Information("Create booking Test started");
            var createBookingRequest = new RestRequest("booking", Method.Post);
            createBookingRequest.AddHeader("Content-Type", "application/json");
            createBookingRequest.AddHeader("Accept", "application/json");
            createBookingRequest.AddJsonBody(new {
                firstname = "gokul",
                lastname = "Raj",
                totalprice = 111,
                depositpaid = true,
                bookingdates = new{
                checkin = "2018-01-01",
                checkout = "2019-01-01"
                },
                additionalneeds = "Breakfast" });

            var createBookingResponse = client.Execute(createBookingRequest);
            try
            {
                Assert.That(createBookingResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("Created booking Successfully");
                var bookingData = JsonConvert.DeserializeObject<Booking>(createBookingResponse.Content);
                Assert.NotNull(bookingData);
                Log.Information("Created and returned the booking");
                Assert.That(bookingData.FirstName, Is.Not.Empty);
                Log.Information("FirstName is not empty");
                Log.Information("Create booking Test Passed");
                test.Pass("Create User Test Passed all asserts");
            }
            catch (AssertionException ex)
            {
                test.Fail("Create Booking Test- Failed");
                Log.Information($"{ex.Message}");
                Log.Information("Create Booking Test- Failed");
            }
        }
        [Test]
        public void UpdateUserTest()
        {
            test = extent.CreateTest("GetToken Test");
            var request = new RestRequest("/auth", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                username = "admin",
                password = "password123"
            });
            try
            {
                var response = client.Execute(request);

                var token = JsonConvert.DeserializeObject<Cookies>(response.Content);
                var requestput = new RestRequest("/booking/13", Method.Put);
                requestput.AddHeader("Content-Type", "application/json");
                requestput.AddHeader("Accept", "application/json");
                requestput.AddHeader("Cookie", "token=" + token.Token);


                requestput.AddJsonBody(new
                {
                    firstname = "John",
                    lastname = "Smith",
                    totalprice = 111,
                    depositpaid = true,
                    bookingdates = new
                    {
                        checkin = "2018-01-01",
                        checkout = "2019-01-01"
                    },
                    additionalneeds = "Breakfast"
                });
                var responseput = client.Execute(requestput);
                Assert.That(responseput.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Status code is not 200");
                Console.WriteLine(responseput.Content);
            }
            catch (AssertionException ex) {


            }

        }
        [Test]
        public void DeleteUserTest()
        {

            test = extent.CreateTest("Delete Booking test");
            ;
            var requestAuth = new RestRequest("/auth", Method.Post);
            requestAuth.AddHeader("Content-Type", "application/json");
            requestAuth.AddJsonBody(new
            {
                username = "admin",
                password = "password123"
            });
            var request = new RestRequest("/booking/11", Method.Delete);
            try
            {
                var responseAuth = client.Execute(requestAuth);
                var token = JsonConvert.DeserializeObject<Cookies>(responseAuth.Content);
                request.AddHeader("Cookie", "token=" + token.Token);
                var response = client.Execute(request);
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created), "Status code is not 200");
                test.Pass("Status code test pass");
                Log.Information("Status code test passed");
                test.Pass("Booking id data test pass");
                Log.Information("Booking id data test passed");
            }
            catch (AssertionException ex)
            {
                string message = ex.Message;
                Log.Error(message);
                test.Fail(message + " Get All Booking id  Fail");
            }
        }
    }
}
