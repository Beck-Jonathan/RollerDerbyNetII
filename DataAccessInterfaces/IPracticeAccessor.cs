﻿using DataObjects;
using System;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface IPracticeAccessor
    {
        int insertPractice(int PracticeID, string LocationID, DateTime DateTime);
        Practice selectPracticeByPrimaryKey(int PracticeID);
        List<Practice> selectAllPractice();
        int updatePractice(int oldPracticeID, string oldLocationID, DateTime oldDateTime, string newLocationID, DateTime newDateTime);
        int deletePractice(string PracticeID);
    }

}
