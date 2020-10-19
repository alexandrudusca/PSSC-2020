using System;
using System.Collections.Generic;
using CSharp.Choices;

namespace L04
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

            public QuestionAdded(Guid _id, string _title, string _text, List<string> _tags)
            {
                Id = _id;
                Title = _title;
                Text= _text;
                Tags = _tags;
            }    
        }

        public class QuestionNotAdded : IAskQuestionResult
        {
            public string ErrorMessage { get; set; }

            public QuestionNotAdded(string _errorMessage)
            {
                ErrorMessage = _errorMessage;
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