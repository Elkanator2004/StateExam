using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLayer
{
    public class ThreeDays : Document
    {
        public string Grade { get; set; }
        
        [Range(1,3)]
        public int Days { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public ThreeDays() : base()
        {

        }

        public ThreeDays(string entrynumber,string title, string senderId, string grade, int days, DateTime datefrom, DateTime dateto, DateTime datesigned, DateTime datereceived)
            :base(entrynumber, title, senderId, datesigned, datereceived)
        {
            Grade = grade;
            Days = days;
            DateFrom = datefrom;
            DateTo = dateto;
        }

        public ThreeDays(string entrynumber, string title, string senderId, string grade, int days, DateTime datefrom, DateTime dateto, DateTime datesigned, DateTime datereceived, string receiverId)
            :base(entrynumber, title, senderId, datesigned, datereceived)
        {
            ReceiverId = receiverId;
            Grade = grade;
            Days = days;
            DateFrom = datefrom;
            DateTo = dateto;
        }
    }
}
