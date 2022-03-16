using Nancy.Json;
using NUnit.Framework;
using System.IO;
using System.Net;

namespace StackOverflowTest
{
    public class Tests
    {
        public class TagsApiTest
        {
            public class Tag
            {
                public int id { get; set; }
                public string? name { get; set; }
                public string? questionTags { get; set; }
            }

            private Tag? _tag;

            [Test, Order(1)]
            public void Get()
            {
                WebRequest request = WebRequest.Create("https://localhost:7001/api/tags");
                request.Method = "GET";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

                response.Close();
            }

            [Test, Order(2)]
            public void Post()
            {
                WebRequest request = WebRequest.Create("https://localhost:7001/api/tags");
                request.Method = "POST";
                request.ContentType = "application/json";

                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
                requestWriter.Write("\"testTag\"");
                requestWriter.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

                StreamReader responseReader = new StreamReader(response.GetResponseStream());

                _tag = new JavaScriptSerializer().Deserialize<Tag>(responseReader.ReadToEnd());

                Assert.AreEqual(_tag.name, "testTag");

                responseReader.Close();
                response.Close();
            }

            [Test, Order(3)]
            public void GetById()
            {
                WebRequest request = WebRequest.Create("https://localhost:7001/api/tags/" + _tag.id);
                request.Method = "GET";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

                response.Close();
            }

            [Test, Order(4)]
            public void Put()
            {
                WebRequest request = WebRequest.Create("https://localhost:7001/api/tags/" + _tag.id);
                request.Method = "PUT";
                request.ContentType = "application/json";

                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
                requestWriter.Write("\"testTagNew\"");
                requestWriter.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

                StreamReader responseReader = new StreamReader(response.GetResponseStream());

                _tag = new JavaScriptSerializer().Deserialize<Tag>(responseReader.ReadToEnd());

                Assert.AreEqual(_tag.name, "testTagNew");

                responseReader.Close();
                response.Close();
            }

            [Test, Order(5)]
            public void Delete()
            {
                WebRequest request = WebRequest.Create("https://localhost:7001/api/tags/" + _tag.id);
                request.Method = "DELETE";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

                response.Close();
            }
        }
    }
}