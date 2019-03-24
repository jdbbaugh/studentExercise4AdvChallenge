using System;
using System.Collections.Generic;
using System.Text;

namespace StudentExercises4.Models
{
    class Instructor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SlackHandle { get; set; }
        public Cohort CohortNumber { get; set; }

        public Instructor(string firstName, string lastName, string slack, Cohort cohort)
        {
            Id = Id;
            FirstName = firstName;
            LastName = lastName;
            SlackHandle = slack;
            CohortNumber = cohort;
        }

        public Instructor()
        {

        }

        public void AssignExercise(Exercise exercise)
        {

            // Cohort list of students
            foreach (Student stu in CohortNumber.Students)
            {
                stu.CurrentExercises.Add(exercise);
                Console.WriteLine($"{stu.FirstName} was asssigned {exercise.Name} by {FirstName}");

            }
            // student.CurrentExercises.Add(exercise);

        }
    }
}
