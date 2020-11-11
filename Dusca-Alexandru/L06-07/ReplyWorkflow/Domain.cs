using System;
using System.Collections.Generic;
using System.Text;
using Access.Primitives.IO;
using LanguageExt;
using ReplyWorkflow.Inputs;
using ReplyWorkflow.Outputs;
using static PortExt;

namespace ReplyWorkflow
{
    public static class BoundedContextDSL
    {

        /// <summary>
        /// CreateReplyCmd -> ICreateReplyResult
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="userId"></param>
        /// <param name="answer"></param>
        /// <returns></returns>
        
        // validate a new reply
        public static Port<CreateReplyResult.ICreateReplyResult> ValidateReply(int questionId, int userId, string answer)
            => NewPort<CreateReplyCmd, CreateReplyResult.ICreateReplyResult>(new CreateReplyCmd(userId, questionId, answer));

        //check reply with ML
        public static Port<CheckLanguageResult.ICheckLanguageResult> CheckLanguage(string text)
            => NewPort<CheckLanguageCmd, CheckLanguageResult.ICheckLanguageResult>(new CheckLanguageCmd(text));

        //send notification to the question owner 
        public static Port<SendAckToOwnerResult.ISendAckToOwnerResult> SendAckToOwner(CheckLanguageResult.SafeText ack, int questionid, int authorid) 
            => NewPort<SendAckToOwnerCmd, SendAckToOwnerResult.ISendAckToOwnerResult>(new SendAckToOwnerCmd(ack, questionid, authorid));

        //send notification to the reply owner
        public static Port<SendAckToAuthorResult.ISendAckToAuthorResult> SendAckToAuthor(CheckLanguageResult.SafeText ack, int authorid, int questionid) 
            => NewPort<SendAckToAuthorCmd, SendAckToAuthorResult.ISendAckToAuthorResult>(new SendAckToAuthorCmd(ack, authorid, questionid));


    }
}
