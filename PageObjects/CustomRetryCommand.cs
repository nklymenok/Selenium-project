using System;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Commands;

namespace Milliman.Pixel.Web.Tests.PageObjects
{
    public class CustomRetryCommand : DelegatingTestCommand
    {
        private int RetryCount;

        public CustomRetryCommand(TestCommand innerCommand, int retryCount) : base(innerCommand)
        {
            RetryCount = retryCount;
        }

        public override TestResult Execute(TestExecutionContext context)
        {
            int count = RetryCount;

            while (count-- > 0)
            {
                context.CurrentResult = innerCommand.Execute(context);
                var results = context.CurrentResult.ResultState;

                Console.WriteLine($"Attempt of Custom Retry: {RetryCount - count} of {RetryCount}");

                if (results != ResultState.Error &&
                    results != ResultState.Failure &&
                    results != ResultState.SetUpError &&
                    results != ResultState.SetUpFailure &&
                    results != ResultState.TearDownError &&
                    results != ResultState.ChildFailure &&
                    results != ResultState.Inconclusive)
                {
                    break;
                }
            }

            Console.WriteLine($"Custom retry executed due to {context.CurrentResult.Name}");

            return context.CurrentResult;
        }
    }
}
