using Authentication.Entity;

namespace Authentication.Dao.Specifications.TodoItemSpec
{
    public class GetTodoItemNameSpec : BaseSpecification<ToDoItem>

    {
        public GetTodoItemNameSpec(string name)
        : base(item => item.Name.ToLower() == name.ToLower())
        {
        }
    }
}
