using DataObjects;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface IExerciseAccessor
    {
        int insertExercise(string ExerciseID, int Exercise_count, string Exercise_units);
        Exercise selectExerciseByPrimaryKey(string ExerciseID);
        List<Exercise> selectAllExercise();
        int updateExercise(string oldExerciseID, int oldExercise_count, string oldExercise_units, int newExercise_count, string newExercise_units);
        int deleteExercise(string ExerciseID);
    }

}
