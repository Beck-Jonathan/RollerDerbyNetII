using DataObjects;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface IGame_Team_LineAccessor
    {
        int insertGame_Team_Line(int GameID, string TeamId, int Points);
        Game_Team_Line selectGame_Team_LineByPrimaryKey(int GameID, string TeamId);
        List<Game_Team_Line> selectAllGame_Team_Line();
        int updateGame_Team_Line(int oldGameID, string oldTeamId, int oldPoints, int newPoints);
        int deleteGame_Team_Line(string Game_Team_LineID);
    }
}
