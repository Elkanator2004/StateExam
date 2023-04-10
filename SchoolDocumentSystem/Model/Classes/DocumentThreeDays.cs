using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class DocumentThreeDays : Document
    {
        public string Teacher { get; set; }
        [Range(1, 3)]
        public int Days { get; set; }
        public DocumentThreeDays()
        {

        }
    }
}
