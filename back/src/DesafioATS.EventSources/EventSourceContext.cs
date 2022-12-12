using MongoDB.Driver;

namespace DesafioATS.EventSources
{
    public class EventSourceContext 
    {
        private IMongoDatabase _database { get; }

        public EventSourceContext(string ConnectionString, string DatabaseName)
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
            var mongoClient = new MongoClient(settings);
            _database = mongoClient.GetDatabase(DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>(typeof(T).Name);
        }
    }
}
