using System.Linq;

namespace WhoWantsToBeAMillionaire.Models.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private MillionaireDbContext _context;

        public AnswerRepository(MillionaireDbContext context) => _context = context;

        public IQueryable<Answer> Answers => _context.Answers;
    }
}
