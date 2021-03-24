using learndotnet.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learndotnet.Services
{
    public class ShipwreckService 
    {
        private IMongoCollection<Shipwreck> _shipWreckColection;
        private IMongoCollection<SubmissionModel> _submissions;
        public ShipwreckService (IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _shipWreckColection = database.GetCollection<Shipwreck>("shipwrecks");
            _submissions = database.GetCollection<SubmissionModel>("Submissions");
        }

        public Task<Shipwreck> GetData()
        {
            var result = _shipWreckColection.Find(s => s.Id == new ObjectId("578f6fa2df35c7fbdbaed8c4")).FirstOrDefaultAsync();
            Console.WriteLine(result.Result.Id);
            return result;
        }
        public SubmissionModel Create(SubmissionModel submission)
        {
            _submissions.InsertOne(submission);
            return submission;
        }
    }
}
