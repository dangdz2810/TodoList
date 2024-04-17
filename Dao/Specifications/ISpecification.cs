using System.Linq.Expressions;

namespace Authentication.Dao.Specifications
{
    public interface ISpecification<T> where T : class 
    {
        // điều kiện where
        Expression<Func<T, bool>> Criteria { get; set; }
        // điều kiện join
        List<Expression<Func<T, object>>> Includes { get; set; }
        // sắp xếp
        Expression<Func<T, object>> OrderBy { get; set; }
        Expression<Func<T, object>> OrderByDescending { get; set; }
        // thuộc tính phân trang
        int Take { get; set; }
        int Skip { get; set; }
        bool IsPaginationEnabled { get; set; }
    }
}
