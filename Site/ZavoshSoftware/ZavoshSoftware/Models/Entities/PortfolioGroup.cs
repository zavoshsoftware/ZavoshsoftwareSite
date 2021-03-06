﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Models
{
    public class PortfolioGroup : BaseEntity
    {
        public PortfolioGroup()
        {
            PortfolioGroups = new List<PortfolioGroup>();
            Portfolios = new List<Portfolio>();
        }

        [Display(Name = "عنوان گروه نمونه کار")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Title { get; set; }

        [Display(Name = "پارامتر آدرس صفحه")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string UrlParameter { get; set; }

        [Display(Name = "خلاصه مطلب صفحه")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        [DataType(DataType.MultilineText)]
        public string Summery { get; set; }

        [Display(Name = "متن صفحه")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        [DataType(DataType.Html)]
        [AllowHtml]
        public string Body { get; set; }

        [Display(Name = "تصویر")]
        [MaxLength(500)]
        public string ImageUrl { get; set; }


        [Display(Name = "یادداشت")]
        [DataType(DataType.MultilineText)]
        public string Notation { get; set; }

        [Display(Name = "توضیحات متا")]
        [DataType(DataType.MultilineText)]
        [MaxLength(350, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string MetaDescription { get; set; }

        [Display(Name = "فعال؟")]
        public bool IsActive { get; set; }

        [Display(Name = "اولویت")]
        public int Order { get; set; }

        [Display(Name = "صفحه پدر")]
        public Guid? ParentId { get; set; }







        public virtual ICollection<Portfolio> Portfolios { get; set; }
        public virtual PortfolioGroup Parent { get; set; }
        public virtual ICollection<PortfolioGroup> PortfolioGroups { get; set; }

        internal class Configuration : EntityTypeConfiguration<PortfolioGroup>
        {
            public Configuration()
            {
                HasOptional(p => p.Parent)
                    .WithMany(t => t.PortfolioGroups)
                    .HasForeignKey(p => p.ParentId);
            }
        }
    }
}