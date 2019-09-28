using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NudeAssignment.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LKPCoverageItemCategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LKPCoverageItemCategories", x => x.CategoryId);
                });

            migrationBuilder.InsertData(
                table: "LKPCoverageItemCategories",
                columns: new[] { "CategoryId", "Description", "Name" },
                values: new object[] { 1, "Cloth items, jacket etc.", "Clothing" });

            migrationBuilder.InsertData(
                table: "LKPCoverageItemCategories",
                columns: new[] { "CategoryId", "Description", "Name" },
                values: new object[] { 2, "TV, Radio, Playstation...", "Electroics" });

            migrationBuilder.InsertData(
                table: "LKPCoverageItemCategories",
                columns: new[] { "CategoryId", "Description", "Name" },
                values: new object[] { 3, "Pots, Flatware...", "kitchen" });

            migrationBuilder.CreateTable(
                name: "CoverageItems",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoverageItems", x => x.ItemId);
                    table.ForeignKey("FK_LKPCoverageItemCategory_CategoryId", x => x.CategoryId, "LKPCoverageItemCategories", "CategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerItems",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    ItemId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerItems", x => x.Id);
                    table.ForeignKey("FK_Customer_CustomerId", x => x.CustomerId, "Customers", "CustomerId");
                    table.ForeignKey("FK_CoverageItems_ItemId", x => x.ItemId, "CoverageItems", "ItemId");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoverageItems");

            migrationBuilder.DropTable(
                name: "CustomerItems");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "LKPCoverageItemCategories");
        }
    }
}
