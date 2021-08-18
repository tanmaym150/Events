using System;
using System.Collections.Generic;
namespace ConsoleApp1
{
	public delegate bool GetStudentResultDelegate(Student student);
	public delegate bool GetTeacherResultDelegate(Teacher teacher);

	public class DelegatesTraining
	{
		public static void Main()
		{
			/*
			 *  What is a delegate ?? -> It is a type safe function pointer >> What is function pointer??
			 *  Types of delegate -> UniCast & MultiCast
			 *  Where a delegate is declared??
			 *  Delegate is similar to a Class >> You can create a instance of an delegate
			 *  Demo 1 
			 */
			List<Student> students = new List<Student>();
			Student student2 = new Student() { ID = 1, Name = "Vinayak", Standard = "12", MarksSub1 = 20, MarksSub2 = 40, MarksSub3 = 55, MarksSub4 = 40 };
			Student student3 = new Student() { ID = 1, Name = "Atharva", Standard = "12", MarksSub1 = 45, MarksSub2 = 10, MarksSub3 = 85, MarksSub4 = 42 };
			Student student4 = new Student() { ID = 1, Name = "Veena", Standard = "12", MarksSub1 = 48, MarksSub2 = 65, MarksSub3 = 59, MarksSub4 = 48 };
			Student student5 = new Student() { ID = 1, Name = "Tanmay", Standard = "12", MarksSub1 = 25, MarksSub2 = 55, MarksSub3 = 52, MarksSub4 = 45 };
			Student student6 = new Student() { ID = 1, Name = "Prajakta", Standard = "12", MarksSub1 = 49, MarksSub2 = 60, MarksSub3 = 75, MarksSub4 = 41 };
			students.Add(new Student() { ID = 1, Name = "Ravi", Standard = "12", MarksSub1 = 40, MarksSub2 = 50, MarksSub3 = 55, MarksSub4 = 42 });
			students.Add(student2);
			students.Add(student3);
			students.Add(student4);
			students.Add(student5);
			students.Add(student6);

			List<Teacher> teachers = new List<Teacher>();
			Teacher teacher2 = new Teacher() {Name="Prasad",NoOfLectures = 50, TeachingExperience = 2 };
			Teacher teacher3 = new Teacher() {Name="Aparna",NoOfLectures = 35, TeachingExperience = 2 };
			Teacher teacher4 = new Teacher() { Name="Gopal",NoOfLectures = 54, TeachingExperience = 3 };
			teachers.Add(teacher2);
			teachers.Add(teacher3);
			teachers.Add(teacher4);



			//Use Delegates
			GetStudentResultDelegate getStudentResultDelegate = new GetStudentResultDelegate(GetResult);
			GetTeacherResultDelegate getTeacherResultDelegate = new GetTeacherResultDelegate(GetBonus);
			Student s = new Student();
			s.IsPassed(students, getStudentResultDelegate);
			Teacher t = new Teacher();
			t.IsGetPassed(teachers, getTeacherResultDelegate);
			
		}
		public static bool GetResult(Student student)
		{
			if (student.MarksSub1 >= 40 && student.MarksSub2 >= 40 && student.MarksSub3 >= 40)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public static bool GetBonus(Teacher teacher)
        {
			if(teacher.NoOfLectures >= 50 && teacher.TeachingExperience >= 2)
            {
				return true;
            }
            else
            {
				return false;
            }
        }
	}
	public class Student
	{
		MailService mailService;
		public Student()
		{
			mailService = new MailService();
			mailService.SendMailEvent += ShareResult;
		}
		public int ID { get; set; }
		public string Name { get; set; }
		public string Standard { get; set; }
		public double MarksSub1 { get; set; }
		public double MarksSub2 { get; set; }
		public double MarksSub3 { get; set; }
		public double MarksSub4 { get; set; }

		public void IsPassed(List<Student> students, GetStudentResultDelegate getStudentResult)
		{
			foreach (var stud in students)
			{
				if (getStudentResult(stud))
				{
					Console.WriteLine($"{stud.Name} is Passed");
					mailService.SendEmail(stud.Name);
				}
			}
		}

		public void ShareResult()
		{
			Console.WriteLine($"Sharing Results with student");
		}
	}

	public class Teacher
    {
		MailService mailService;
		public Teacher()
        {
			mailService = new MailService();
			mailService.SendMailEvent += ShareBonus;
		}
		public string Name { get; set; }
		public int NoOfLectures { get; set; }
		public int TeachingExperience { get; set; }

		public void IsGetPassed(List<Teacher> teachers, GetTeacherResultDelegate getTeacherResult)
		{
			foreach (var tea in teachers)
			{
				if (getTeacherResult(tea))
				{
					Console.WriteLine($"{tea.Name} got the BONUS..");
					mailService.SendEmail(tea.Name);
				}
			}
		}
		public void ShareBonus()
		{
			Console.WriteLine($"Congratulations..");
		}
	}
	public class MailService
	{
		public delegate void SendMailDelegate();
		public event SendMailDelegate SendMailEvent;
		public void SendEmail(string name)
		{
			if (SendMailEvent != null)
			{
				// Raise the event by using () operator
				SendMailEvent();
			
			}
		}
	}
}
