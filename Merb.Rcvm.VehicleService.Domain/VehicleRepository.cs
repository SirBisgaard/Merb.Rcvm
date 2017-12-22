using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Merb.Rcvm.VehicleService.Domain
{
    public class VehicleRepository
    {
        private readonly IMongoClient _client;

        public VehicleRepository()
        {
            _client = new MongoClient("mongodb://localhost:27017");
        }

        private IMongoCollection<Vehicle> GetCollection()
        {
            var db = _client.GetDatabase("recycling-center");
            var collection = db.GetCollection<Vehicle>("vehicles");
            return collection;
        }

        public async Task<IEnumerable<Vehicle>> GetAllRecyclingCenters()
        {
            var collection = GetCollection();
            return await collection.Find(Builders<Vehicle>.Filter.Empty).ToListAsync();
        }

        public async Task<Vehicle> GetRecyclingCenterAsync(string id)
        {
            var collection = GetCollection();
            var filter = Builders<Vehicle>.Filter.Eq(rc => rc.Id, id);
            return await collection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task CreateRecyclingCenter(Vehicle recyclingCenter)
        {
            var collection = GetCollection();
            recyclingCenter.Id = $"{Guid.NewGuid()}";
            await collection.InsertOneAsync(recyclingCenter);
        }

        public async Task UpdateRecyclingCenter(Vehicle recyclingCenter)
        {
            var collection = GetCollection();
            var filter = Builders<Vehicle>.Filter.Eq(rc => rc.Id, recyclingCenter.Id);
            await collection.FindOneAndReplaceAsync(filter, recyclingCenter);
        }

        public async Task DeleteRecyclingCenter(string id)
        {
            var collection = GetCollection();
            var filter = Builders<Vehicle>.Filter.Eq(rc => rc.Id, id);
            await collection.FindOneAndDeleteAsync(filter);
        }
    }
}
