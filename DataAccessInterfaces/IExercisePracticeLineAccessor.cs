using DataObjects;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface Iexercise_practice_lineAccessor
    {
        int insertexercise_practice_line(string ExerciseID, int PracticeID, int Count);
        exercise_practice_line selectexercise_practice_lineByPrimaryKey(string ExerciseID, int PracticeID);
        List<exercise_practice_line> selectAllexercise_practice_line();
        int updateexercise_practice_line(string oldExerciseID, int oldPracticeID, int oldCount, int newCount);
        int deleteexercise_practice_line(string exercise_practice_lineID);
    }
}