using Authentication.Entity;

namespace Authentication.Dao.Specifications.UserSpec
{
    public class GetUserByUserName : BaseSpecification<User>

    {
        public GetUserByUserName(string UserName)
        : base(user => user.UserName.ToLower() == UserName.ToLower())
        {
        }
    }
}
