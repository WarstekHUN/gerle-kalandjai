﻿namespace Gerle_Lib.BaseClasses
{
    class EndCredit
    {
        public string Role { get; init; }
        public string Name { get; init; }

        public EndCredit(string role, string name)
        {
            Role = role;
            Name = name;
        }
    }
}