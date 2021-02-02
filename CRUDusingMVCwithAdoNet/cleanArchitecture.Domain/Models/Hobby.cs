using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace cleanArchitecture.Domain.Models
{
    public class Hobby
    {
        public int HobbyId { get; set; }
        public string HobbyName { get; set; }
    }
}
