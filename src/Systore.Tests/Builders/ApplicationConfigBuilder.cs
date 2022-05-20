using System;
using Systore.CrossCutting;

namespace Systore.Tests.Builders;

public static partial class Builder
{
    public static ApplicationConfig ApplicationConfig => new()
    {
        Secret = "123456789012345678901234567890",
        ConnectionStrings = new()
        {
            DefaultConnection = "connection"
        },
        ReleaseConfig = new()
        {
            BaseUrl = new Uri("http://example.com"),
            ClientId = "client-id"
        }
    };
}