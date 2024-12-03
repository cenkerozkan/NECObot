# NECOBot

## Description
This is a project for my ISE413 .NET MVC Programming course.
NECOBot is a Discord bot that is designed to help users with their daily tasks. It is a multipurpose bot that can be used for various purposes such as moderation, fun, and utility. The bot is designed to be user-friendly and easy to use. It is constantly being updated with new features and improvements to make it more useful and enjoyable for users.

## TODO
- [x] Create a symbolic chat page
- [x] Create BLL Project with DAL directory for data access.
- [] Create services for:
  - [] Deleting chats
  - [] Getting chats
  - [] Getting message histories
    - [] Sort them with role and timestamp
  - [] Updating chat names
- [] Create a service to interact with the python backend


## Migrations:
- Here is what you should do if you want to apply a migration:
  - Open the terminal
  - Navigate to the project directory
  - Run the following command:
    ```bash
    dotnet ef database update
    ```
  - If you want to create a new migration, run the following command:
    ```bash
    dotnet ef migrations add <MigrationName>
    ```
    - Replace `<MigrationName>` with the name of the migration you want to create.
    - After creating the migration, you can apply it by running the `dotnet ef database update` command.