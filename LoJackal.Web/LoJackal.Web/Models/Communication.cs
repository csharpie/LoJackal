namespace LoJackal.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Communication")]
    public partial class Communication
    {
        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        public string ComputerName { get; set; }

        [Required]
        [StringLength(27)]
        public string IPAddress { get; set; }

        public DateTime LastCommunicated { get; set; }
    }
}
