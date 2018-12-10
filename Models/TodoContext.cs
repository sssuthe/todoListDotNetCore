using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TodoApi.Models
{
    //This will expose our MongoDB collections, specifically the todo item collection in this case
    public class TodoContext : ITodoContext
    {
        private readonly IMongoDatabase _db;

        public TodoContext(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<TodoItem> TodoItems => _db.GetCollection<TodoItem>("todos");
    }

    public interface ITodoContext
    {
        IMongoCollection<TodoItem> TodoItems { get; }
    }
}