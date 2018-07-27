﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Interfaces
{
    public interface IOnlineService
    {
        void UserOnline(int userId);
        void UserOffline(int userId);
    }
}
