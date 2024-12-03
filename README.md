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

## Migrations
### Creating a Migration
1. **Open the terminal**.
2. **Navigate to the project directory**:
    ```bash
    cd /path/to/your/project/BLL
    ```
3. **Create a new migration**:
    ```bash
    dotnet ef migrations add <MigrationName>
    ```
  - Replace `<MigrationName>` with the name of the migration you want to create.

### Applying a Migration to the Database
1. **Open the terminal**.
2. **Navigate to the project directory**:
    ```bash
    cd /path/to/your/project/BLL
    ```
3. **Apply the migration**:
    ```bash
    dotnet ef database update
    ```

### Removing the Last Migration
1. **Open the terminal**.
2. **Navigate to the project directory**:
    ```bash
    cd /path/to/your/project/BLL
    ```
3. **Remove the last migration**:
    ```bash
    dotnet ef migrations remove
    ```
  - This will remove the last migration that was added.

### Reverting to a Previous Migration
1. **Open the terminal**.
2. **Navigate to the project directory**:
    ```bash
    cd /path/to/your/project/BLL
    ```
3. **Revert to a specific migration**:
    ```bash
    dotnet ef database update <MigrationName>
    ```
  - Replace `<MigrationName>` with the name of the migration you want to revert to. To revert all migrations, use `0` as the migration name:
    ```bash
    dotnet ef database update 0
    ```

By following these steps, you can manage your Entity Framework Core migrations effectively.