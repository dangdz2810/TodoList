using Authentication.Entity;

namespace Authentication.Repository
{
    public interface ITodoItemRepository
    {
        Task<ToDoItem> GetByIdAsync(int id);
        Task<List<ToDoItem>> GetAllAsync();
        Task<ToDoItem?> GetByTodoItemname(string name);
        Task<ToDoItem> CreateAsync(ToDoItem item);
        Task<ToDoItem> UpdateAsync(int id, ToDoItem item);
        Task DeleteAsync(int id);
        Task<List<ToDoItem>> GetAllByUserIdAsync(int userId);
    }
}
