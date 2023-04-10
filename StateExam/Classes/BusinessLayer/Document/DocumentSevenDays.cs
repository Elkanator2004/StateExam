using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Classes
{
    public class DocumentSevenDays : Document
    {
        [Display(Name = "Receiver")]
        [ForeignKey("Receiver")]
        public int HeadMasterID { get; set; }

        public HeadMaster Receiver { get; set; }
        public DocumentSevenDays()
        {

        }
    }
}
