using DataObjects;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface IExercisePracticeLineManager
    {
        int addexercise_practice_line(string ExerciseID, int PracticeID, int Count);
        exercise_practice_line getexercise_practice_lineByPrimaryKey(string exercise_practice_lineID);
        List<exercise_practice_line> getAllexercise_practice_line();
        int editexercise_practice_line(string oldExerciseID, int oldPracticeID, int oldCount, string newExerciseID, int newPracticeID, int newCount);
        int purgeexercise_practice_line(string exercise_practice_lineID);
    }

}