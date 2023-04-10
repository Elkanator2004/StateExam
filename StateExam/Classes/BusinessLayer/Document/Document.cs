using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Document
    {
        [Key]
        [StringLength(20)]
        public string EntryNumber { get; set; }

        public string Title { get; set; }

        [MaxLength(3), MinLength(2)]
        public string Grade { get; set; }

        [Display(Name = "Sender")]
        [ForeignKey("Sender")]
        public int SenderID { get; set; }

        public string Adress { get; set; }
        [StringLength(10)]
        public string Telephone { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime DateSigned { get; set; }
        public DateTime DateReceived { get; set; }

        public Student Sender { get; set; }

        public string Status { get; set; }

        public Document()
        {

        }
    }
}
