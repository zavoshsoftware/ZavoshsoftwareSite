using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class BaseEntity:object
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
        public bool IsDelete { get; set; }
        [Display(Name ="تاریخ ثبت")]
        public DateTime CreationDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        [Display(Name ="تاریخ آخرین ویرایش")]
        public DateTime? LastModificationDate { get; set; }
    }
}