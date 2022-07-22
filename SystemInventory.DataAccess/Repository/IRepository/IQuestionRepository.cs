using SystemInventory.Models;

namespace SystemInventory.DataAccess.Repository.IRepository
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Question FindAndUpdate(Question data);
    }
}
