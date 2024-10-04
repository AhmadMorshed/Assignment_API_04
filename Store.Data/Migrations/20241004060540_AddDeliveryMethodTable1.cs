using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDeliveryMethodTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_deliveryMethods",
                table: "deliveryMethods");

            migrationBuilder.RenameTable(
                name: "deliveryMethods",
                newName: "DeliveryMethod");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryMethod",
                table: "DeliveryMethod",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryMethod",
                table: "DeliveryMethod");

            migrationBuilder.RenameTable(
                name: "DeliveryMethod",
                newName: "deliveryMethods");

            migrationBuilder.AddPrimaryKey(
                name: "PK_deliveryMethods",
                table: "deliveryMethods",
                column: "Id");
        }
    }
}
