using System;

namespace OTS.Services.Common
{
    public static class Extensions
    {
        public static T IsNullEmptyDefault<T>(this string config, T defaultValue)
        {
            if (!string.IsNullOrWhiteSpace(config))
            {
                return (T)Convert.ChangeType(config, typeof(T));
            }

            return (T)Convert.ChangeType(defaultValue, typeof(T));
        }
    }
}
