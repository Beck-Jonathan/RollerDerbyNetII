using DataObjects;
using System;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface IPracticeManager
    {
        int addPractice(int PracticeID, string LocationID, DateTime DateTime);
        Practice getPracticeByPrimaryKey(string PracticeID);
        List<Practice> getAllPractice();
        int editPractice(int oldPracticeID, string oldLocationID, DateTime oldDateTime, int newPracticeID, string newLocationID, DateTime newDateTime);
        int purgePractice(string PracticeID);
    }

}
