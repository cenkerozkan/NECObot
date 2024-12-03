using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BLL.Migrations
{
    /// <inheritdoc />
    public partial class new_notation_in_message_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_chat_threads_ChatThreadId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "messages");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ChatThreadId",
                table: "messages",
                newName: "IX_messages_ChatThreadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_messages",
                table: "messages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_messages_chat_threads_ChatThreadId",
                table: "messages",
                column: "ChatThreadId",
                principalTable: "chat_threads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messages_chat_threads_ChatThreadId",
                table: "messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_messages",
                table: "messages");

            migrationBuilder.RenameTable(
                name: "messages",
                newName: "Messages");

            migrationBuilder.RenameIndex(
                name: "IX_messages_ChatThreadId",
                table: "Messages",
                newName: "IX_Messages_ChatThreadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_chat_threads_ChatThreadId",
                table: "Messages",
                column: "ChatThreadId",
                principalTable: "chat_threads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
