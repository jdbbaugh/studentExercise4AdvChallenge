using System;
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
            bool sweetApp = true;
            while (sweetApp)
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
                Console.WriteLine("9. Display students by Cohort.");
                Console.WriteLine("10. Move existing student to new cohort.");
                Console.WriteLine("11. List specific students exercises.");
                Console.WriteLine("12. Assign specific student specific exercise.");
                Console.WriteLine("13. Exit.");
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
                    case "10":
                        Console.WriteLine();
                        List<Student> chooseAStudent = repository.GetAllStudentsBasic();
                        Console.WriteLine($"Select student by entering 1 - {chooseAStudent.Count} ");
                        int chooseAStudentCounter = 0;
                        foreach (Student student in chooseAStudent)
                        {
                            ++chooseAStudentCounter;
                            Console.WriteLine($"{chooseAStudentCounter}: {student.FirstName} {student.LastName}");
                        }
                        int theeChosenStudent = Int32.Parse(Console.ReadLine());

                        List<Cohort> chooseANewCohort = repository.GetallCohorts();
                        Console.WriteLine($"Select their new Cohort by entering 1 - {chooseANewCohort.Count} ---------------------");
                        int choosingNewCohortOptionCounter = 0;
                        foreach (Cohort cohort in chooseANewCohort)
                        {
                            ++choosingNewCohortOptionCounter;
                            Console.WriteLine($"{choosingNewCohortOptionCounter}: {cohort.Name}");
                        }
                        int theeChosenNewCohort = Int32.Parse(Console.ReadLine());
                        repository.ChangeStudentCohort(theeChosenStudent, theeChosenNewCohort);
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine($"{chooseAStudent[--theeChosenStudent].FirstName} has been moved to {chooseANewCohort[--theeChosenNewCohort].Name}");
                        break;
                    case "11":
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Choose a Student to see their exercises");
                        List<Student> chooseAStudentForEX = repository.GetAllStudentsBasic();
                        Console.WriteLine($"Select student by entering 1 - {chooseAStudentForEX.Count} ");
                        int chooseAStudentForExCounter = 0;
                        foreach (Student student in chooseAStudentForEX)
                        {
                            ++chooseAStudentForExCounter;
                            Console.WriteLine($"{chooseAStudentForExCounter}: {student.FirstName} {student.LastName}");
                        }
                        int theeChosenStudentForEx = Int32.Parse(Console.ReadLine());

                       List<Exercise> chosenStudentsExercises = repository.QueStudentExercises(theeChosenStudentForEx);
                        int chosenStuExCounter = 0;
                        Console.WriteLine($"{chooseAStudentForEX[--theeChosenStudentForEx].FirstName} is currently working on:");
                        foreach (Exercise ex in chosenStudentsExercises)
                        {
                            ++chosenStuExCounter;
                            Console.WriteLine($"{chosenStuExCounter}: {ex.Name} ");
                        }

                        Console.WriteLine();
                        Console.WriteLine();
                        break;
                    case "12":
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Choose the student you'd like to assign an exercise to");
                        List<Student> chooseAStudentForAssigningEX = repository.GetAllStudentsBasic();
                        Console.WriteLine($"Select student by entering 1 - {chooseAStudentForAssigningEX.Count} ");

                        int chooseAStudentForAssigningExCounter = 0;
                        foreach (Student student in chooseAStudentForAssigningEX)
                        {
                            ++chooseAStudentForAssigningExCounter;
                            Console.WriteLine($"{chooseAStudentForAssigningExCounter}: {student.FirstName} {student.LastName}");
                        }
                        int theeChosenStudentForAssigningEx = Int32.Parse(Console.ReadLine());

                        List<Exercise> chosenStudentsAlreadyAssignedExercises = repository.QueStudentExercises(theeChosenStudentForAssigningEx);
                        List<Exercise> allPossibleExercises = repository.GetAllExercises();
                        List<Exercise> unassignedExercises = new List<Exercise>();



                        Console.WriteLine($"You've chosen {chooseAStudentForAssigningEX[--theeChosenStudentForAssigningEx].FirstName} {chooseAStudentForAssigningEX[theeChosenStudentForAssigningEx].LastName}");
                        Console.WriteLine("Who's already been assigned:");
                        foreach (Exercise ex in chosenStudentsAlreadyAssignedExercises)
                        {
                            Console.WriteLine(ex.Name);
                        }

                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("But not:");
                        List<int> list1 = new List<int>();
                        List<int> list2 = new List<int>();

                        foreach (Exercise exNum in allPossibleExercises)
                        {
                            list1.Add(exNum.Id);
                        }

                        foreach (Exercise exIn in chosenStudentsAlreadyAssignedExercises)
                        {
                            list2.Add(exIn.Id);
                        }

                        List<int> list3 = list1.Except(list2).ToList();

                        Console.WriteLine($"{chooseAStudentForAssigningEX[theeChosenStudentForAssigningEx].FirstName} {chooseAStudentForAssigningEX[theeChosenStudentForAssigningEx].Id}");
                        Console.WriteLine("Input Exercise Id to assign it to student");
                        foreach (Exercise num in allPossibleExercises)
                        {
                            if (!chosenStudentsAlreadyAssignedExercises.Exists(x => x.Id == num.Id))
                            {
                                Console.WriteLine($"{num.Id}: {num.Name}");
                            }
                            
                        }

                       int flipinEx = Int32.Parse(Console.ReadLine());

                        repository.FinallyAssignEx(flipinEx, chooseAStudentForAssigningEX[theeChosenStudentForAssigningEx].Id);

                        break;
                    case "13":
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Goodbye");
                        sweetApp = false;
                        Console.WriteLine();
                        Console.WriteLine();
                        break;
                    default:
                        Console.WriteLine("Goodbye");
                        sweetApp = false;
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
