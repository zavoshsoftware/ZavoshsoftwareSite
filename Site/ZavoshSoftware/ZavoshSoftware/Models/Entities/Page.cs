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
    public class Page : BaseEntity
    {
        public Page()
        {
            Comments = new List<Comment>();
            PagePositions = new List<PagePosition>();
            Faqs = new List<Faq>();
        }
        [Display(Name = "عنوان صفحه")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
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

        [Display(Name = "گروه صفحه")]
        public Guid? PageGroupId { get; set; }

        [Display(Name = "یادداشت")]
        [DataType(DataType.MultilineText)]
        public string Notation { get; set; }

        [Display(Name = "توضیحات متا")]
        [DataType(DataType.MultilineText)]
        [MaxLength(350, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string MetaDescription { get; set; }

        [Display(Name = "فعال؟")]
        public bool IsActive { get; set; }

        [Display(Name = "تگ Title")]
        [MaxLength(350, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string TitleTag { get; set; }

        [Display(Name = "اولویت")]
        public int Order { get; set; }
        [Display(Name = "امتیاز")]
        public decimal? AverageRate { get; set; }

        public bool HasFaq { get; set; }
        public string FaqTitle { get; set; }
        public virtual PageGroup PageGroup { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PagePosition> PagePositions { get; set; }
        public virtual ICollection<Faq> Faqs { get; set; }

        internal class Configuration : EntityTypeConfiguration<Page>
        {
            public Configuration()
            {
                HasOptional(p => p.PageGroup)
                    .WithMany(t => t.Pages)
                    .HasForeignKey(p => p.PageGroupId);

            }
        }
    }
}