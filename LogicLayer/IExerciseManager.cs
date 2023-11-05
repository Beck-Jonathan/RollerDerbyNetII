using DataObjects;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface IExerciseManager
    {
        int addExercise(string ExerciseID, int Exercise_count, string Exercise_units);
        Exercise getExerciseByPrimaryKey(string ExerciseID);
        List<Exercise> getAllExercise();
        int editExercise(string oldExerciseID, int oldExercise_count, string oldExercise_units, string newExerciseID, int newExercise_count, string newExercise_units);
        int purgeExercise(string ExerciseID);
    }

}
