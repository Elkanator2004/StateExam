using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusinessLayer
{
    public abstract class Document
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

        public DateTime DateSigned { get; set; }

        public DateTime DateReceived { get; set; }

        public User Sender { get; set; }

        public User Receiver { get; set; }

        public DocumentType Type { get; set; }

        public Document()
        {

        }

        public Document(string entrynumber, string title, string senderId, DateTime datesigned, DateTime datereceived)
        {
            this.EntryNumber = entrynumber;
            this.Title = title;
            this.SenderId = senderId;
            this.DateSigned = datesigned;
            this.DateReceived = datereceived;
            this.Receiver = null;
        }

        public Document(string entrynumber, string title, string senderId, DateTime datesigned, DateTime datereceived, string receiverId) :
            this(entrynumber, title, senderId, datesigned, datereceived)
        {
            this.ReceiverId = receiverId;
        }
    }
}
