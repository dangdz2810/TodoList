using Authentication.Data;
using Authentication.Entity;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Repository
{
    public class TodoItemRepository : ITodoItemRepository

    {
        private readonly DataContext _context;

        public TodoItemRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ToDoItem> CreateAsync(ToDoItem item)
        {
            _context.ToDoItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task DeleteAsync(int id)
        {
            var item = _context.ToDoItems.Where(i => i.Id == id).FirstOrDefault();
            _context.ToDoItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ToDoItem>> GetAllAsync()
        {
            return await _context.ToDoItems.ToListAsync();
        }

        public async Task<List<ToDoItem>> GetAllByUserIdAsync(int userId)
        {
            var TodoItemByUser = await _context.ToDoItems.Where(t => t.UserId == userId).ToListAsync();
            return TodoItemByUser;
        }

        public async Task<ToDoItem> GetByIdAsync(int id)
        {
            return await _context.ToDoItems.FindAsync(id);
        }

        public async Task<ToDoItem?> GetByTodoItemname(string name)
        {
            return await _context.ToDoItems.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<ToDoItem> UpdateAsync(int id, ToDoItem item)
        {
            ToDoItem existingItem = await _context.ToDoItems.FindAsync(id);
            if (item == null)
            {
                throw new ArgumentException("not found item with id : " + id);
            }
            ToDoItem newToDoItem = new ToDoItem
            {
                Name = item.Name,
                IsComplete = item.IsComplete,
            };
            await _context.SaveChangesAsync();
            return newToDoItem;
        }
    }
}
