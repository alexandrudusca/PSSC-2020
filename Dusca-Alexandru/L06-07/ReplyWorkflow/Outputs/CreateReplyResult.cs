using System;
using System.Collections.Generic;
using System.Text;
using ReplyWorkflow.Models;

namespace ReplyWorkflow.Outputs
{

    public static partial class CreateReplyResult
    {
        public interface ICreateReplyResult
         { 
             
         }

        public class ReplyValid : ICreateReplyResult
        {
            public Reply Reply { get; }

            public ReplyValid(Reply reply)
            {
                Reply = reply;
            }
        }

        public class ReplyInvalid : ICreateReplyResult
        {
            public string Reasons { get; }

            public ReplyInvalid(string reasons)
            {
                Reasons = reasons;
            }
        }

        public class InvalidRequest : ICreateReplyResult
        {
            public string Msg { get; }

            public InvalidRequest(string msg)
            {
                Msg = msg;
            }
        }

    }
}
