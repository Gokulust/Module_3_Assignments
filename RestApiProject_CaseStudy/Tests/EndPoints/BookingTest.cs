using Newtonsoft.Json;
using RestApiProject_CaseStudy.Models;
using RestApiProject_CaseStudy.Utilities;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
