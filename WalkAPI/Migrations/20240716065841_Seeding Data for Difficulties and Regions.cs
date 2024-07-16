using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WalkAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataforDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_walks_difficulties_DifficultyId",
                table: "walks");

            migrationBuilder.DropForeignKey(
                name: "FK_walks_regions_RegionId",
                table: "walks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_walks",
                table: "walks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_regions",
                table: "regions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_difficulties",
                table: "difficulties");

            migrationBuilder.RenameTable(
                name: "walks",
                newName: "Walks");

            migrationBuilder.RenameTable(
                name: "regions",
                newName: "Regions");

            migrationBuilder.RenameTable(
                name: "difficulties",
                newName: "Difficulties");

            migrationBuilder.RenameIndex(
                name: "IX_walks_RegionId",
                table: "Walks",
                newName: "IX_Walks_RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_walks_DifficultyId",
                table: "Walks",
                newName: "IX_Walks_DifficultyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Walks",
                table: "Walks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Regions",
                table: "Regions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Difficulties",
                table: "Difficulties",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("51324663-b646-487b-b7b5-185a7e68fee7"), "Hard" },
                    { new Guid("6732d6ee-977c-4081-aff1-f3e7c9a4f6e8"), "Medium" },
                    { new Guid("8626d6a1-9e34-4250-a94f-6a7203af9b80"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImgUrl" },
                values: new object[,]
                {
                    { new Guid("1a95b499-1c1c-4f43-8d5f-f1e7dfd9bcc9"), "KV", "Khanh", null },
                    { new Guid("3dcd48d5-4ceb-47de-a99f-1b5284ce0b10"), "VHH", "Huy", "https://www.bing.com/images/search?view=detailV2&ccid=2TumufLJ&id=BB5718BF86899F23683761741B9FFD007A8EE8F6&thid=OIP.2TumufLJYnKvtmU6UMWe6wHaE8&mediaurl=https%3a%2f%2fwww.imgacademy.com%2fsites%2fdefault%2ffiles%2flegacy-hotel-20.jpg&cdnurl=https%3a%2f%2fth.bing.com%2fth%2fid%2fR.d93ba6b9f2c96272afb6653a50c59eeb%3frik%3d9uiOegD9nxt0YQ%26pid%3dImgRaw%26r%3d0&exph=1067&expw=1600&q=img&simid=607990275582734595&FORM=IRPRST&ck=F13EACAFAEF9792505C5B101D9D2052D&selectedIndex=0&itb=0" },
                    { new Guid("be2e701f-2fe5-4e75-927e-8f1bbaee274b"), "BNH", "Hieu", "https://www.bing.com/images/search?view=detailV2&ccid=2TumufLJ&id=BB5718BF86899F23683761741B9FFD007A8EE8F6&thid=OIP.2TumufLJYnKvtmU6UMWe6wHaE8&mediaurl=https%3a%2f%2fwww.imgacademy.com%2fsites%2fdefault%2ffiles%2flegacy-hotel-20.jpg&cdnurl=https%3a%2f%2fth.bing.com%2fth%2fid%2fR.d93ba6b9f2c96272afb6653a50c59eeb%3frik%3d9uiOegD9nxt0YQ%26pid%3dImgRaw%26r%3d0&exph=1067&expw=1600&q=img&simid=607990275582734595&FORM=IRPRST&ck=F13EACAFAEF9792505C5B101D9D2052D&selectedIndex=0&itb=0" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_Difficulties_DifficultyId",
                table: "Walks",
                column: "DifficultyId",
                principalTable: "Difficulties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_Regions_RegionId",
                table: "Walks",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walks_Difficulties_DifficultyId",
                table: "Walks");

            migrationBuilder.DropForeignKey(
                name: "FK_Walks_Regions_RegionId",
                table: "Walks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Walks",
                table: "Walks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Regions",
                table: "Regions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Difficulties",
                table: "Difficulties");

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("51324663-b646-487b-b7b5-185a7e68fee7"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("6732d6ee-977c-4081-aff1-f3e7c9a4f6e8"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("8626d6a1-9e34-4250-a94f-6a7203af9b80"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("1a95b499-1c1c-4f43-8d5f-f1e7dfd9bcc9"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("3dcd48d5-4ceb-47de-a99f-1b5284ce0b10"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("be2e701f-2fe5-4e75-927e-8f1bbaee274b"));

            migrationBuilder.RenameTable(
                name: "Walks",
                newName: "walks");

            migrationBuilder.RenameTable(
                name: "Regions",
                newName: "regions");

            migrationBuilder.RenameTable(
                name: "Difficulties",
                newName: "difficulties");

            migrationBuilder.RenameIndex(
                name: "IX_Walks_RegionId",
                table: "walks",
                newName: "IX_walks_RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_Walks_DifficultyId",
                table: "walks",
                newName: "IX_walks_DifficultyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_walks",
                table: "walks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_regions",
                table: "regions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_difficulties",
                table: "difficulties",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_walks_difficulties_DifficultyId",
                table: "walks",
                column: "DifficultyId",
                principalTable: "difficulties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_walks_regions_RegionId",
                table: "walks",
                column: "RegionId",
                principalTable: "regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
