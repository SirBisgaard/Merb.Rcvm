using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Merb.Rcvm.RecyclingCenterService.Domain
{
    public class RecyclingCenterRepository
    {
        private readonly IMongoClient _client;

        public RecyclingCenterRepository()
        {
            _client = new MongoClient("mongodb://localhost:27017");
        }

        public async Task<IEnumerable<RecyclingCenter>> GetAllRecyclingCenters()
        {
            var db = _client.GetDatabase("recycling-center");
            var collection = db.GetCollection<RecyclingCenter>("centers");

            return await collection.Find(Builders<RecyclingCenter>.Filter.Empty).ToListAsync();
        }

        public async Task<RecyclingCenter> GetRecyclingCenterAsync(string id)
        {
            var db = _client.GetDatabase("recycling-center");
            var collection = db.GetCollection<RecyclingCenter>("centers");
            var filter = Builders<RecyclingCenter>.Filter.Eq(rc => rc.Id, id);

            return await collection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task CreateRecyclingCenter(RecyclingCenter recyclingCenter)
        {
            var db = _client.GetDatabase("recycling-center");
            var collection = db.GetCollection<RecyclingCenter>("centers");

            recyclingCenter.Id = $"{Guid.NewGuid()}";

            await collection.InsertOneAsync(recyclingCenter);
        }

        public async Task UpdateRecyclingCenter(RecyclingCenter recyclingCenter)
        {
            var db = _client.GetDatabase("recycling-center");
            var collection = db.GetCollection<RecyclingCenter>("centers");
            var filter = Builders<RecyclingCenter>.Filter.Eq(rc => rc.Id, recyclingCenter.Id);

            await collection.FindOneAndReplaceAsync(filter, recyclingCenter);
        }

        public async Task DeleteRecyclingCenter(string id)
        {
            var db = _client.GetDatabase("recycling-center");
            var collection = db.GetCollection<RecyclingCenter>("centers");
            var filter = Builders<RecyclingCenter>.Filter.Eq(rc => rc.Id, id);

            await collection.FindOneAndDeleteAsync(filter);
        }
    }
}
