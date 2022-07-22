using System.Linq;
using SystemInventory.DataAccess.Repository.IRepository;
using SystemInventory.Models;
using SystemInventoryWebNet5.DataAccess.Data;

namespace SystemInventory.DataAccess.Repository
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        private readonly ApplicationDbContext _context;
        public QuestionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Question FindAndUpdate(Question data)
        {
            Question entity = _context.Question.FirstOrDefault(w => w.Id == data.Id);
            if (entity is not null)
            {
                entity.Title = data.Title;
                entity.Response = data.Response;
                entity.Category = data.Category;
                entity.Image = data.Image;
                entity.Status = data.Status;
                return entity;
            }

            return null;
        }
    }
}
