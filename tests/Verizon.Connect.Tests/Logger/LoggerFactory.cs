namespace Verizon.Connect.Tests.Logger
{
    using Microsoft.Extensions.Logging;
    using NSubstitute;
    using System;

    using Xunit.Abstractions;

    public static class LoggerFactory
    {
        public static ILogger<T> CreateLogger<T>(ITestOutputHelper output)
        {
            var logger = Substitute.For<ILogger<T>>();

            logger
                .When(method => method.Log(Arg.Any<LogLevel>(), Arg.Any<EventId>(), Arg.Any<object>(), Arg.Any<Exception>(), Arg.Any<Func<object, Exception, string>>()))
                .Do(args => output.WriteLine(args[2].ToString()));

            return logger;
        }
    }
}