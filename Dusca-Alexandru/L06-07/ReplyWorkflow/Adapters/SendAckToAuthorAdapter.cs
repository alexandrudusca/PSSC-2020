using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using LanguageExt;
using ReplyWorkflow.Inputs;
using ReplyWorkflow.Models;
using ReplyWorkflow.Outputs;
using static LanguageExt.Prelude;

namespace ReplyWorkflow.Adapters
{
    public class SendAckToAuthorAdapter : Adapter<SendAckToAuthorCmd, SendAckToAuthorResult.ISendAckToAuthorResult, QuestionWriteContext>
    {
        public override Task PostConditions(SendAckToAuthorCmd cmd, SendAckToAuthorResult.ISendAckToAuthorResult result, QuestionWriteContext state)
        {
            return Task.CompletedTask;
        }

        public override async Task<SendAckToAuthorResult.ISendAckToAuthorResult> Work(SendAckToAuthorCmd cmd, QuestionWriteContext state)
        {
            var wf = from isValid in cmd.TryValidate()
                     from sendAcknowledgement in SendAcknowledgement(cmd, state)
                     select sendAcknowledgement;

            return await wf.Match(
                Succ: success => success,
                Fail: ex => new SendAckToAuthorResult.AckToAuthorFailed(ex.ToString()));
        }

        private TryAsync<SendAckToAuthorResult.ISendAckToAuthorResult> SendAcknowledgement(SendAckToAuthorCmd cmd, QuestionWriteContext state)
        {
            var Id = Guid.NewGuid();

            return TryAsync<SendAckToAuthorResult.ISendAckToAuthorResult>(async () =>
            {
                if(new Random().Next(10) == 1)
                    return new SendAckToAuthorResult.AckToAuthorFailed("Oops! Something went wrong!");
                else
                    return new SendAckToAuthorResult.AckToAuthorSent("Your reply is now added!");
            });
        }
    }
}