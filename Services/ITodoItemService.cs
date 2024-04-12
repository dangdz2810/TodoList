using Authentication.Entity;

namespace Authentication.Services
{
    public interface ITodoItemService
    {
        Task<ToDoItem> GetByIdAsync(int id);
        Task<List<ToDoItem>> GetAllAsync();
        Task<ToDoItem> CreateAsync(ToDoItem item);
        Task<ToDoItem> UpdateAsync(int id, ToDoItem item);
        Task<List<ToDoItem>> GetAllByUserIdAsync(int userId);
        Task DeleteAsync(int id);
    }
}
