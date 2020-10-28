using System;
using System.Collections.Generic;
using LanguageExt;
using Question.Domain.AskQuestionWorkflow;
using static Question.Domain.AskQuestionWorkflow.QuestionData;

namespace Test.App
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> _taglist = new List<string>{ "DDD", "Beginner" }; //2 valid tags
            var questionResult = UnverifiedQuestion.Create("What is DDD?", "Hello, I would like to know more details about Domain Driver Design. Thanks!", _taglist); //valid question data example
           
            questionResult.Match(
                Succ: result => //if question data is valid
                {
                    UpdateVotesService(result); //update votes service to maintain a valid number of votes per question
                    Console.WriteLine("Question successfully was added to the list!"); //return an acknowledgment
                    return Unit.Default; 
 
                },
                Fail: ex => //if question data is not valid
                {
                    Console.WriteLine("Invalid question data. The inputs are: \n" + ex.Message);
                    return Unit.Default;
                }
            ); 
        }

        private static void UpdateVotesService(UnverifiedQuestion unverifiedQuestion)
        {
            IReadOnlyCollection<VoteEnum> emptyList = Array.Empty<VoteEnum>();
            var verifiedQuestionResult = new VerifyQuestionData().VerifyQuestion(unverifiedQuestion);
                    verifiedQuestionResult.Match(
                        verifiedQuestion => //if question is verified you can vote
                        {
                            var questionId = Guid.NewGuid(); //simulate an added question
                            var questionAdded = new AskQuestionResult.QuestionAdded(questionId, verifiedQuestion.Title, verifiedQuestion.Text, verifiedQuestion.Taglist, 0, emptyList); 

                            var updatedQuestion = new UpdateVotes().Update(questionAdded, VoteEnum.Up); //simulate a vote up system by giving a vote
                            Console.WriteLine("Current question: " + updatedQuestion.Title + "\n" +
                                              "Current vote status: " + updatedQuestion.TotalVotes.Count); //check result (number of votes)
                            return Unit.Default;
                        },
                        ex => //if question is not verified you cannot vote
                        {
                            Console.WriteLine("Error to vote!");
                            return Unit.Default;
                        }
                    );
        }
    }
}
