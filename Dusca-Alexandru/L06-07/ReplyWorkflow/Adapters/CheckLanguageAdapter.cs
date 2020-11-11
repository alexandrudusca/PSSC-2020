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
    public class CheckLanguageAdapter : Adapter<CheckLanguageCmd, CheckLanguageResult.ICheckLanguageResult, QuestionWriteContext>
    {
        public override Task PostConditions(CheckLanguageCmd cmd, CheckLanguageResult.ICheckLanguageResult result, QuestionWriteContext state)
        {
            return Task.CompletedTask;
        }

        public override async Task<CheckLanguageResult.ICheckLanguageResult> Work(CheckLanguageCmd cmd, QuestionWriteContext state)
        {
            var wf = from isValid in cmd.TryValidate()
                     from checkLanguageWithML in CheckLanguageWithML(cmd, state)
                     select checkLanguageWithML;

            return await wf.Match(
                Succ: success => success,
                Fail: ex => new CheckLanguageResult.ErrorText(ex.ToString()));
        }

        private TryAsync<CheckLanguageResult.ICheckLanguageResult> CheckLanguageWithML(CheckLanguageCmd cmd, QuestionWriteContext state)
        {
            var Id = Guid.NewGuid();

            return TryAsync<CheckLanguageResult.ICheckLanguageResult>(async () =>
            {
                if(new Random().Next(10) == 1)
                    return new CheckLanguageResult.ErrorText("ML failed!");
                else
                    return new CheckLanguageResult.SafeText("ML finished with success!");
            });
        }
    }
}