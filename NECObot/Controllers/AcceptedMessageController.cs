using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using BLL.DAL;

[Authorize(Roles = "Admin")]
public class AcceptedMessageController : Controller
{
    private readonly Db _context;

    public AcceptedMessageController(Db context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> IsMessageAccepted(Guid messageId)
    {
        var exists = await _context.AcceptedMessages.AnyAsync(am => am.MessageId == messageId);
        return Json(exists);
    }

    [HttpPost]
    public async Task<IActionResult> AcceptMessage(Guid messageId)
    {
        var message = await _context.Messages
            .Include(m => m.ChatThread)
            .FirstOrDefaultAsync(m => m.Id == messageId);

        if (message == null)
            return NotFound("Message not found");

        if (message.Role != "bot")
            return BadRequest("Only bot messages can be accepted");

        var acceptedMessage = new AcceptedMessage
        {
            MessageId = messageId,
            Content = message.Content,
            Role = message.Role,
            Timestamp = message.Timestamp,
            AcceptedBy = User.Identity.Name,
            AcceptedTimestamp = DateTime.UtcNow
        };

        _context.AcceptedMessages.Add(acceptedMessage);
        await _context.SaveChangesAsync();

        return Ok();
    }

    public async Task<IActionResult> Index()
    {
        var acceptedMessages = await _context.AcceptedMessages
            .Include(am => am.Message)
            .ThenInclude(m => m.ChatThread)
            .OrderByDescending(am => am.AcceptedTimestamp)
            .ToListAsync();

        return View(acceptedMessages);
    }
} 