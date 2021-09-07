using System.Collections.Generic;

namespace WhoWantsToBeAMillionaire.Models.Services
{
    public interface IGameService
    {
        Dictionary<Question, Answer[]> GetGameQuestions();
    }
}
