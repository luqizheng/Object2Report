using System;
using System.Collections.Generic;
using System.Text;

namespace Coder.File2Object
{
    public class File2ObjectException : Exception
    {
        public File2ObjectException()
        {
            
        }
        public File2ObjectException(string message) : base(message)
        {

        }

        public File2ObjectException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }

    public class TitleNotMatchSettingException : File2ObjectException
    {
        public TitleNotMatchSettingException(string settingName,string actualName) : base($"文件标题不一致(设定:{settingName},实际:{actualName})，请检查是不是上传错文件")
        {
        }
    }
}
