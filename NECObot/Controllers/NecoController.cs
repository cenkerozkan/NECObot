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
        private readonly GeminiService _geminiService; 
        
        
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
            _geminiService = new GeminiService();
            
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
            // Returns the main view of NECO bot.
            return View();
        }

        // To get all chat threads.
        [HttpGet]
        public IActionResult GetAllChats()
        {
            // Returns the partial view of all chat threads.
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
            // Returns the partial view of messages in a chat thread.
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
        public async Task<IActionResult> SendMessage(Guid threadId, string content)
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
            
            // First I'm saving the user message into the database.
            var userResult = _messageService.Create(userMessage);
            if (!userResult.IsSuccessful)
                return BadRequest(userResult.Message);
            
            // BOT PART.
            // Create and save bot message
            // I need to bring gemini service result into
            // Content field of Message. Call Gemini service above this function
            // To provide the context, I need to take last six messages of 
            // the ongoing thread and send them to Gemini service.
            
            // NEW ADDITION:
            // I'm now trying to send the message content which is
            // Taken from new userMessage object to the Gemini service.
            var geminiResult =  await _geminiService.SendGeminiRequest(userMessage.Content);
            var botMessage = new Message
            {
                Id = Guid.NewGuid(),
                ChatThreadId = threadId,
                Content = "",
                Role = "bot",
                Timestamp = DateTime.UtcNow
            };
            botMessage.Content = geminiResult.IsSuccessful ? geminiResult.Message : "Something went wrong with the API.";

            
            var botResult = _messageService.Create(botMessage);
            if (!botResult.IsSuccessful)
                return BadRequest(botResult.Message);
            
            return Ok();
        }
    }
}
