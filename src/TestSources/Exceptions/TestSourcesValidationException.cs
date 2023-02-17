using System;
using System.Collections.Generic;
using System.Text;

namespace TestSources.Exceptions;

public class TestSourcesValidationException : Exception
{
    public TestSourcesValidationException()
    {
    }

    public TestSourcesValidationException(string message)
        : base(message)
    {
    }

    public TestSourcesValidationException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
