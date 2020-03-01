using System;

namespace FoodCalc.Common.Exceptions
{
    public class BusinessException : Exception
    {
        public bool UseMarkdownFormatting { get; private set; }
        
        public BusinessException(string message, bool useMarkdownFormatting = false) : base(message)
        {
            UseMarkdownFormatting = useMarkdownFormatting;
        }
    }
}