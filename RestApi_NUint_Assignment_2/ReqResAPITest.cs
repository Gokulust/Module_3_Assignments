using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RestExNuint
{
    [TestFixture]
    internal class ReqResAPITest
    {
        private RestClient client;
        private string baseUrl = "https://jsonplaceholder.typicode.com/";

        [SetUp]

        public void SetUp()
        {
            client = new RestClient(baseUrl);
        }
        [Test]
        public void GetSingleUser()
        {
            var Request = new RestRequest("posts/1", Method.Get);

            var Res = client.Execute(Request);
            Assert.That(Res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
        
            var postData= JsonConvert.DeserializeObject<PostData>(Res.Content);

            Assert.NotNull(postData);
            Console.WriteLine(postData);
            Assert.That(postData.Id, Is.EqualTo(1));
            Assert.IsNotEmpty(postData.Title);
            Console.WriteLine(postData.Body);
              

        }

        [Test]
        [Order(1)]
        public void CreatePost()
        {
            var request = new RestRequest("posts", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                title= "foo",
                body= "bar",
                userId= 1,
            });
            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));

            var post = JsonConvert.DeserializeObject<PostData>(response.Content);
            Assert.NotNull(post);
            Assert.IsNotNull(post.Title);
            
        }
        [Test]
        [Order(2)]
        public void UpdatePost()
        {
            var request = new RestRequest("posts/1", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                id= 1,
                title="foo",
                body= "bar",
                userId= 1,
            });

            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var user = JsonConvert.DeserializeObject<PostData>(response.Content);
            Assert.NotNull(user);
        }

        [Test]
        [Order(3)]
        public void DeletePost()
        {
            var request = new RestRequest("posts/2", Method.Delete);
            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
        }

        [Test]
        [Order(5)]
        public void GetNonExistingUser()
        {
            var request = new RestRequest("posts/999", Method.Get);
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
        }
    }
}
