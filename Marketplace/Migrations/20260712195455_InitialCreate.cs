using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassifiedAds",
                columns: table => new
                {
                    ClassifiedAdId = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerId_Value = table.Column<Guid>(type: "uuid", nullable: true),
                    Title_Value = table.Column<string>(type: "text", nullable: true),
                    Text_Value = table.Column<string>(type: "text", nullable: true),
                    Price_Amount = table.Column<decimal>(type: "numeric", nullable: true),
                    Price_Currency_CurrencyCode = table.Column<string>(type: "text", nullable: true),
                    Price_Currency_InUse = table.Column<bool>(type: "boolean", nullable: true),
                    Price_Currency_DecimalPlaces = table.Column<int>(type: "integer", nullable: true),
                    ApprovedBy_Value = table.Column<Guid>(type: "uuid", nullable: true),
                    State = table.Column<int>(type: "integer", nullable: false),
                    Id_Value = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassifiedAds", x => x.ClassifiedAdId);
                });

            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    PictureId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentId_Value = table.Column<Guid>(type: "uuid", nullable: false),
                    Size_Width = table.Column<int>(type: "integer", nullable: false),
                    Size_Height = table.Column<int>(type: "integer", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    ClassifiedAdId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.PictureId);
                    table.ForeignKey(
                        name: "FK_Picture_ClassifiedAds_ClassifiedAdId",
                        column: x => x.ClassifiedAdId,
                        principalTable: "ClassifiedAds",
                        principalColumn: "ClassifiedAdId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Picture_ClassifiedAdId",
                table: "Picture",
                column: "ClassifiedAdId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Picture");

            migrationBuilder.DropTable(
                name: "ClassifiedAds");
        }
    }
}
