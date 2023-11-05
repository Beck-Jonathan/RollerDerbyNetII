using DataObjects;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface IGame_Team_LineManager
    {
        int addGame_Team_Line(int GameID, string TeamId, int Points);
        Game_Team_Line getGame_Team_LineByPrimaryKey(string Game_Team_LineID);
        List<Game_Team_Line> getAllGame_Team_Line();
        int editGame_Team_Line(int oldGameID, string oldTeamId, int oldPoints, int newGameID, string newTeamId, int newPoints);
        int purgeGame_Team_Line(string Game_Team_LineID);
    }
}
