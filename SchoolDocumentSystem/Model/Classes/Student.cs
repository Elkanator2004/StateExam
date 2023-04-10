using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Student : User
    {
        public string Grade { get; set; }
        public int DaysTeacher { get; set; }
        public int DaysHeadMaster { get; set; }

        public Student()
        {

        }
    }
}
