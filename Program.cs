using System.Text.Json;

namespace StudentRegisterWithJSON
{

    public class Student
    {
        public string Name { get; set; }
        public int Grade { get; set; }

        public Student(string name, int grade)
        {
            Name = name;
            Grade = grade;
        }
    }


    public class Register
    {
        List<Student> students = new List<Student>();

        public void AddStudent()
        {
            Console.Write("Please enter a name for your student: ");
            string name = Console.ReadLine();

            Console.Write("Please enter their grade: ");
            string gradeInput = Console.ReadLine();

            try
            {
                int grade = Convert.ToInt32(gradeInput);
                if (grade < 1 || grade > 6)
                {
                    Console.WriteLine("Please write a whole number 1-6.");
                    Console.WriteLine("Press any key return.");
                    Console.Read();
                    return;
                }
                students.Add(new Student(name, grade));
                var json = JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true});
                File.WriteAllText("students.txt", json);
                Console.WriteLine("Student added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void DisplayAllStudentsFromGrade(int grade)
        {
            Console.WriteLine("GRADE " + grade.ToString() + ":");
            List<Student> ofGrade = students.Where(student => student.Grade == grade).ToList();
            foreach (Student student in ofGrade)
            {
  
                    Console.WriteLine(student.Name);
 
            }
        }
        internal class Program
        {
            static void Main(string[] args)
            {

                Register School = new Register();
                var json = File.ReadAllText("students.txt");
                School.students = JsonSerializer.Deserialize<List<Student>>(json);

                bool isRunning = true;

                while (isRunning)
                {
                    Console.Clear();
                    Console.WriteLine("1) Search student from grade");
                    Console.WriteLine("2) Add a new student");
                    Console.WriteLine("3) To Exit");
                    string answerInput = Console.ReadLine();
                    try
                    {
                        int answer = Convert.ToInt32(answerInput);

                        switch (answer)
                        {
                            case 1:
                                Console.WriteLine("What grade are you looking for? 1-6, please.");
                                int grade;
                                try
                                {
      
                                        grade = Convert.ToInt32(Console.ReadLine());
                                        if (grade < 1 || grade > 6)
                                        {
                                            Console.WriteLine("Please write a whole number 1-6.");
                                            Console.WriteLine("Press any key return.");
                                            Console.Read();
                                        break;
                                        }
                                      
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Wrong format: Please enter a whole number. It must be 1-6");
                                    Console.WriteLine("Press any key return.");
                                    Console.Read();
                                    break;
                                }
                                School.DisplayAllStudentsFromGrade(grade);
                                Console.WriteLine("Press any key return.");
                                Console.Read();
                                break;
                            case 2:
                                School.AddStudent();
                                Console.WriteLine("Press any key return.");
                                Console.Read();
                                break;
                            case 3:
                                isRunning = false;
                                break;
                            default:
                                Console.WriteLine("Please choose from 1-3 above");
                                Console.WriteLine("Press any key return.");
                                Console.Read();
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Press any key return.");
                        Console.ReadLine();
                    }

                }
                Console.WriteLine("Thank you for your time!");
                Console.WriteLine("Press any key to close application.");
                Console.Read();
            }
        }
    }
}
