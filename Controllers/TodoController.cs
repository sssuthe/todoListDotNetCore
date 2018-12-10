using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Todo")]
    public class ToDoController : Controller
    {
        private readonly ITodoRepository _todoRepository;

        public ToDoController(ITodoRepository  todoRepository)
        {
            _todoRepository = todoRepository;
        }

        // GET: api/Todo
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new ObjectResult(await _todoRepository.GetAllTodos());
        }

        // GET: api/Todo/TodoName
        [HttpGet("{todo}", Name = "Get")]
        public async Task<IActionResult> Get(string todo)
        {
            var todoItem = await _todoRepository.GetTodo(todo);

            if (todoItem == null)
            {
                return new NotFoundResult();
            }

            return new ObjectResult(todoItem);
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TodoItem todo)
        {
            await _todoRepository.Create(todo);
            return new OkObjectResult(todo);
        }

        // PUT: api/Todo/5
        [HttpPut("{todo}")]
        public async Task<IActionResult> Put(string todo, [FromBody]TodoItem todoItem)
        {
            var todoFromDb = await _todoRepository.GetTodo(todo);

            if (todoFromDb == null)
            {
                return new NotFoundResult();
            }

            todoItem.Id = todoFromDb.Id;
            await _todoRepository.Update(todoItem);
            return new OkObjectResult(todoItem);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{todo}")]
        public async Task<IActionResult> Delete(string todo)
        {
            var todoFromDb = await _todoRepository.GetTodo(todo);

            if (todoFromDb == null)
            {
                return new NotFoundResult();
            }

            await _todoRepository.Delete(todo);

            return new OkResult();
        }
    }
}