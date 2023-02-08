using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WBSBE.DAL.Migrations
{
    /// <inheritdoc />
    public partial class initver2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mAduan",
                columns: table => new
                {
                    txtNomorID = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    txtStatus = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    txtPelapor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    txtNIK = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    txtNama = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    txtTlp = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    txtEmail = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    bitSentMail = table.Column<bool>(type: "boolean", nullable: false),
                    bitActive = table.Column<bool>(type: "boolean", nullable: false),
                    dtmInserted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    dtmUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    txtInsertedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    txtUpdatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mAduan", x => x.txtNomorID);
                });

            migrationBuilder.CreateTable(
                name: "mConfig",
                columns: table => new
                {
                    intConfig = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    txtName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    txtType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    txtValue = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    bitActive = table.Column<bool>(type: "boolean", nullable: false),
                    txtInsertedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    dtmInserted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    txtUpdatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    dtmUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mConfig", x => x.intConfig);
                });

            migrationBuilder.CreateTable(
                name: "TestDB",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'100', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Test = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TestDB_pkey", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "mAttachment",
                columns: table => new
                {
                    intAttachmentID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    txtType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    txtFileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    txtEncryptedName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    txtFileSize = table.Column<decimal>(type: "numeric", nullable: true),
                    txtFilePath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    bitActive = table.Column<bool>(type: "boolean", nullable: false),
                    txtInsertedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    dtmInserted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    txtUpdatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    dtmUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    mAduantxtNomorID = table.Column<string>(type: "character varying(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mAttachment", x => x.intAttachmentID);
                    table.ForeignKey(
                        name: "FK_mAttachment_mAduan_mAduantxtNomorID",
                        column: x => x.mAduantxtNomorID,
                        principalTable: "mAduan",
                        principalColumn: "txtNomorID");
                });

            migrationBuilder.CreateTable(
                name: "mJawabPertanyaan",
                columns: table => new
                {
                    intJwbPertanyaanID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    txtPertanyaan1 = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    txtPertanyaan2 = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    txtPertanyaan3 = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    txtPertanyaan4 = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    txtInsertedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    dtmInserted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    txtUpdatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    dtmUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    bitActive = table.Column<bool>(type: "boolean", nullable: false),
                    txtNomorAduantxtNomorID = table.Column<string>(type: "character varying(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mJawabPertanyaan", x => x.intJwbPertanyaanID);
                    table.ForeignKey(
                        name: "FK_mJawabPertanyaan_mAduan_txtNomorAduantxtNomorID",
                        column: x => x.txtNomorAduantxtNomorID,
                        principalTable: "mAduan",
                        principalColumn: "txtNomorID");
                });

            migrationBuilder.CreateTable(
                name: "TestDB2",
                columns: table => new
                {
                    ID2 = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'100', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    ID1 = table.Column<int>(type: "integer", nullable: false),
                    Nama2 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Tanggal = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    checklist = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TestDB2_pkey", x => x.ID2);
                    table.ForeignKey(
                        name: "TestDB2_ID1_fkey",
                        column: x => x.ID1,
                        principalTable: "TestDB",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_mAttachment_mAduantxtNomorID",
                table: "mAttachment",
                column: "mAduantxtNomorID");

            migrationBuilder.CreateIndex(
                name: "IX_mJawabPertanyaan_txtNomorAduantxtNomorID",
                table: "mJawabPertanyaan",
                column: "txtNomorAduantxtNomorID");

            migrationBuilder.CreateIndex(
                name: "IX_TestDB2_ID1",
                table: "TestDB2",
                column: "ID1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mAttachment");

            migrationBuilder.DropTable(
                name: "mConfig");

            migrationBuilder.DropTable(
                name: "mJawabPertanyaan");

            migrationBuilder.DropTable(
                name: "TestDB2");

            migrationBuilder.DropTable(
                name: "mAduan");

            migrationBuilder.DropTable(
                name: "TestDB");
        }
    }
}
