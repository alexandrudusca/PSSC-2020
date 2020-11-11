using System.ComponentModel.DataAnnotations;
using ReplyWorkflow.Outputs;

namespace ReplyWorkflow.Inputs
{
    public class SendAckToOwnerCmd
    {
        //product type
        
        //send notification to the question owner
        [Required]
        //get acknowledgement type
        public CheckLanguageResult.SafeText SafeAck;
        [Required]
        //get id of the owner
        public int QuestionId { get; }
        [Required]
        //get the author's id who replied to the question
        public int AutoherId { get; }

        public SendAckToOwnerCmd(CheckLanguageResult.SafeText ack, int questionid, int authorid)
        {
            SafeAck = ack;
            QuestionId = questionid;
            AutoherId = authorid;
        }
    }
}