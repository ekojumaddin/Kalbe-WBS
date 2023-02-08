using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBSBE.DAL.Migrations
{
    /// <inheritdoc />
    public partial class perbaikanmJawaban : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mJawaban_mPertanyaan_txtPertanyaanintPertanyaanID",
                table: "mJawaban");

            migrationBuilder.DropIndex(
                name: "IX_mJawaban_txtPertanyaanintPertanyaanID",
                table: "mJawaban");

            migrationBuilder.DropColumn(
                name: "txtPertanyaanintPertanyaanID",
                table: "mJawaban");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "txtPertanyaanintPertanyaanID",
                table: "mJawaban",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_mJawaban_txtPertanyaanintPertanyaanID",
                table: "mJawaban",
                column: "txtPertanyaanintPertanyaanID");

            migrationBuilder.AddForeignKey(
                name: "FK_mJawaban_mPertanyaan_txtPertanyaanintPertanyaanID",
                table: "mJawaban",
                column: "txtPertanyaanintPertanyaanID",
                principalTable: "mPertanyaan",
                principalColumn: "intPertanyaanID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
