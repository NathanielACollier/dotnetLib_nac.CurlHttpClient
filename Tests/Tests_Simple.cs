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
        
        
        
    }
}
