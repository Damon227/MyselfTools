// ***********************************************************************
// Solution         : MyselfTools
// Project          : ConsoleApp1
// File             : Teacher.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************


namespace ConsoleApp1
{
    public class Student
    {

    }

    public class Teacher
    {
        
    }

    public class GoodTeacher : Teacher
    {

    }

    public class OldTeacher<T> : Teacher
        where T : class, new()
    {

    }

    public static class TeacherTest
    {
        public static void Test()
        {
            GoodTeacher s1 = new GoodTeacher();
            Teacher s11 = s1 as Teacher;

            OldTeacher<Student> s2 = new OldTeacher<Student>();
            Teacher s22 = s2 as Teacher;

        }
    }
}