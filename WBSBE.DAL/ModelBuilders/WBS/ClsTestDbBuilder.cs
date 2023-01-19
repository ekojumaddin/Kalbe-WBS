using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBSBE.Common.Entity.WBS;

namespace WBSBE.DAL.ModelBuilders.WBS
{
    public class ClsTestDbBuilder
    {
        public static void Builder(ref ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestDb>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("TestDB_pkey");

                entity.ToTable("TestDB");

                entity.Property(e => e.Id)
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(100L, null, null, null, null, null)
                    .HasColumnName("ID");
                entity.Property(e => e.Name).HasMaxLength(100);
                
                entity.Property(e => e.Test).HasMaxLength(200);
            });
        }
    }
}
