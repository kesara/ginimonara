﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * YouTubeSecrets - YouTubeSecrets data class
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

namespace GiniMonara.UserSecrets
{
    class YouTubeSecrets
    {
        public string username { get; set; }
        public string password { get; set; }

        public YouTubeSecrets(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
