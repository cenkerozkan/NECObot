using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using BLL.DAL;
using BLL.Services;

[Authorize(Roles = "Admin")]
public class UserManagementController : Controller
{
    private readonly Db _context;
    private readonly IUserAuthenticationService _authService;

    public UserManagementController(Db context, IUserAuthenticationService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ToListAsync();
        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound();

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> ToggleAdminRole(Guid userId)
    {
        var user = await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return NotFound();

        var adminRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
        if (adminRole == null)
            return NotFound("Admin role not found");

        var existingAdminRole = user.UserRoles.FirstOrDefault(ur => ur.RoleId == adminRole.Id);
        if (existingAdminRole != null)
        {
            _context.UserRoles.Remove(existingAdminRole);
        }
        else
        {
            user.UserRoles.Add(new UserRole { UserId = userId, RoleId = adminRole.Id });
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
} 