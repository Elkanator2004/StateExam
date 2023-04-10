using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Classes
{
    public class DocumentThreeDays : Document
    {
        [Display(Name = "Receiver")]
        [ForeignKey("Receiver")]
        public int TeacherID { get; set; }
        [Range(1, 3)]
        public int Days { get; set; }

        public Teacher Receiver { get; set; }
        public DocumentThreeDays()
        {

        }
    }
}
