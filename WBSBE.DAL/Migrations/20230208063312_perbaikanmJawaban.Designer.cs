﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WBSBE.DAL.Context;

#nullable disable

namespace WBSBE.DAL.Migrations
{
    [DbContext(typeof(WBSDBContext))]
    [Migration("20230208063312_perbaikanmJawaban")]
    partial class perbaikanmJawaban
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WBSBE.Common.Entity.WBS.TestDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));
                    NpgsqlPropertyBuilderExtensions.HasIdentityOptions(b.Property<int>("Id"), 100L, null, null, null, null, null);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Test")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id")
                        .HasName("TestDB_pkey");

                    b.ToTable("TestDB", (string)null);
                });

            modelBuilder.Entity("WBSBE.Common.Entity.WBS.TestDb2", b =>
                {
                    b.Property<int>("Id2")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ID2");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id2"));
                    NpgsqlPropertyBuilderExtensions.HasIdentityOptions(b.Property<int>("Id2"), 100L, null, null, null, null, null);

                    b.Property<bool?>("Checklist")
                        .HasColumnType("boolean")
                        .HasColumnName("checklist");

                    b.Property<int>("Id1")
                        .HasColumnType("integer")
                        .HasColumnName("ID1");

                    b.Property<string>("Nama2")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("Tanggal")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id2")
                        .HasName("TestDB2_pkey");

                    b.HasIndex("Id1");

                    b.ToTable("TestDB2", (string)null);
                });

            modelBuilder.Entity("WBSBE.Common.Entity.WBS.mAduan", b =>
                {
                    b.Property<string>("txtNomorID")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("bitActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("bitSentMail")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("dtmInserted")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("dtmUpdated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("txtEmail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("txtInsertedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("txtNIK")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("txtNama")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("txtPelapor")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("txtStatus")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.Property<string>("txtTlp")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("txtUpdatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("txtNomorID");

                    b.ToTable("mAduan");
                });

            modelBuilder.Entity("WBSBE.Common.Entity.WBS.mAttachment", b =>
                {
                    b.Property<int>("intAttachmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("intAttachmentID"));

                    b.Property<bool>("bitActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("dtmInserted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("dtmUpdated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("mAduantxtNomorID")
                        .HasColumnType("character varying(50)");

                    b.Property<string>("txtEncryptedName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("txtFileName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("txtFilePath")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<decimal?>("txtFileSize")
                        .HasColumnType("numeric");

                    b.Property<string>("txtInsertedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("txtType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("txtUpdatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("intAttachmentID");

                    b.HasIndex("mAduantxtNomorID");

                    b.ToTable("mAttachment");
                });

            modelBuilder.Entity("WBSBE.Common.Entity.WBS.mConfig", b =>
                {
                    b.Property<int>("intConfig")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("intConfig"));

                    b.Property<bool>("bitActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("dtmInserted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("dtmUpdated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("txtInsertedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("txtName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("txtType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("txtUpdatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("txtValue")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("intConfig");

                    b.ToTable("mConfig");
                });

            modelBuilder.Entity("WBSBE.Common.Entity.WBS.mJawabPertanyaan", b =>
                {
                    b.Property<int>("intJwbPertanyaanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("intJwbPertanyaanID"));

                    b.Property<bool>("bitActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("dtmInserted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("dtmUpdated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("txtInsertedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("txtNomorAduantxtNomorID")
                        .HasColumnType("character varying(50)");

                    b.Property<string>("txtPertanyaan1")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("txtPertanyaan2")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("txtPertanyaan3")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("txtPertanyaan4")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("txtUpdatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("intJwbPertanyaanID");

                    b.HasIndex("txtNomorAduantxtNomorID");

                    b.ToTable("mJawabPertanyaan");
                });

            modelBuilder.Entity("WBSBE.Common.Entity.WBS.mJawaban", b =>
                {
                    b.Property<int>("intJawabanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("intJawabanID"));

                    b.Property<bool>("bitActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("dtmInserted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("dtmUpdated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("intOrderJawaban")
                        .HasColumnType("integer");

                    b.Property<int>("intPertanyaanID")
                        .HasColumnType("integer");

                    b.Property<string>("txtInsertedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("txtJawaban")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("txtNomorAduantxtNomorID")
                        .HasColumnType("character varying(50)");

                    b.Property<string>("txtUpdatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("intJawabanID");

                    b.HasIndex("txtNomorAduantxtNomorID");

                    b.ToTable("mJawaban");
                });

            modelBuilder.Entity("WBSBE.Common.Entity.WBS.mLookup", b =>
                {
                    b.Property<int>("intLookupID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("intLookupID"));

                    b.Property<bool>("bitActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("dtmInserted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("dtmUpdated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("intOrderNo")
                        .HasColumnType("integer");

                    b.Property<int?>("intValue")
                        .HasColumnType("integer");

                    b.Property<string>("txtInsertedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("txtName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("txtType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("txtUpdatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("intLookupID");

                    b.ToTable("mLookup");
                });

            modelBuilder.Entity("WBSBE.Common.Entity.WBS.mPertanyaan", b =>
                {
                    b.Property<int>("intPertanyaanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("intPertanyaanID"));

                    b.Property<bool>("bitActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("bitMandatory")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("dtmInserted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("dtmUpdated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("intOrderPertanyaan")
                        .HasColumnType("integer");

                    b.Property<string>("txtInsertedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("txtPertanyaan")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("txtUpdatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("intPertanyaanID");

                    b.ToTable("mPertanyaan");
                });

            modelBuilder.Entity("WBSBE.Common.Entity.WBS.TestDb2", b =>
                {
                    b.HasOne("WBSBE.Common.Entity.WBS.TestDb", "Id1Navigation")
                        .WithMany("TestDb2s")
                        .HasForeignKey("Id1")
                        .IsRequired()
                        .HasConstraintName("TestDB2_ID1_fkey");

                    b.Navigation("Id1Navigation");
                });

            modelBuilder.Entity("WBSBE.Common.Entity.WBS.mAttachment", b =>
                {
                    b.HasOne("WBSBE.Common.Entity.WBS.mAduan", "mAduan")
                        .WithMany("listAttachments")
                        .HasForeignKey("mAduantxtNomorID");

                    b.Navigation("mAduan");
                });

            modelBuilder.Entity("WBSBE.Common.Entity.WBS.mJawabPertanyaan", b =>
                {
                    b.HasOne("WBSBE.Common.Entity.WBS.mAduan", "txtNomorAduan")
                        .WithMany("answerForQuestion")
                        .HasForeignKey("txtNomorAduantxtNomorID");

                    b.Navigation("txtNomorAduan");
                });

            modelBuilder.Entity("WBSBE.Common.Entity.WBS.mJawaban", b =>
                {
                    b.HasOne("WBSBE.Common.Entity.WBS.mAduan", "txtNomorAduan")
                        .WithMany("listJawaban")
                        .HasForeignKey("txtNomorAduantxtNomorID");

                    b.Navigation("txtNomorAduan");
                });

            modelBuilder.Entity("WBSBE.Common.Entity.WBS.TestDb", b =>
                {
                    b.Navigation("TestDb2s");
                });

            modelBuilder.Entity("WBSBE.Common.Entity.WBS.mAduan", b =>
                {
                    b.Navigation("answerForQuestion");

                    b.Navigation("listAttachments");

                    b.Navigation("listJawaban");
                });
#pragma warning restore 612, 618
        }
    }
}
