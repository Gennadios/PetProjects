using System.Linq;
using System.Collections.Generic;
using WhoWantsToBeAMillionaire.Models.Repositories;
using System;

namespace WhoWantsToBeAMillionaire.Models.Services
{
    public class GameService : IGameService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;

        public GameService(IQuestionRepository questionRepository, IAnswerRepository answerRepository)
        {
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
        }

        public static int QuestionNumber { get; set; } = 1;

        public int NumberOfQuestions { get; set; }

        public Dictionary<Question, Answer[]> GetGameQuestions()
        {
            var gameQuestions = new Dictionary<Question, Answer[]>();
            var questions = GetQuestions(NumberOfQuestions);

            for (int i = 0; i < questions.Length; i++)
            {
                var answers = _answerRepository.Answers.Where(a => a.QuestionId == questions[i].Id).ToArray();
                gameQuestions.Add(questions[i], answers);
            }

            return gameQuestions;
        }

        private Question[] GetQuestions(int numberOfQuestions)
        {
            Question[] questions = new Question[numberOfQuestions];
            Random rnd = new Random();

            for (int i = 0; i < numberOfQuestions; i++)
            {
                int randomId = rnd.Next(1, _questionRepository.Questions.Count() + 1);

                if (!questions.Any(q => q?.Id == randomId) || questions.Length == 0)
                    questions[i] = _questionRepository.Questions.ToArray()[randomId - 1];
                else
                    i--;
            }

            return questions;
        }
    }
}
