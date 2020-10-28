using LanguageExt.Common;
using static Question.Domain.AskQuestionWorkflow.QuestionData;

namespace Question.Domain.AskQuestionWorkflow
{
    public class VerifyQuestionData
    {
        public Result<VerifiedQuestion> VerifyQuestion(UnverifiedQuestion _question)
        {
            //test question with ML or manual review
            
            return new VerifiedQuestion(_question.Title, _question.Text, _question.Taglist);
        }
    }
}