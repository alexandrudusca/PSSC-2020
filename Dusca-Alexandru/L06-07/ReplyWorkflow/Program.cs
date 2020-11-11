using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Authentication.ExtendedProtection;
using System.Threading.Tasks;
using Access.Primitives.IO;
using LanguageExt;
using Microsoft.Extensions.DependencyInjection;
using ReplyWorkflow.Adapters;
using ReplyWorkflow.Outputs;

namespace ReplyWorkflow
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var wf = from createReplyResult in BoundedContextDSL.ValidateReply(100, 1, "What is DDD? I would like to know more details about it.")
                     let validReply = (CreateReplyResult.ReplyValid)createReplyResult //3 outputs
                     from checkLanguageResult in BoundedContextDSL.CheckLanguage(validReply.Reply.Answer)
                     from ownerAck in BoundedContextDSL.SendAckToOwner((CheckLanguageResult.SafeText)checkLanguageResult, 100, 1)
                     from authorAck in BoundedContextDSL.SendAckToAuthor((CheckLanguageResult.SafeText)checkLanguageResult, 1, 100)
                     select (validReply, checkLanguageResult, ownerAck, authorAck);

            var serviceProvider = new ServiceCollection()
                .AddOperations(typeof(ValidateReplyAdapter).Assembly) //port connection
                .AddOperations(typeof(CheckLanguageAdapter).Assembly)
                .AddOperations(typeof(SendAckToOwnerAdapter).Assembly)
                .AddOperations(typeof(SendAckToAuthorAdapter).Assembly)
                .AddTransient<IInterpreterAsync>(sp => new LiveInterpreterAsync(sp))
                .BuildServiceProvider();

            var interpreter = serviceProvider.GetService<IInterpreterAsync>();

            var writeContext = new QuestionWriteContext(new List<int>() { 1, 2, 3 }, new List<int>() { 100, 101, 102 });
            var result = await interpreter.Interpret(wf, writeContext); //author id                    //question id


            Console.WriteLine("----- Finished -----");
        }

    }

    internal interface IReplyPosted
    {
    }
}
