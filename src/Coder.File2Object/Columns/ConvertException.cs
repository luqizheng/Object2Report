using System;

namespace Coder.File2Object.Columns
{
    public class ConvertException : Exception
    {
        public ConvertException(string value, Type targetType) : base("无法把" + value + "转为为" + targetType + "类型")
        {
        }
    }
}