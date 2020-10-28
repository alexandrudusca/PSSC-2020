using System;
using System.Collections.Generic;
using CSharp.Choices;
using Question.Domain.AskQuestionWorkflow;

namespace Question.Domain.AskQuestionWorkflow
{
    //sum type
    [AsChoice]
    public static partial class AskQuestionResult
    {
        public interface IAskQuestionResult 
        {

        }

        public class QuestionAdded : IAskQuestionResult
        {
            public Guid Id { get; private set; }
            public string Title { get; private set; }
            public string Text { get; private set; }
            public List<string> Tags { get; private set; }
            public int VoteCount { get; private set; }
            public IReadOnlyCollection<VoteEnum> TotalVotes { get; private set; }

            public QuestionAdded(Guid id, string title, string text, List<string> tags, int votecount, IReadOnlyCollection<VoteEnum> totalvotes)
            {
                Id = id;
                Title = title;
                Text= text;
                Tags = tags;
                VoteCount = votecount;
                TotalVotes = totalvotes;
            } 
        }

        public class QuestionNotAdded : IAskQuestionResult
        {
            public string ErrorMessage { get; set; }

            public QuestionNotAdded(string errorMessage)
            {
                ErrorMessage = errorMessage;
            }
        }

        public class QuestionValidationFailed : IAskQuestionResult
        {
            public List<string> ValidationErrros { get; set; }

            public QuestionValidationFailed(List<string> errors)
            {
                ValidationErrros = errors;
            }
        }
    }
}