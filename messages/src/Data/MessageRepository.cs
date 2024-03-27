using MongoDB.Bson;
using MongoDB.Driver;

namespace FP.ContainerTraining.Messages.Data;

public class MessageRepository : IMessageRepository
{
    private string connectionstring;

    public MessageRepository(IConfiguration configuration)
    {
        connectionstring = configuration.GetConnectionString("MessageDB")!;
    }

    public async Task<string> SaveMessage(string user, string content)
    {
        var db = GetMongoDatabase();
        var collection = db.GetCollection<DtoMessage>("Messages");
        var dto = new DtoMessage
        {
            Content = content,
            Timestamp = DateTime.Now,
            User = user
        };
        await collection.InsertOneAsync(dto);
        return dto.Id.ToString();
    }

    public async Task<List<DtoMessage>> GetMessages()
    {
        var db = GetMongoDatabase();
        var collection = db.GetCollection<DtoMessage>("Messages");
        var filter = new BsonDocument();
        var sort = Builders<DtoMessage>.Sort.Descending("timestamp");
        return await collection.Find(filter).Sort(sort).ToListAsync();
    }

    private IMongoDatabase GetMongoDatabase()
    {
        var client = new MongoClient(connectionstring);
        return client.GetDatabase("MessageData");
    }
}