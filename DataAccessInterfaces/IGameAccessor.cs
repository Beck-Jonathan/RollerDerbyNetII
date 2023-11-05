using DataObjects;
using System;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface IGameAccessor
    {
        int insertGame(int GameID, string LocationID, DateTime Date);
        Game selectGameByPrimaryKey(int GameID);
        List<Game> selectAllGame();
        int updateGame(int oldGameID, string oldLocationID, DateTime oldDate, string newLocationID, DateTime newDate);
        int deleteGame(string GameID);
    }
}
