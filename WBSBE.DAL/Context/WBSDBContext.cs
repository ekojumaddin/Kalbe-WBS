﻿using KN2021_GlobalClient_NetCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBSBE.Common.Entity.WBS;
using WBSBE.Common.Library;
using WBSBE.DAL.ModelBuilders.WBS;

namespace WBSBE.DAL.Context
{
    public partial class WBSDBContext : DbContext
    {
        public WBSDBContext()
        {
        }

        public WBSDBContext(DbContextOptions<WBSDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TestDb> TestDbs { get; set; }
        public virtual DbSet<TestDb2> TestDb2s { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(GetConnString());
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
            }
        }

        private string GetConnString()
        {
            string conn = AppServicesHelper.getConnetionString.WBSDBConnection;
            try
            {
                return ClsRijndael.Decrypt(conn);
            }
            catch
            {
                return conn;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ClsTestDbBuilder.Builder(ref modelBuilder);
            ClsTestDb2Builder.Builder(ref modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
