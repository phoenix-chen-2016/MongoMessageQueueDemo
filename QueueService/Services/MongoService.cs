using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Routing;
using MongoDB.Bson;
using MongoDB.Driver;

namespace QueueService.Services
{
	public class MongoService : Mongo.MongoBase
	{
		private IMongoDatabase m_MongoDatabase;

		public MongoService()
		{
			var client = new MongoClient("mongodb://mongo_mongo_1");
			m_MongoDatabase = client.GetDatabase("Store");
		}
		public override async Task<EmptyReply> AddData(MongoAddRequest request, ServerCallContext context)
		{
			var collection = m_MongoDatabase.GetCollection<BsonDocument>(request.CollectionName);

			await collection.InsertOneAsync(BsonDocument.Parse(request.Data)).ConfigureAwait(false);
			return new EmptyReply();
		}
	}
}
