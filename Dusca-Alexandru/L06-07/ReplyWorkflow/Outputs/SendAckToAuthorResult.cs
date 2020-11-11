namespace ReplyWorkflow.Outputs
{
    public static partial class SendAckToAuthorResult
    {
        //sum type
        public interface ISendAckToAuthorResult 
        {

        }

        //Acknowledgment of success sent to the author
        public class AckToAuthorSent : ISendAckToAuthorResult 
        {
            public string Acknowledgment { get; }

            public AckToAuthorSent(string ack)
            {
                Acknowledgment = ack;
            }
        }

        //Acknowledgment of an invalid reply sent to the author
        public class AckToAuthorFailed : ISendAckToAuthorResult 
        {
            public string FailedAcknowledgment { get; }

            public AckToAuthorFailed( string failedAck)
            {
                FailedAcknowledgment = failedAck;
            }
        }
    }
}