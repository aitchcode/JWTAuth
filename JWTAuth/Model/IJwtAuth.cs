﻿namespace JWTAuth.Model
{
    public interface IJwtAuth
    {
        string Authentication(string username, string password);
    }
}
