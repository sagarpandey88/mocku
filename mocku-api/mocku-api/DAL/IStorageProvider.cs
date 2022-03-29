using Mocku.API.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mocku.API.DAL
{
    public interface IStorageProvider
    {
        /// <summary>
        /// Connection String of the Storage Provider
        /// </summary>
        public string ConnectionString { get; set; }        


        /// <summary>
        /// Inserts the endpoint Data
        /// </summary>
        /// <param name="endpointData">Endpoint information</param>
        /// <returns>Result of the transaction , true = Success , false = failure</returns>
        bool InsertEndPointData(IEndPointData endpointData);

        bool UpdateEndPointData(IEndPointData endpointData);

        IEndPointData GetEndPointData(string org, string app, string endpPoint);

        List<IEndPointData> GetEndPoints(string org);
        List<IEndPointData> GetEndPoints(string org, string app);

        bool DeleteEndPointData(string org, string app, string endpPoint);

        bool DeleteEndPointData(string endPointId);

    }

}
