using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task get_basic()
        {
            var result = await lib.shared.http.getJSONAsync<string>("http://httpbin.org/ip");
            
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result));
        }


        [TestMethod]
        public async Task get_basic_DecodeResponse()
        {
            var result = await lib.shared.http.getJSONAsync<model.HttpBin_IP_ResponseType>("http://httpbin.org/ip");
            
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result.origin));
        }


        [TestMethod]
        public async Task post_basic()
        {
            var result = await lib.shared.http.postJSONAsync<string>("http://httpbin.org/post",
                data: new
                {
                    Param1 = "Apple",
                    Param2 = "Orange"
                });
            
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result));
        }




        [TestMethod]
        public async Task get_baseAddressSet()
        {
            var result = await lib.shared.http_HttpBin.getJSONAsync<model.HttpBin_IP_ResponseType>("ip");
            
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result.origin));
        }


        [TestMethod]
        public async Task testShortTimeout()
        {
            var http = lib.httpFactory.create(options =>
            {
                options.baseAddress = "http://httpbin.org/";
                options.Timeout = new TimeSpan(0, 0, 0, 0, 20);
            });

            try
            {
                var result = await http.getJSONAsync<string>("ip");
                Assert.Fail("Exception should have been thrown because of timeout");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("Curl failure") && 
                              ex.Message.EndsWith("OPERATION_TIMEDOUT"));
            }
            
        }
        
    }
}
