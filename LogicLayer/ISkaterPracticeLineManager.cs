using DataObjects;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface ISkaterpracticeLineManager
    {
        int addSkater_practice_Line(string SkaterID, int PracticeID);
        Skater_practice_Line getSkater_practice_LineByPrimaryKey(string Skater_practice_LineID);
        List<Skater_practice_Line> getAllSkater_practice_Line();
        int editSkater_practice_Line(string oldSkaterID, int oldPracticeID, string newSkaterID, int newPracticeID);
        int purgeSkater_practice_Line(string Skater_practice_LineID);
    }
}
