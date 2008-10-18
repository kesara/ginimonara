using System;
using System.IO;
using System.Security.Cryptography;

/*
 * Signature - GiniMonara Media File Signature
 * Developer: Kesara Nanayakkara Rathnayake < kesara@bcs.org >
 * Copyright (C) 2008 GiniMonara Team
 * 
 * This file is part of GiniMonara.
 * 
 * GiniMonara is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License.
 * 
 * GiniMonara is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with GiniMonara.  If not, see <http://www.gnu.org/licenses/>.
 * 
 */

namespace GiniMonara.Utilities
{
    class Signature
    {
        public static string getSignature(string fileName)
        {
            string signature = "";
            byte[] hash;
            MD5CryptoServiceProvider md5Hash = new MD5CryptoServiceProvider();
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            hash = md5Hash.ComputeHash(fileStream);
            fileStream.Close();

            signature = BitConverter.ToString(hash);
            signature = signature.Replace("-", "");

            return signature;
        }
    }
}
