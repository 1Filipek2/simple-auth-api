using System;

namespace SimpleAuthApi.Application.Common.Exceptions;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException(string message) : base(message)
    {
    }
}
