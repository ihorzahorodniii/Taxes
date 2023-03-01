using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Taxes.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Municipality",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipality", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tax",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    MunicipalityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tax", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tax_Municipality_MunicipalityId",
                        column: x => x.MunicipalityId,
                        principalTable: "Municipality",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tax_TaxType_TaxTypeId",
                        column: x => x.TaxTypeId,
                        principalTable: "TaxType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Municipality",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("a8577c30-bd98-4bee-b1ed-bede0812df24"), "Copenhagen" });

            migrationBuilder.InsertData(
                table: "TaxType",
                columns: new[] { "Id", "Name", "Priority" },
                values: new object[,]
                {
                    { new Guid("19064510-fdad-41ec-a52b-f9aad3099549"), "yearly", (short)1 },
                    { new Guid("9720e647-f87c-4fed-a84d-c18e4f7a6024"), "daily", (short)4 },
                    { new Guid("a7d328d1-0c15-4fd5-a5a2-f4dbe5ddb3e8"), "montly", (short)2 },
                    { new Guid("c427ee85-afe4-4910-90c2-e42d19149821"), "weekly", (short)3 }
                });

            migrationBuilder.InsertData(
                table: "Tax",
                columns: new[] { "Id", "MunicipalityId", "TaxTypeId", "ValidFrom", "Value" },
                values: new object[,]
                {
                    { new Guid("091cd1c2-26d4-4af1-9152-df5a59e7c648"), new Guid("a8577c30-bd98-4bee-b1ed-bede0812df24"), new Guid("19064510-fdad-41ec-a52b-f9aad3099549"), new DateTime(2016, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.20000000000000001 },
                    { new Guid("0bef2975-c8d4-4e28-ae0f-7f030663969c"), new Guid("a8577c30-bd98-4bee-b1ed-bede0812df24"), new Guid("9720e647-f87c-4fed-a84d-c18e4f7a6024"), new DateTime(2016, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.10000000000000001 },
                    { new Guid("42e189cb-7963-4fd5-a3f7-4cf8db7d1b3b"), new Guid("a8577c30-bd98-4bee-b1ed-bede0812df24"), new Guid("9720e647-f87c-4fed-a84d-c18e4f7a6024"), new DateTime(2016, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.10000000000000001 },
                    { new Guid("4b114786-b308-46af-b371-5634f062871b"), new Guid("a8577c30-bd98-4bee-b1ed-bede0812df24"), new Guid("a7d328d1-0c15-4fd5-a5a2-f4dbe5ddb3e8"), new DateTime(2016, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.40000000000000002 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tax_MunicipalityId",
                table: "Tax",
                column: "MunicipalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tax_TaxTypeId",
                table: "Tax",
                column: "TaxTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tax");

            migrationBuilder.DropTable(
                name: "Municipality");

            migrationBuilder.DropTable(
                name: "TaxType");
        }
    }
}
