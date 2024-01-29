﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication_Week2.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }




    }
}
