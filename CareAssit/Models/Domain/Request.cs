﻿using System.ComponentModel.DataAnnotations;

namespace CareAssit.Models.Domain
{
    public class Request
    {
        [Key]
        public Guid Request_Id { get; set; }
        public Guid? InsurancePlan_Id { get; set; }
        public Guid? User_Id { get; set; }
        public Guid? Health_Id { get; set; }
        public Status1 status1 { get; set; } = Status1.notsubmitted;

        public string DocUrl { get; set; } = string.Empty;
        public enum Status1
        {
            Submitted,
            notsubmitted
        }

    }

}
