using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerServiceWebAPI.Models
{
    public class DataFile
    {
        [Key]
        public int ID { get; set; }
        public byte[] Filerecord { get; set; }
        public string Filetype { get; set; }
        public string Name { get; set; }
    }
}