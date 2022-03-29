using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using Mocku.API.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mocku.API.DAL
{
    public class AzureStorageRepository : IStorageProvider
    {
        public string ConnectionString { get; set; }

        /// <summary>
        /// Returns an instance of Storage provider with connection string from environment variable StorageConnectionString
        /// </summary>
        public AzureStorageRepository()
        {
            ConnectionString = Environment.GetEnvironmentVariable("StorageConnectionString");
        }

        /// <summary>
        /// Returns and instance of storage provider with connection string provided as parameter
        /// </summary>
        /// <param name="connectionString"></param>
        public AzureStorageRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }



        public EndPointDataEntity GetMockData(string org, string appName, string endPoint)
        {

            TableClient tc = GetTableClient();
            EndPointDataEntity ed = tc.GetEntity<EndPointDataEntity>(org + appName, endPoint).Value;

            return ed;

        }


        protected TableClient GetTableClient()
        {

            string connectionString = System.Environment.GetEnvironmentVariable("EndPointStorage", EnvironmentVariableTarget.Process);
            string tableName = "EndPointTable";

            var serviceClient = new TableServiceClient(connectionString);

            try
            {
                serviceClient.CreateTableIfNotExists(tableName);
                return serviceClient.GetTableClient(tableName);

            }
            catch (Exception ex)
            {

                serviceClient.CreateTableIfNotExists(tableName);
                return serviceClient.GetTableClient(tableName);
            }

        }

        public bool InsertEndPointData(IEndPointData endpointData)
        {
            TableClient tc = GetTableClient();
            var res = tc.UpsertEntityAsync(new Model.EndPointDataEntity
            {
                Organization = endpointData.Organization,
                AppName = endpointData.AppName,
                EndPoint = endpointData.EndPoint,
                RequestBody = endpointData.RequestBody,
                RequestType = endpointData.RequestType,
                PartitionKey = endpointData.Organization + endpointData.AppName,
                RowKey = endpointData.EndPoint,
                ResponseBody = endpointData.ResponseBody,
                RequestHeaders = endpointData.RequestHeaders
            }, TableUpdateMode.Replace).Result;

            

            return true;
        }

        public bool UpdateEndPointData(IEndPointData endpointData)
        {
            return InsertEndPointData(endpointData);

        }

        public IEndPointData GetEndPointData(string org, string app, string endpPoint)
        {
            string partitionKey = EndPointDataEntity.GetPartionKey(org, app);
            string rowKey = EndPointDataEntity.GetRowKey(endpPoint);
            TableClient tc = GetTableClient();
            return tc.GetEntity<EndPointDataEntity>(partitionKey, rowKey).Value;

        }

        /// <summary>
        /// Gets the value by Partionkey
        /// </summary>
        /// <param name="org"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        public List<IEndPointData> GetEndPoints(string org, string app)
        {

            return GetEndPointsByQuery("PartionKey", EndPointDataEntity.GetPartionKey(org, app));
        }



        public bool DeleteEndPointData(string org, string app, string endpPoint)
        {
            TableClient tc = GetTableClient();
            string partionKey = EndPointDataEntity.GetPartionKey(org, app);
            string rowKey = EndPointDataEntity.GetRowKey(endpPoint);
            tc.DeleteEntity(partionKey, rowKey);
            return true;

        }

        public bool DeleteEndPointData(string endPointId)
        {
            throw new NotImplementedException();
        }

        public List<IEndPointData> GetEndPoints(string org)
        {
            return GetEndPointsByQuery("Organization", org);
        }


        protected List<IEndPointData> GetEndPointsByQuery(string filterColumnName, string filterValue)
        {
            TableClient tc = GetTableClient();

            string query = TableClient.CreateQueryFilter($"{filterColumnName} eq {filterValue}");
            //Azure.Pageable<EndPointDataEntity> queryResultsFilter = tc.Query<EndPointDataEntity>(x => x.PartitionKey == "");
            Azure.Pageable<EndPointDataEntity> queryResultsFilter = tc.Query<EndPointDataEntity>(filter: query);


            List<IEndPointData> edList = new List<IEndPointData>();
            foreach (EndPointDataEntity ed in queryResultsFilter)
            {
                edList.Add(ed);
            }

            return edList;

        }
    }
}
