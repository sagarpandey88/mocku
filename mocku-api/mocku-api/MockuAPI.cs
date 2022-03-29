using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Mocku.API.BAL;

namespace Mocku.API
{
    public static class MockuAPI
    {
        [FunctionName("Mocks")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "MockAPI/{org:alpha}/{app:alpha}/{endpoint:alpha}")] HttpRequest req,
            //params here
            string org,
            string app,
            string endpoint,
            ILogger log)
        {
            log.LogInformation("Request for " + org + app + endpoint);

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;


            //req.GetHashCode()
            return GetMockAPI(org, app, endpoint, req.Method, requestBody, "HEADER HERE");
        }




        [FunctionName("InsertMockAPIData")]
        public static async Task<IActionResult> InsertMockAPIData(
          [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "InsertMockAPIData/{org:alpha}/{app:alpha}/{endpoint:alpha}")] HttpRequest req,
          //params here
          string org,
          string app,
          string endpoint,
          ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Model.EndPointResponse expectedResponse = JsonConvert.DeserializeObject<Model.EndPointResponse>(requestBody);


            EndpointBL endPointBL = new EndpointBL();

            endPointBL.InsertEndPointData(
                new Model.EndPointData
                {
                    Organization = org,
                    AppName = app,
                    EndPoint = endpoint,
                    RequestBody = requestBody,
                    RequestType = req.Method,
                    ResponseBody = JsonConvert.SerializeObject(expectedResponse.ResponseBody) //this comes from body of the request
                }
                );


            return new JsonResult(true);
        }




        [FunctionName("UpdateMockAPIData")]
        public static async Task<IActionResult> UpdateMockAPIData(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "UpdateMockAPIData/{org:alpha}/{app:alpha}/{endpoint:alpha}")] HttpRequest req,
        //params here
        string org,
        string app,
        string endpoint,
        ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Model.EndPointResponse expectedResponse = JsonConvert.DeserializeObject<Model.EndPointResponse>(requestBody);



            EndpointBL endPointBL = new EndpointBL();

            endPointBL.InsertEndPointData(
                new Model.EndPointData
                {
                    Organization = org,
                    AppName = app,
                    EndPoint = endpoint,
                    RequestBody = requestBody,
                    RequestType = req.Method,
                    ResponseBody = JsonConvert.SerializeObject(expectedResponse.ResponseBody) //this comes from body of the request
                }
                );


            return new JsonResult(true);
        }



        [FunctionName("DeleteMockAPIData")]
        public static async Task<IActionResult> DeleteMockAPIData(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "DeleteMockAPIData/{org:alpha}/{app:alpha}/{endpoint:alpha}")] HttpRequest req,
        //params here
        string org,
        string app,
        string endpoint,
        ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);


            EndpointBL endPointBL = new EndpointBL();

            endPointBL.DeleteEndPoint(new Model.EndPointData
            {
                Organization = org,
                AppName = app,
                EndPoint = endpoint

            });


            return new JsonResult(true);
        }





        private static JsonResult GetMockAPI(string org, string app, string endpoint, string requestType, string requestBody, string requestHeaders)
        {

            EndpointBL endPointBL = new EndpointBL();
            Model.IEndPointData endpointData = endPointBL.GetEndPoint(org, app, endpoint);



            return new JsonResult(JsonConvert.DeserializeObject(endpointData.ResponseBody));


        }

    }
}
