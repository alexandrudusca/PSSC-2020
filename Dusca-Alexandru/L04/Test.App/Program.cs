using System;
using System.Collections.Generic;
using L04;

namespace Test.App
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> _taglist = new List<string>{ "DDD", "Beginner" };
            var cmd = new AskQuestionCmd("What is DDD?", "Hello, I would like to know more details about Domain Driver Design. Thanks!", _taglist);
            var result = AskQuestion(cmd);
            
            result.Match(
                ProcessQuestionAdded,
                ProcessQuestionNotAdded,
                ProcessQuestionValidationFailed
            );
        }

        private static string ProcessQuestionNotAdded(AskQuestionResult.QuestionNotAdded arg)
        {
            Console.WriteLine("Question not added. Reason: " + arg.ErrorMessage.ToString());
            
            return arg.ErrorMessage.ToString();
        }

        private static string ProcessQuestionValidationFailed(AskQuestionResult.QuestionValidationFailed arg)
        {
            Console.WriteLine("Question failed validation: ");
            foreach(var error in arg.ValidationErrros)
            {
                Console.WriteLine(error);
            }

            Console.WriteLine("Feel free to contact administration for a manual question review!");
            return arg.ToString();
        }

        private static string ProcessQuestionAdded(AskQuestionResult.QuestionAdded arg)
        {
            Console.WriteLine("Question added with id:" + arg.Id);
            Console.WriteLine("Title: " + arg.Title);
            Console.WriteLine("Body text: " + arg.Text);
            Console.WriteLine("Tags:");
            foreach(var item in arg.Tags)
            {
                Console.WriteLine(item);
            }

            return arg.ToString();
        }

        static AskQuestionResult.IAskQuestionResult AskQuestion(AskQuestionCmd cmd)
        {
            if(string.IsNullOrWhiteSpace(cmd.Title))
            {
                string _error = new string ("Invalid title!");
                return new AskQuestionResult.QuestionNotAdded(_error);
            }

            if(string.IsNullOrWhiteSpace(cmd.Text))
            {
                string _error = new string ("Invalid text!");
                return new AskQuestionResult.QuestionNotAdded(_error);
            }

            if(new Random().Next(10) == 1)
            {
                var _error = new List<string> {"ML validation failed!"};
                return new AskQuestionResult.QuestionValidationFailed(_error);
            }

            var Id = Guid.NewGuid();
            var result = new AskQuestionResult.QuestionAdded(Id, cmd.Title, cmd.Text, cmd.Tags);
            return result;
        }
    }
}
