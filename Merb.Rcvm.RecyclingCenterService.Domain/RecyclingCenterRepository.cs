﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Merb.Rcvm.RecyclingCenterService.Domain
{
    public class RecyclingCenterRepository
    {
        private readonly IMongoClient _mongoClient;
        private readonly RecyclingCenterQueueClient _queueClient;

        public RecyclingCenterRepository()
        {
            _mongoClient = new MongoClient("mongodb://localhost:27017");
            _queueClient = new RecyclingCenterQueueClient();
        }

        private IMongoCollection<RecyclingCenter> GetCollection()
        {
            var db = _mongoClient.GetDatabase("recycling-center");
            var collection = db.GetCollection<RecyclingCenter>("centers");
            return collection;
        }

        public async Task<IEnumerable<RecyclingCenter>> GetAllRecyclingCenters()
        {
            var collection = GetCollection();
            var filter = Builders<RecyclingCenter>.Filter.Eq(rc => rc.IsDeleted, false);
            return await collection.Find(filter).ToListAsync();
        }

        public async Task<RecyclingCenter> GetRecyclingCenter(string id)
        {
            var collection = GetCollection();
            var filter = Builders<RecyclingCenter>.Filter.Eq(rc => rc.Id, id) & Builders<RecyclingCenter>.Filter.Eq(rc => rc.IsDeleted, false);
            return await collection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task CreateRecyclingCenter(RecyclingCenter recyclingCenter)
        {
            var collection = GetCollection();
            recyclingCenter.Id = $"{Guid.NewGuid()}";
            await collection.InsertOneAsync(recyclingCenter);
        }

        public async Task UpdateRecyclingCenter(RecyclingCenter recyclingCenter)
        {
            var collection = GetCollection();
            var filter = Builders<RecyclingCenter>.Filter.Eq(rc => rc.Id, recyclingCenter.Id);
            await collection.FindOneAndReplaceAsync(filter, recyclingCenter);
        }

        public async Task DeleteRecyclingCenter(string id)
        {
            var center = await GetRecyclingCenter(id);
            center.IsDeleted = true;
            await UpdateRecyclingCenter(center);

            // Publish that the recycling center is deleted.
            _queueClient.RecyclingCenterDeleted(center);
        }
    }
}
