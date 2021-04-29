# Curl HttpClient

+ This library will be an alternative to System.Net.Http.HttpClient.
+ Originaly started because of some things curl would do that newer dotnet would not.

## Examples

### GET
+ Basic GET Request looks like this
```c#
    var http = new nac.CurlHttpClient.HttpClient();
    var jsonStr = await http.getJSONAsync<string>("http://httpbin.org/ip");
```

### POST
+ Basic Post
```c#
    var http = new nac.CurlHttpClient.HttpClient();
    var result = await lib.shared.http.postJSONAsync<string>("http://httpbin.org/post",
        data: new
        {
            Param1 = "Apple",
            Param2 = "Orange"
        });
```