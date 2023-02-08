using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WBSBE.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateDatabaseVer2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            


            migrationBuilder.CreateTable(
                name: "mLookup",
                columns: table => new
                {
                    intLookupID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    txtType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    txtName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    intValue = table.Column<int>(type: "integer", nullable: true),
                    intOrderNo = table.Column<int>(type: "integer", nullable: true),
                    bitActive = table.Column<bool>(type: "boolean", nullable: false),
                    dtmInserted = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    dtmUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    txtInsertedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    txtUpdatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mLookup", x => x.intLookupID);
                });

            migrationBuilder.CreateTable(
                name: "mPertanyaan",
                columns: table => new
                {
                    intPertanyaanID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    txtPertanyaan = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    intOrderPertanyaan = table.Column<int>(type: "integer", nullable: true),
                    txtInsertedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    dtmInserted = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    txtUpdatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    dtmUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    bitMandatory = table.Column<bool>(type: "boolean", nullable: false),
                    bitActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mPertanyaan", x => x.intPertanyaanID);
                });


            migrationBuilder.CreateTable(
                name: "mJawaban",
                columns: table => new
                {
                    intJawabanID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    txtJawaban = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    intOrderJawaban = table.Column<int>(type: "integer", nullable: true),
                    txtInsertedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    dtmInserted = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    txtUpdatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    dtmUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    bitActive = table.Column<bool>(type: "boolean", nullable: false),
                    txtNomorAduantxtNomorID = table.Column<string>(type: "character varying(50)", nullable: true),
                    txtPertanyaanintPertanyaanID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mJawaban", x => x.intJawabanID);
                    table.ForeignKey(
                        name: "FK_mJawaban_mAduan_txtNomorAduantxtNomorID",
                        column: x => x.txtNomorAduantxtNomorID,
                        principalTable: "mAduan",
                        principalColumn: "txtNomorID");
                    table.ForeignKey(
                        name: "FK_mJawaban_mPertanyaan_txtPertanyaanintPertanyaanID",
                        column: x => x.txtPertanyaanintPertanyaanID,
                        principalTable: "mPertanyaan",
                        principalColumn: "intPertanyaanID",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_mJawaban_txtNomorAduantxtNomorID",
                table: "mJawaban",
                column: "txtNomorAduantxtNomorID");

            migrationBuilder.CreateIndex(
                name: "IX_mJawaban_txtPertanyaanintPertanyaanID",
                table: "mJawaban",
                column: "txtPertanyaanintPertanyaanID");



        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropTable(
                name: "mJawaban");

           

            migrationBuilder.DropTable(
                name: "mLookup");

           

            migrationBuilder.DropTable(
                name: "mPertanyaan");

           
        }
    }
}
