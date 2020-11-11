namespace ReplyWorkflow.Outputs
{
    public static partial class SendAckToOwnerResult
    {
        //sum type
        public interface ISendAckToOwnerResult 
        {

        }

        //case acknowledgment was sent with success
        public class AckToOwnerSent : ISendAckToOwnerResult 
        {
            public string Acknowledgment { get; }

            public AckToOwnerSent(string ack)
            {
                Acknowledgment = ack;
            }
        }

        //case acknowledgment was sent with an error message 
        public class AckToOwnerFailed : ISendAckToOwnerResult
        {
            public string FailedAcknowledgment { get; }

            public AckToOwnerFailed( string failedAck)
            {
                FailedAcknowledgment = failedAck;
            }
        }
    }
}