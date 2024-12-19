using System;
using DotNetEnv;

namespace SAMM.DataAccess.Infrastructure
{
    public static class EnvLoader
    {
        private static bool _isLoaded = false;

        public static void LoadEnv()
        {
            if (!_isLoaded)
            {
                DotNetEnv.Env.Load("../../../../.env");
                _isLoaded = true;
            }
        }

        public static string GetEnvVariable(string key)
        {
            LoadEnv();
            return Environment.GetEnvironmentVariable(key);
        }
    }
}