using Mocku.API.DAL;
using Mocku.API.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mocku.API.BAL
{
    public class EndpointBL
    {

        private IStorageProvider StorageProvider { get; set; }


        /// <summary>
        /// Creates an Instance of EndPointBL with Storage provider sent as a parameter and connection string is fetched from environment variable "EndPointStorage"
        /// </summary>
        /// <param name="storageProviders">Storage provider option, this value is defaulted to Azure Storage if none is selected</param>
        public EndpointBL(StorageProviders storageProviders = StorageProviders.AzureStorage)
        {
            this.StorageProvider = GetStorageProvider(storageProviders);
        }

        /// <summary>
        /// Creates an instance of EndPoint BL with connection string and storage provider sent as parameter.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="storageProviders"></param>
        public EndpointBL(string connectionString, StorageProviders storageProviders)
        {

            this.StorageProvider = GetStorageProvider(storageProviders, connectionString);

        }

        protected IStorageProvider GetStorageProvider(StorageProviders provider, string connectionString = null)
        {
            IStorageProvider storageProvider = null;
            switch (provider)
            {
                case StorageProviders.AzureStorage:
                    storageProvider = string.IsNullOrEmpty(connectionString) ? new AzureStorageRepository() : new AzureStorageRepository(connectionString);
                    break;
            }

            return storageProvider;
        }



        public bool InsertEndPointData(IEndPointData endPointData)
        {

            return this.StorageProvider.InsertEndPointData(endPointData);

        }


        public bool UpdateEndPoint(IEndPointData endPointData)
        {

            return this.StorageProvider.UpdateEndPointData(endPointData);

        }

        public bool DeleteEndPoint(IEndPointData endPointData)
        {
            return this.StorageProvider.DeleteEndPointData(endPointData.Organization, endPointData.AppName, endPointData.EndPoint);

        }

        public List<IEndPointData> GetEndPoints(string org)
        {
            return this.StorageProvider.GetEndPoints(org);
        }


        public IEndPointData GetEndPoint(string org, string app, string endPoint)
        {
            return this.StorageProvider.GetEndPointData(org, app, endPoint);
        }





        public enum StorageProviders
        {
            AzureStorage,
            MongoDb,
            AzureSQL

        }

    }
}
