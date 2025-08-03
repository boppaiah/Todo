using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TodoAPI.DTOs;
using TodoAPI.Models;
using TodoAPI.Todos.CreateTodo;
using TodoAPI.Todos.DeleteTodo;
using TodoAPI.Todos.GetTodos;
using TodoAPI.Todos.UpdateTodo;

namespace TodoAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TodoController : Controller
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public TodoController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        //api/v1/todo
        [HttpGet]
        [Route("items")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var query = new GetTodosQuery(pageNumber, pageSize);
            var todoItems = await _sender.Send(query);
            if (todoItems?.TodoItems == null || todoItems?.TodoItems.Any() == false)
            {
                return BadRequest("No items Found");
            }
            return Ok(todoItems);

        }

        //api/v1/todo
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<bool>> CreateTodoItem([FromBody] CreateTodoRequest request)
        {
            var command = _mapper.Map<CreateTodoCommand>(request);
            var response = await _sender.Send(command);

            if (!response.IsSuccess)
            {
                return BadRequest("No todo Item found");
            }
            return Ok(true);
        }

        //api/v1/todo?id
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<bool>> DeleteTodoItem([FromQuery] Guid id)
        {
            var command = _mapper.Map<DeleteTodoCommand>(id);
            var response = await _sender.Send(command);

            if (!response.IsSuccess)
            {
                return BadRequest("No todo Item found");
            }
            return Ok(true);

        }


        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<bool>> UpdateTodoItem([FromBody] UpdateTodoRequest request)
        {

            var command = _mapper.Map<UpdateTodoCommand>(request);
            var response = await _sender.Send(command);

            if (!response.IsSuccess)
            {
                return BadRequest("No todo Item found");
            }
            return Ok(true);
        }
    }
}
