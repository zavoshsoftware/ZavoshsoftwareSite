using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Models
{
    public class Comment:BaseEntity
    {
     
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Name { get; set; }

        [Display(Name = "ایمیل")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Email { get; set; }

        [Display(Name = "متن نظر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        //[MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        [Column(TypeName = "ntext")]
        public string Body { get; set; }

    
        [Display(Name = "فعال؟")]
        public bool IsActive { get; set; }
     


        public Guid PageId { get; set; }
        public virtual Page Page { get; set; }
        internal class Configuration : EntityTypeConfiguration<Comment>
        {
            public Configuration()
            { 

                HasRequired(p => p.Page)
                    .WithMany(t => t.Comments)
                    .HasForeignKey(p => p.PageId);
            }
        }

        public string Response { get; set; }
        public DateTime? ResponseDate { get; set; }
    }
}