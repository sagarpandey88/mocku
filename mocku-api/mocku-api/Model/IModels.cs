using System;
using System.Collections.Generic;
using System.Text;

namespace Mocku.API.Model
{
    public interface IEndPointData
    {
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
        //public string[] ResponseHeaders { get; set; }
    }



    public interface IExpectedResponse
    {
        public object ResponseBody { get; set; }
        public string ResponseHeaders { get; set; }
        public string ResponseType { get; set; }

        public string ResponseStatus { get; set; }

        public string ContentType { get; set; }
    }



}
