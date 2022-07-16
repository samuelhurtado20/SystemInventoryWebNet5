using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemInventory.DataAccess.Migrations
{
    public partial class WarehouseProductentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Warehouse_WarehouseaId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseProduct_Product_ProductId",
                table: "WarehouseProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseProduct_Warehouse_WarehouseId",
                table: "WarehouseProduct");

            migrationBuilder.DropColumn(
                name: "BodegaId",
                table: "WarehouseProduct");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "WarehouseProduct");

            migrationBuilder.RenameColumn(
                name: "WarehouseaId",
                table: "Inventory",
                newName: "WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_WarehouseaId",
                table: "Inventory",
                newName: "IX_Inventory_WarehouseId");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "WarehouseProduct",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "WarehouseProduct",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Warehouse_WarehouseId",
                table: "Inventory",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseProduct_Product_ProductId",
                table: "WarehouseProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseProduct_Warehouse_WarehouseId",
                table: "WarehouseProduct",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Warehouse_WarehouseId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseProduct_Product_ProductId",
                table: "WarehouseProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseProduct_Warehouse_WarehouseId",
                table: "WarehouseProduct");

            migrationBuilder.RenameColumn(
                name: "WarehouseId",
                table: "Inventory",
                newName: "WarehouseaId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_WarehouseId",
                table: "Inventory",
                newName: "IX_Inventory_WarehouseaId");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "WarehouseProduct",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "WarehouseProduct",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BodegaId",
                table: "WarehouseProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "WarehouseProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Warehouse_WarehouseaId",
                table: "Inventory",
                column: "WarehouseaId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseProduct_Product_ProductId",
                table: "WarehouseProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseProduct_Warehouse_WarehouseId",
                table: "WarehouseProduct",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
