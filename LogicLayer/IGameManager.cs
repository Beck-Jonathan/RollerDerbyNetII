using DataObjects;
using System;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface IGameManager
    {
        int addGame(int GameID, string LocationID, DateTime Date);
        Game getGameByPrimaryKey(string GameID);
        List<Game> getAllGame();
        int editGame(int oldGameID, string oldLocationID, DateTime oldDate, int newGameID, string newLocationID, DateTime newDate);
        int purgeGame(string GameID);
    }
}
