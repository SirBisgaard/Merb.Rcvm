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

        public async Task<IEnumerable<Vehicle>> GetAllVehicles(string recyclingCenterId)
        {
            var collection = GetCollection();
            var filter = Builders<Vehicle>.Filter.Eq(v => v.RecyclingCenterId, recyclingCenterId) & Builders<Vehicle>.Filter.Eq(v => v.IsDeleted, false);
            return await collection.Find(filter).ToListAsync();
        }

        public async Task<Vehicle> GetVehicle(string id)
        {
            var collection = GetCollection();
            var filter = Builders<Vehicle>.Filter.Eq(v => v.Id, id) & Builders<Vehicle>.Filter.Eq(v => v.IsDeleted, false);
            return await collection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task CreateVehicle(Vehicle vehicle)
        {
            var collection = GetCollection();
            vehicle.Id = $"{Guid.NewGuid()}";
            await collection.InsertOneAsync(vehicle);
        }

        public async Task UpdateVehicle(Vehicle vehicle)
        {
            var collection = GetCollection();
            var filter = Builders<Vehicle>.Filter.Eq(rc => rc.Id, vehicle.Id);
            await collection.FindOneAndReplaceAsync(filter, vehicle);
        }

        public async Task DeleteVehicle(string id)
        {
            var vehicle = await GetVehicle(id);
            vehicle.IsDeleted = true;
            await UpdateVehicle(vehicle);
        }
    }
}
