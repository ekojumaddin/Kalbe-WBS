using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBSBE.Common.Entity.WBS;

namespace WBSBE.DAL.ModelBuilders.WBS
{
    public class ClsTestDb2Builder
    {
        public static void Builder(ref ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestDb2>(entity =>
            {
                entity.HasKey(e => e.Id2).HasName("TestDB2_pkey");

                entity.ToTable("TestDB2");

                entity.Property(e => e.Id2)
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(100L, null, null, null, null, null)
                    .HasColumnName("ID2");
                entity.Property(e => e.Checklist).HasColumnName("checklist");
                entity.Property(e => e.Id1).HasColumnName("ID1");
                entity.Property(e => e.Nama2).HasMaxLength(100);
                entity.Property(e => e.Tanggal).HasColumnType("timestamp without time zone");

                entity.HasOne(d => d.Id1Navigation).WithMany(p => p.TestDb2s)
                    .HasForeignKey(d => d.Id1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TestDB2_ID1_fkey");
            });
        }
    }
}
