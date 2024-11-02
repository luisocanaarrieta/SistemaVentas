using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class Users
    {
        public int ID { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string USER_NAME { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string USER_PASSWORD { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime LOG_DATE_CREATE { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? LOG_DATE_UPDATE { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string LOG_USER_CREATE { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? LOG_USER_UPDATE { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ENDS { get; set; }

    }
}
