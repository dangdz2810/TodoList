using Authentication.Entity;

namespace Authentication.Dao.Specifications.TodoItemSpec
{
    public class GetTodoItemByUserIdSpec : BaseSpecification<ToDoItem>

    {
        public GetTodoItemByUserIdSpec(int userId)
        : base(item => item.UserId == userId)
        {
        }
    }
}
