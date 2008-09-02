using System;
using System.IO;

namespace GiniMonara
{
    class MetaDataUtility
    {
        public static bool checkMetaDataExsists(string fileName)
        {
            if (File.Exists(fileName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
