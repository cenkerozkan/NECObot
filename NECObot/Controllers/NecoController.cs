#nullable disable
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Services;
using BLL.Models;
using BLL.Services.Bases;
using BLL.DAL;  

namespace NECObot.Controllers
{
    // I believe single controller would be enough for handling
    // a single chat page. So I am not going to create two different
    // controllers for ChatThread and Message.
    public class NecoController : Controller
    {
        // Needed services.
        private readonly ILogger<NecoController> _logger;
        private readonly IService<ChatThread, ChatThreadModel> _chatThreadService;
        private readonly IService<Message, MessageModel> _messageService;
        
        
        // Constructor with injected services.
        public NecoController(
            ILogger<NecoController> logger,
            IService<ChatThread, ChatThreadModel> chatThreadService,
            IService<Message, MessageModel> messageService)
        {
            // Injected services.
            _logger = logger;
            _chatThreadService = chatThreadService;
            _messageService = messageService;
            
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TryNeco()
        {
            var chatThreads = _chatThreadService.Query().ToList();
            return View(chatThreads);
        }
        
        public IActionResult AboutNeco()
        {
            return View();
        }

        // To get all chat threads.
        [HttpGet]
        public IActionResult GetAllChats()
        {
            var chatThreads = _chatThreadService.Query()
                .Select(x => new ChatThreadModel
                {
                    Id = x.Record.Id,
                    Title = x.Record.Title,
                    CreatedAt = x.Record.CreatedAt,
                    Record = x.Record
                })
                .ToList();
            return PartialView("_Partials/_ChatThreads", chatThreads);
        }
        
        // To get messages from a specific chat thread.
        [HttpGet]
        public IActionResult GetMessages(Guid threadId)
        {
            var messages = _messageService.Query().Where(x => x.ChatThreadId == threadId).ToList();
            return PartialView("_Partials/_Messages", messages);
        }


        // To delete chat threads.
        [HttpPost]
        public IActionResult DeleteChatThread(Guid id)
        {
            var result = _chatThreadService.Delete(id);
            if (!result.IsSuccessful)   
                return BadRequest(result.Message);
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateChatThread(string title)
        {
            var chatThread = new ChatThread { Title = title };
            var result = _chatThreadService.Create(chatThread);
            
            if (!result.IsSuccessful)
                return BadRequest(result.Message);
            
            return Ok();
        }

        [HttpPost]
        public IActionResult SendMessage(Guid threadId, string content)
        {
            // USER PART.
            // Create and save user message
            var userMessage = new Message 
            { 
                Id = Guid.NewGuid(),
                ChatThreadId = threadId,
                Content = content,
                Role = "user",
                Timestamp = DateTime.UtcNow
            };
            
            var userResult = _messageService.Create(userMessage);
            if (!userResult.IsSuccessful)
                return BadRequest(userResult.Message);
            
            // BOT PART.
            // Create and save bot message
            // I need to bring gemini service result into
            // Content field of Message. Call Gemini service above this function
            var botMessage = new Message 
            { 
                Id = Guid.NewGuid(),
                ChatThreadId = threadId,
                Content = "I am NECO, your symbolic assistant! 🤖",
                Role = "bot",
                Timestamp = DateTime.UtcNow
            };
            
            var botResult = _messageService.Create(botMessage);
            if (!botResult.IsSuccessful)
                return BadRequest(botResult.Message);
            
            return Ok();
        }
    }
}
