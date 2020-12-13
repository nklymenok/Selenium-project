using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal.Commands;
using System;

namespace Milliman.Pixel.Web.Tests.PageObjects
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    class CustomRetry : PropertyAttribute, IWrapSetUpTearDown
    {
        private const int RETRY_COUNT = 2;
        public CustomRetry() : base(RETRY_COUNT)
        {
        }

        public TestCommand Wrap(TestCommand command)
        {
            return new CustomRetryCommand(command, RETRY_COUNT);
        }
    }
}

