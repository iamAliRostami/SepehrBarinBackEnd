﻿using Hiwapardaz.SepehrBarin.Common.Exceptions;
using Hiwapardaz.SepehrBarin.Common.Literals;

namespace Hiwapardaz.SepehrBarin.Common.Extensions
{
    public static class Guard
    {
        public static void NotNull(this object obj)
        {
            if (obj is null)
            {
                throw new ArgumentIsNullException(nameof(obj));
            }
        }
        public static void NotNull(this object obj, string name)
        {
            if (obj is null)
            {
                throw new ArgumentIsNullException(name);
            }
        }
        public static string NotEmptyString(this object? value, string name)
        {
            var s = value as string ?? value?.ToString();
            if (s == null)
            {
                throw new ArgumentIsNullException(name);
            }
            return string.IsNullOrWhiteSpace(s) ? throw new ArgumentException(ExceptionLiterals.EmptyString, name) : s;
        }
    }
}