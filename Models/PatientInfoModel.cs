using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PatientInfoModel
    {
        public int Patient_ID { get; set; }
        public string Name { get; set; }
        public string HKID { get; set; }
        public string Phone_Number { get; set; }
        public System.DateTime Date_of_Birth { get; set; }
        public string Gender { get; set; }
    }
}