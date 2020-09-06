using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class Position : BaseEntity
    {
        public Position()
        {
            PagePositions = new List<PagePosition>();

        }
        [Display(Name = "موقعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Title { get; set; }

        public virtual ICollection<PagePosition> PagePositions { get; set; }
    }
}