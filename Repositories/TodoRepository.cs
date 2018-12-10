using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    //This will handle all of the CRUD operations for Todos
    public class TodoRepository : ITodoRepository
    {
        private readonly ITodoContext _context;

        public TodoRepository(ITodoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetAllTodos()
        {
            return await _context
                            .TodoItems
                            .Find(_ => true)
                            .ToListAsync();
        }

        public Task<TodoItem> GetTodo(string name)
        {
            FilterDefinition<TodoItem> filter = Builders<TodoItem>.Filter.Eq(m => m.todo, name);
            return _context
                    .TodoItems
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }

        public async Task Create(TodoItem todoItem)
        {
            try
            {
                await _context.TodoItems.InsertOneAsync(todoItem);
            }
            catch (Exception ex)
            {
                   
            }
        }

        public async Task<bool> Update(TodoItem todoItem)
        {
            ReplaceOneResult updateResult =
                await _context
                        .TodoItems
                        .ReplaceOneAsync(
                            filter: g => g.Id == todoItem.Id,
                            replacement: todoItem);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string name)
        {
            FilterDefinition<TodoItem> filter = Builders<TodoItem>.Filter.Eq(m => m.todo, name);
            DeleteResult deleteResult = await _context
                                                .TodoItems
                                                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }

    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetAllTodos();
        Task<TodoItem> GetTodo(string name);
        Task Create(TodoItem todoItem);
        Task<bool> Update(TodoItem todoItem);
        Task<bool> Delete(string name);
    }
}
