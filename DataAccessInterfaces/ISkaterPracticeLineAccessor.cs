using DataObjects;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface ISkaterpracticeLineAccessor
    {
        int insertSkater_practice_Line(string SkaterID, int PracticeID);
        Skater_practice_Line selectSkater_practice_LineByPrimaryKey(string SkaterID, int PracticeID);
        List<Skater_practice_Line> selectAllSkater_practice_Line();
        int updateSkater_practice_Line(string oldSkaterID, int oldPracticeID, string newSkaterID, int newPracticeID);
        int deleteSkater_practice_Line(string Skater_practice_LineID);
    }
}
