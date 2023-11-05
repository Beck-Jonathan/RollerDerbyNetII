﻿using DataObjects;
using System;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface ISkaterManager
    {
        SkaterVM LoginSkater(string email, string password);
        //helpers but public so we can use them for other things
        bool AuthenticateSkater(string email, string password);
        String hashSHA256(string source);
        SkaterVM GetSkaterVMByDerbyName(string derbyName);
        List<string> getRolesBySkaterDerbyName(string derbyName);
        bool resetPassword(string derbyName, string oldPassword, string newPassword);



    }
}