using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace encuesta.Models
{
    public class NewID
    {
        [Key]
        public string C_Token { set; get; }
    }
}