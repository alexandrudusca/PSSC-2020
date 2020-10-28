using System;
using System.Linq;
using static Question.Domain.AskQuestionWorkflow.AskQuestionResult;

namespace Question.Domain.AskQuestionWorkflow
{
    public class UpdateVotes
    {
        public QuestionAdded Update(QuestionAdded result, VoteEnum newVote) //update votes counter on every new vote added to the list
        {
            var totalvotes = result.TotalVotes.ToList(); //get current list of votes
            totalvotes.Add(newVote); //add current vote to the list
   
            return new QuestionAdded(result.Id, result.Title, result.Text, result.Tags, totalvotes.Sum(nrOfVotes => Convert.ToInt32(nrOfVotes)), totalvotes); //update question data
        }
    }
}