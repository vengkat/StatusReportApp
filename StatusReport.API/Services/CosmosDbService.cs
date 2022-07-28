using Microsoft.Azure.Cosmos;
using StatusReport.API.Models;
using User = StatusReport.API.Models.User;

namespace StatusReport.API.Services
{
    public class CosmosDbService<T> : ICosmosDbService<T>
    {

        #region PublicandPrivateDeclaration
        private Container _container;
        #endregion

        #region Constructor
        public CosmosDbService(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }
        #endregion

        #region AddAsync
        /// <summary>
        /// AddAsync
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task AddAsync(T item)
        {
            dynamic obj = item;
            var id = obj.id;
            await _container.CreateItemAsync(item, new PartitionKey(id));
        }
        #endregion

        #region DeleteAsync
        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<T>(id, new PartitionKey(id));
        }
        #endregion

        #region GetAsync
        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(string id)
        {
            try
            {
                //dynamic obj = (T)Activator.CreateInstance(typeof(T));
                var response = await _container.ReadItemAsync<T>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException Ex) //For handling item not found and other exceptions
            {
                throw;
            }
        }
        #endregion

        #region GetMultipleAsync
        /// <summary>
        /// GetMultipleAsync
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<User>(new QueryDefinition(queryString));

            var results = new List<User>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return (IEnumerable<T>)results;
        }
        #endregion

        #region UpdateAsync
        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task UpdateAsync(string id, T item)
        {
            await _container.UpsertItemAsync(item, new PartitionKey(id));
        }
        #endregion
    }
}
