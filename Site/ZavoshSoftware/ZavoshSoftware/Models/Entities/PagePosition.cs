using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Models
{
    public class PagePosition:BaseEntity
    {
        public Guid PageId { get; set; }
        public Guid PositionId { get; set; }
        public Page Page { get; set; }
        public Position Position { get; set; }

        internal class Configuration : EntityTypeConfiguration<PagePosition>
        {
            public Configuration()
            {
                HasRequired(p => p.Page)
                    .WithMany(t => t.PagePositions)
                    .HasForeignKey(p => p.PageId);

                HasRequired(p => p.Position)
                    .WithMany(t => t.PagePositions)
                    .HasForeignKey(p => p.PositionId);
            }
        }
    }
}