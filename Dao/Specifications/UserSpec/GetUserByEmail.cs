using Authentication.Entity;

namespace Authentication.Dao.Specifications.UserSpec
{
    public class GetUserByEmail : BaseSpecification<User>

    {
        public GetUserByEmail(string email)
        : base(item => item.Email.ToLower() == email.ToLower())
        {
        }
    }
}
