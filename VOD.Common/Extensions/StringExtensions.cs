using System;
using System.Collections.Generic;
using System.Text;

namespace VOD.Common.Extensions
{
   public static class StringExtensions //Extensions are always static The advantage of using a static class is that the compiler can check to make sure that no instance members are accidentally added. The compiler will guarantee that instances of this class cannot be created. Static classes are sealed and therefore cannot be inherited. They cannot inherit from any class except Object.
    {
        public static bool IsEmpty (this string value) => string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value); //Check if the value is null/empty or white spaces
    }
}
