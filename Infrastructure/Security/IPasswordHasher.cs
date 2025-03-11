﻿namespace verticalSliceArchitecture.Infrastructure.Security
{
    public interface IPasswordHasher
    {
        string Hash(string password);

        bool Verify(string password, string passwordHash);
    }
}
