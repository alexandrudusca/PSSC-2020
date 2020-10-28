using System;
using System.Collections.Generic;

namespace Question.Domain.AskQuestionWorkflow
{
    [Serializable]
    public class InvalidQuestionDataException : Exception
    {
        public InvalidQuestionDataException(string title, string text, List<string> taglist) : base("Title length: "  + title.Length + "\n" + "Body text length: " + text.Length + "\n" + "Number of tags: " + taglist.Count)
        {
            
        }
        

    }
}