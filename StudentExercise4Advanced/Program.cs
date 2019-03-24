﻿using System;
using System.Collections.Generic;
using System.Linq;
using StudentExercise4Advanced.Data;
using StudentExercises4.Models;

namespace StudentExercise4Advanced
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("------------------------");
                Console.WriteLine("Choose a menu option:");
                Console.WriteLine("1. Dispaly all Students.");
                Console.WriteLine("2. Dispaly all Instructors.");
                Console.WriteLine("3. Dispaly all Exercises.");
                Console.WriteLine("4. Dispaly all Cohorts.");
                Console.WriteLine("5. Search Students by last name.");
                Console.WriteLine("6. Create New Cohort.");
                Console.WriteLine("7. Create New Student.");
                Console.WriteLine("8. Create New Instructor.");
                Console.WriteLine("9. Display specific cohort students.");
                Console.WriteLine("10. Move existing student to new cohort.");
                Console.WriteLine("11. List specific students exercises.");
                Console.WriteLine("11. Assign specific student specific exercise.");
                Console.WriteLine("12. Exit.");
                Console.WriteLine();
                Console.WriteLine();

                string option = Console.ReadLine();


                Repository repository = new Repository();
                switch (option)
                {
                    case "1":

                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("ALL STUDENTS---------------");

                        List<Student> allStudentsBasicList = repository.GetAllStudentsBasic();
                        int studentCounter = 0;
                        foreach (Student student in allStudentsBasicList)
                        {
                            ++studentCounter;
                            Console.WriteLine($"{studentCounter}: {student.FirstName} {student.LastName}");
                        }
                        break;
                    case "2":
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("ALL INSTRUCTORS ---------------------");
                        List<Instructor> allInstructorsList = repository.GetAllInstructors();
                        int instructorCounter = 0;
                        foreach (Instructor instructor in allInstructorsList)
                        {
                            ++instructorCounter;
                            Console.WriteLine($"{instructorCounter}: {instructor.FirstName} {instructor.LastName} is the instructor for {instructor.CohortNumber.Name}");
                        }
                        break;
                    case "3":
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("ALL EXERCISES ---------------------");
                        List<Exercise> exercises = repository.GetAllExercises();
                        int exerciseCounter = 0;
                        foreach (Exercise assignment in exercises)
                        {
                            ++exerciseCounter;
                            Console.WriteLine($"{exerciseCounter}: {assignment.Name} using {assignment.CodeLanguage}");
                        }
                        break;
                    case "4":
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("ALL COHORTS ---------------------");
                        List<Cohort> cohorts = repository.GetallCohorts();
                        int cohortCounter = 0;
                        foreach (Cohort cohort in cohorts)
                        {
                            ++cohortCounter;
                            Console.WriteLine($"{cohortCounter}: {cohort.Name}");
                        }
                        break;
                    case "5":
                        Console.WriteLine();
                        Console.WriteLine();
                        List<Student> studentByLast = repository.StudentSearchByLast();
                        foreach (Student stu in studentByLast)
                        {
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine("Student search complete----------------------");
                            Console.WriteLine($"{stu.FirstName} {stu.LastName} SlackHandle: {stu.SlackHandle}");
                        }
                        break;
                    case "6":
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("What would you like to name the Cohort?");
                        string newCohortName = Console.ReadLine();
                        repository.AddCohort(newCohortName);
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Cohort added.");
                        break;
                    case "7":
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("What is the first name of the new student?");
                        string newStudentFirstName = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("What is the last name of the new student?");
                        string newStudentLastName = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("What is the slack handle for this new student?");
                        string newStudentSlackHandle= Console.ReadLine();

                        Console.WriteLine();
                        Console.WriteLine();
                        List<Cohort> cohortsToChoose = repository.GetallCohorts();
                        Console.WriteLine($"Select Cohort by entering 1 - {cohortsToChoose.Count} ---------------------");
                        int cohortOptionCounter = 0;
                        foreach (Cohort cohort in cohortsToChoose)
                        {
                            ++cohortOptionCounter;
                            Console.WriteLine($"{cohortOptionCounter}: {cohort.Name}");
                        }

                        int chosenCohort = Int32.Parse(Console.ReadLine());

                        Student newStudentToBeAdded = new Student
                        {
                            FirstName = newStudentFirstName,
                            LastName = newStudentLastName,
                            SlackHandle = newStudentSlackHandle,
                            CohortNumber = cohortsToChoose[--chosenCohort]
                        };

                        repository.AddStudent(newStudentToBeAdded);

                        Console.WriteLine($"{newStudentToBeAdded.FirstName} is now in {cohortsToChoose[chosenCohort].Name}");
                        
                        break;
                    case "8":
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("What is the first name of the new instructor?");
                        string newInstructorFirstName = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("What is the last name of the new instructor?");
                        string newInstructorLastName = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("What is the slack handle for this new instructor?");
                        string newInstructorSlackHandle = Console.ReadLine();

                        Console.WriteLine();
                        Console.WriteLine();
                        List<Cohort> instructorCohortsToChoose = repository.GetallCohorts();
                        Console.WriteLine($"Select Cohort by entering 1 - {instructorCohortsToChoose.Count} ---------------------");
                        int instructorCohortOptionCounter = 0;
                        foreach (Cohort cohort in instructorCohortsToChoose)
                        {
                            ++instructorCohortOptionCounter;
                            Console.WriteLine($"{instructorCohortOptionCounter}: {cohort.Name}");
                        }

                        int instructorChosenCohort = Int32.Parse(Console.ReadLine());

                        Instructor newInstructorToBeAdded = new Instructor
                        {
                            FirstName = newInstructorFirstName,
                            LastName = newInstructorLastName,
                            SlackHandle = newInstructorSlackHandle,
                            CohortNumber = instructorCohortsToChoose[--instructorChosenCohort]
                        };

                        repository.AddInstructor(newInstructorToBeAdded);

                        Console.WriteLine($"{newInstructorToBeAdded.FirstName} is now teaching {instructorCohortsToChoose[instructorChosenCohort].Name}");
                        
                        break;
                    case "9":
                        Console.WriteLine();
                        Console.WriteLine();
                        List<Cohort> chooseACohort = repository.GetallCohorts();
                        Console.WriteLine($"Select Cohort by entering 1 - {chooseACohort.Count} ---------------------");
                        int choosingCohortOptionCounter = 0;
                        foreach (Cohort cohort in chooseACohort)
                        {
                            ++choosingCohortOptionCounter;
                            Console.WriteLine($"{choosingCohortOptionCounter}: {cohort.Name}");
                        }
                        int theeChosenCohort = Int32.Parse(Console.ReadLine());
                        List<Student> studentsInCohort = repository.StudentByCohort(theeChosenCohort);
                        Console.WriteLine();
                        int cohortStudentCounter = 0;
                        Console.WriteLine($"{chooseACohort[--theeChosenCohort].Name} Students:");
                        foreach (Student student in studentsInCohort)
                        {
                            ++cohortStudentCounter;
                            Console.WriteLine($"{cohortStudentCounter}: {student.FirstName} {student.LastName}");
                        }
                        Console.WriteLine();
                        break;
                    default:
                        Console.WriteLine("Goodbye");
                        break;
                }
                

                /*
                if (option == "1")
                {
                    Console.Write("What should I shout? ");
                    string input = Console.ReadLine();
                    Console.WriteLine(input.ToUpper() + "!!!");
                }
                else if (option == "2")
                {
                    Console.Write("What should I reverse? ");
                    string input = Console.ReadLine();
                    Console.WriteLine(new string(input.Reverse().ToArray()));
                }
                else if (option == "3")
                {
                    Console.WriteLine("Goodbye");
                    break; // break out of the while loop
                }
                else
                {
                    Console.WriteLine($"Invalid option: {option}");
                }*/
            }
        }
    }
}
