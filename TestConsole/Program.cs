// ***********************************************************************
// Solution         : MyselfTools
// Project          : TestConsole
// File             : Program.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2017 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<Student> students = new List<Student>
            {
                new Student("1"),
                new Student("2"),
                new Student("21"),
                new Student("11"),
                new Student("1A"),
                new Student("3"),
                new Student("12"),
                new Student("21A"),
                new Student("A1"),
                new Student("B1"),
                new Student("C")
            };

            List<Student> r1 = students.OrderBy(t => t.Floor).ToList();
            foreach (Student student in r1)
            {
                Console.WriteLine(student.Floor);
            }

            Console.WriteLine();
            Console.WriteLine();

            List<Student> r2 = students.OrderBy(t => t.Floor, new FloorComparer()).ToList();
            foreach (Student student in r2)
            {
                Console.WriteLine(student.Floor);
            }

            Console.ReadLine();
        }
    }

    public class Student
    {
        public Student(string floor)
        {
            Floor = floor;
        }

        public string Floor { get; set; }
    }

    public class FloorComparer : IComparer<string>
    {
        private List<string> Value1 = new List<string>();
        private List<string> Value2 = new List<string>();

        public int Compare(string x, string y)
        {
            if (int.TryParse(x, out int currentFloor) && int.TryParse(y, out int otherFloor))
            {
                return currentFloor - otherFloor;
            }
           
            return string.CompareOrdinal(x, y);
        }

        
    }
}