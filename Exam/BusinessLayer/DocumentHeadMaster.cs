using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLayer
{
    public class DocumentHeadMaster
    {
        [Key]
        [MaxLength(20)]
        public string EntryNumber { get; set; }

        [Required]
        [MaxLength(20)]
        public string Title { get; set; }

        [Display(Name = "Sender")]
        [ForeignKey("Sender")]
        public string SenderId { get; set; }

        [Display(Name = "Receiver")]
        [ForeignKey("Receiver")]
        public string ReceiverId { get; set; }

        public string Grade { get; set; }

        [Range(1, 3)]
        public int Days { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public DateTime DateSigned { get; set; }

        public DateTime DateReceived { get; set; }

        public User Sender { get; set; }

        public User Receiver { get; set; }

        public DocumentHeadMaster()
        {

        }

        public DocumentHeadMaster(string entrynumber, string title, string senderId, string grade, int days, DateTime datefrom, DateTime dateto, DateTime datesigned, DateTime datereceived)
        {
            this.EntryNumber = entrynumber;
            this.Title = title;
            this.SenderId = senderId;
            this.Grade = grade;
            this.Days = days;
            this.DateFrom = datefrom;
            this.DateTo = dateto;
            this.DateSigned = datesigned;
            this.DateReceived = datereceived;
            this.Receiver = null;
        }

        public DocumentHeadMaster(string entrynumber, string title, string senderId, string grade, int days, DateTime datefrom, DateTime dateto, DateTime datesigned, DateTime datereceived, string receiverId) :
            this(entrynumber, title, senderId, grade, days, datefrom, dateto, datesigned, datereceived)
        {
            this.ReceiverId = receiverId;
        }
    }
}
