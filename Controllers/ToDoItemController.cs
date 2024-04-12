﻿using Authentication.Attributes;
using Authentication.Entity;
using Authentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;

        public ToDoItemController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }
        [HttpGet]
        [JwtAuthorize("user","admin")]
        public async Task<IActionResult> GetAllAsync()
        {
            List<ToDoItem> toDoItems = await _todoItemService.GetAllAsync();
            if (toDoItems.Count() == 0)
            {
                return NotFound();
            }
            return Ok(toDoItems);
        }

        [HttpGet("todoItemByUser/{userId}")]
        public async Task<IActionResult> GetAllByUserId(int userId)
        {
            List<ToDoItem> listTodoItem = await _todoItemService.GetAllByUserIdAsync(userId);
            if (listTodoItem.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listTodoItem);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var item = await _todoItemService.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ToDoItem item)
        {
            var newItem = await _todoItemService.CreateAsync(item);
            return Ok(newItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, ToDoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            try
            {
                await _todoItemService.UpdateAsync(id, item);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _todoItemService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
    }
}