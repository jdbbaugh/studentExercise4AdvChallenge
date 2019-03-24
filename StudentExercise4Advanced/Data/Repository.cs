using System;
using System.Linq;
using  System.Collections.Generic;
using StudentExercises4.Models;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace StudentExercise4Advanced.Data
{
    class Repository
    {
        public SqlConnection Connection
        {
            get
            {
                string _connectionString =
                    "Server=DESKTOP-ALG4281\\SQLEXPRESS;Database=StudentExercises2;Trusted_Connection=True;";
                    return new SqlConnection(_connectionString);
            }
        }

        //EXERCISES

        public List<Exercise> GetAllExercises()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, ExerciseName, ExerciseLanguage FROM Exercise";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Exercise> exercises = new List<Exercise>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int exerciseNameColumnPosition = reader.GetOrdinal("ExerciseName");
                        string exerciseNameValue = reader.GetString(exerciseNameColumnPosition);

                        int exerciseLanguageColumnPosition = reader.GetOrdinal("ExerciseLanguage");
                        string exerciseLanguageValue = reader.GetString(exerciseLanguageColumnPosition);
                        Exercise exercise = new Exercise
                        {
                            Id = idValue,
                            Name = exerciseNameValue,
                            CodeLanguage = exerciseLanguageValue
                        };
                        exercises.Add(exercise);
                    }
                        reader.Close();
                        return exercises;
                }
            }
        }

        //INSTRUCTORS 

        public void AddInstructor(Instructor newInstructor)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Instructor (FirstName, LastName, SlackHandle, CohortId) VALUES (@firstName, @lastName, @slackHandle, @cohortId)";
                    cmd.Parameters.Add(new SqlParameter("@firstName", newInstructor.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@lastName", newInstructor.LastName));
                    cmd.Parameters.Add(new SqlParameter("@slackHandle", newInstructor.SlackHandle));
                    cmd.Parameters.Add(new SqlParameter("@cohortId", newInstructor.CohortNumber.Id));
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public List<Instructor> GetAllInstructors()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "SELECT i.Id, i.FirstName, i.LastName, i.SlackHandle,c.Id AS CohortID, c.CohortName FROM Instructor i INNER JOIN Cohort c ON i.CohortId = c.id";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Instructor> instructors = new List<Instructor>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int firstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string instFirstNameValue = reader.GetString(firstNameColumnPosition);

                        int lastNameColumnPosition = reader.GetOrdinal("LastName");
                        string instLastNameValue = reader.GetString(lastNameColumnPosition);

                        int slackHandleColumnPosition = reader.GetOrdinal("SlackHandle");
                        string slackHandleValue = reader.GetString(lastNameColumnPosition);

                        int cohoritIdColumnPosition = reader.GetOrdinal("CohortId");
                        int cohortId = reader.GetInt32(cohoritIdColumnPosition);

                        int cohortNameColumnPosition = reader.GetOrdinal("CohortName");
                        string cohortName = reader.GetString(cohortNameColumnPosition);

                        Cohort instructorsCohort = new Cohort
                        {
                            Id = cohortId,
                            Name = cohortName
                        };

                        Instructor instructor = new Instructor(instFirstNameValue, instLastNameValue, slackHandleValue, instructorsCohort);

                        instructors.Add(instructor);
                    }
                    reader.Close();
                    return instructors;
                }
            }
        }

        //STUDENTS

        public void AddStudent(Student newStudent)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Student (FirstName, LastName, SlackHandle, CohortId) VALUES (@firstName, @lastName, @slackHandle, @cohortId)";
                    cmd.Parameters.Add(new SqlParameter("@firstName", newStudent.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@lastName", newStudent.LastName));
                    cmd.Parameters.Add(new SqlParameter("@slackHandle", newStudent.SlackHandle));
                    cmd.Parameters.Add(new SqlParameter("@cohortId", newStudent.CohortNumber.Id));
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public List<Student> StudentByCohort(int userInput)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT FirstName, LastName, SlackHandle FROM Student WHERE CohortId = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", userInput));
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Student> students = new List<Student>();

                    while (reader.Read())
                    {
                        int stuFirstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string stuFirstNameValue = reader.GetString(stuFirstNameColumnPosition);

                        int stuLastNameColumnPosition = reader.GetOrdinal("LastName");
                        string stuLastNameValue = reader.GetString(stuLastNameColumnPosition);

                        int stuSlackNameColumnPosition = reader.GetOrdinal("SlackHandle");
                        string stuSlackNameValue = reader.GetString(stuSlackNameColumnPosition);

                        Student student = new Student
                        {
                            FirstName = stuFirstNameValue,
                            LastName = stuLastNameValue,
                            SlackHandle = stuSlackNameValue
                        };

                        students.Add(student);
                    }
                    reader.Close();
                    return students;
                }
            }
        }

        public List<Student> StudentSearchByLast()
        {
            Console.WriteLine("What is the students last name?");
            string userInput = Console.ReadLine();
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT FirstName, LastName, SlackHandle FROM Student WHERE LastName = @lastName";
                    cmd.Parameters.Add(new SqlParameter("@lastName", userInput));
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Student> students = new List<Student>();

                    while (reader.Read())
                    {
                        int stuFirstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string stuFirstNameValue = reader.GetString(stuFirstNameColumnPosition);

                        int stuLastNameColumnPosition = reader.GetOrdinal("LastName");
                        string stuLastNameValue = reader.GetString(stuLastNameColumnPosition);

                        int stuSlackNameColumnPosition = reader.GetOrdinal("SlackHandle");
                        string stuSlackNameValue = reader.GetString(stuSlackNameColumnPosition);

                        Student student = new Student
                        {
                            FirstName = stuFirstNameValue,
                            LastName = stuLastNameValue,
                            SlackHandle = stuSlackNameValue
                        };

                        students.Add(student);
                    }
                    reader.Close();
                    return students;
                }
            }
        }

        public List<Student> GetAllStudentsBasic()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT FirstName, LastName, SlackHandle FROM Student";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Student> students = new List<Student>();

                    while (reader.Read())
                    {
                        int firstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string firstNameValue = reader.GetString(firstNameColumnPosition);

                        int lastNameColumnPosition = reader.GetOrdinal("LastName");
                        string lastNameValue = reader.GetString(lastNameColumnPosition);

                        int slackHandleColumnPosition = reader.GetOrdinal("SlackHandle");
                        string slackHandleValue = reader.GetString(slackHandleColumnPosition);

                        Student student = new Student
                        {
                            FirstName = firstNameValue,
                            LastName = lastNameValue,
                            SlackHandle = slackHandleValue,
                        };

                        students.Add(student);
                    }
                    reader.Close();
                    return students;
                }
            }
        }
        //====================================================================================
        //Cohort
        //====================================================================================
        public void AddCohort(string userInput)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Cohort (CohortName) VALUES (@cohortName)";
                    cmd.Parameters.Add(new SqlParameter("@cohortName", userInput));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Cohort> GetallCohorts()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, CohortName FROM Cohort";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Cohort> cohorts = new List<Cohort>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int cohortNameColumnPosition = reader.GetOrdinal("CohortName");
                        string cohortNameValue = reader.GetString(cohortNameColumnPosition);

                        Cohort cohort = new Cohort
                        {
                            Id = idValue,
                            Name = cohortNameValue
                        };

                        cohorts.Add(cohort);
                    }
                    reader.Close();
                    return cohorts;
                }
            }
        }
    }
}
