﻿namespace Xaelith.Micro.Infrastructure.DataModel.Core.Authentication;

public class UserCredentials
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool RememberLogin { get; set; }
}