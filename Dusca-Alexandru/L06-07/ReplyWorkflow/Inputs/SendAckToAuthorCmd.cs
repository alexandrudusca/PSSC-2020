using System.ComponentModel.DataAnnotations;
using ReplyWorkflow.Outputs;

namespace ReplyWorkflow.Inputs
{
    public class SendAckToAuthorCmd
    {
        //product type
        
        //send notification to the author of the reply
        [Required]
        public CheckLanguageResult.SafeText safeAck;
        //get acknowledgement type
        
        [Required]
        //get author path
        public int AuthorId { get; }
        [Required]
        //get question path
        public int QuestionId { get; }

        public SendAckToAuthorCmd(CheckLanguageResult.SafeText ack, int authorid, int questionid)
        {
            safeAck = ack;
            AuthorId = authorid;
            QuestionId = questionid;
        }
    }
}