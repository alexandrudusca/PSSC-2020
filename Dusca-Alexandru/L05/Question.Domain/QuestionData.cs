using System.Collections.Generic;
using CSharp.Choices;
using LanguageExt.Common;


namespace Question.Domain.AskQuestionWorkflow
{
    [AsChoice]
    public partial class QuestionData 
    {
        public interface IQuestionData 
        {

        }   
        public class UnverifiedQuestion : IQuestionData //case question is unverified
        {
            public string Title { get; private set; }
            public string Text { get;  private set; }
            public List<string> Taglist { get; private set; }

            private UnverifiedQuestion(string title, string text, List<string> taglist)
            {
                Title = title;
                Text = text;
                Taglist = taglist;
            }

            public static Result<UnverifiedQuestion> Create(string title, string text, List<string> taglist)
            {
                if (IsQuestionDataValid(title, text, taglist)) //validate a question data
                {
                    return new UnverifiedQuestion(title, text, taglist); //data is valid
                }
                else
                {
                    return new Result<UnverifiedQuestion>(new InvalidQuestionDataException(title, text, taglist)); //data is invalid
                }
            }

            private static bool IsQuestionDataValid(string title, string text, List<string> taglist)
            {
                if(!string.IsNullOrWhiteSpace(title) &&
                    !string.IsNullOrWhiteSpace(text) && text.Length < 1000 &&
                    taglist.Count >= 1 && taglist.Count <= 3)
                    {
                        return true;
                    }
                return false;
            }
        } 
        public class VerifiedQuestion : IQuestionData //case question is verified
        {
            public string Title { get; private set; }
            public string Text { get;  private set; }
            public List<string> Taglist { get; private set; }

            internal VerifiedQuestion(string title, string text, List<string> taglist)
            {
                Title = title;
                Text = text;
                Taglist = taglist;
            }
        }    
    }
}