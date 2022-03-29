using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mocku.API.Model
{
    /// <summary>
    /// Endpoint data entity used for Azure Storage
    /// </summary>
    public class EndPointDataEntity : ITableEntity, IEndPointData
    {


        /// <summary>
        /// Organization + App Name will be the Partion key. 
        /// </summary>
        public string PartitionKey { get; set; }
        /// <summary>
        /// Hash of EndPointName + RequestBody + headers + Request Type
        /// </summary>
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public string Organization { get; set; }
        public string AppName { get; set; }

        public string EndPoint { get; set; }

        //Request
        public string RequestBody { get; set; }

        public string RequestQueryString { get; set; }

        public string RequestHeaders { get; set; }

        public string RequestType { get; set; }


        //Response
        public string ResponseBody { get; set; }
        public string ResponseContentType { get; set; }



        public static string GetPartionKey(string org, string app)
        {
            return org + app;
        }

        public static string GetRowKey(string endPoint)
        {
            //maybe hash the data
            return endPoint;
        }

    }


    /// <summary>
    /// EndPoint data used in business layer
    /// </summary>
    public class EndPointData : IEndPointData
    {
        public string Organization { get; set; }
        public string AppName { get; set; }
        public string EndPoint { get; set; }
        public string RequestBody { get; set; }

        public string RequestQueryString { get; set; }

        public string RequestHeaders { get; set; }
        public string RequestType { get; set; }


        //response
        public string ResponseBody { get; set; }
        public string ResponseContentType { get; set; }
    }



    public class EndPointResponse : IExpectedResponse
    {
        public object ResponseBody { get; set; }
        public string ResponseHeaders { get; set; }
        public string ResponseType { get; set; }

        public string ResponseStatus { get; set; }

        public string ContentType { get; set; }

    }


}
