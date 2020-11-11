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
    public class SendAckToOwnerAdapter : Adapter<SendAckToOwnerCmd, SendAckToOwnerResult.ISendAckToOwnerResult, QuestionWriteContext>
    {
        public override Task PostConditions(SendAckToOwnerCmd cmd, SendAckToOwnerResult.ISendAckToOwnerResult result, QuestionWriteContext state)
        {
            return Task.CompletedTask;
        }

        public override async Task<SendAckToOwnerResult.ISendAckToOwnerResult> Work(SendAckToOwnerCmd cmd, QuestionWriteContext state)
        {
            var wf = from isValid in cmd.TryValidate()
                     from sendAcknowledgement in SendAcknowledgement(cmd, state)
                     select sendAcknowledgement;

            return await wf.Match(
                Succ: success => success,
                Fail: ex => new SendAckToOwnerResult.AckToOwnerFailed(ex.ToString()));
        }

        private TryAsync<SendAckToOwnerResult.ISendAckToOwnerResult> SendAcknowledgement(SendAckToOwnerCmd cmd, QuestionWriteContext state)
        {
            var Id = Guid.NewGuid();

            return TryAsync<SendAckToOwnerResult.ISendAckToOwnerResult>(async () =>
            {
                if(new Random().Next(10) == 1)
                    return new SendAckToOwnerResult.AckToOwnerFailed("Oops! Something went wrong!");
                else
                    return new SendAckToOwnerResult.AckToOwnerSent("Your question received a new reply!");
            });
        }
    }
}