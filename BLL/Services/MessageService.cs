using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BLL.DAL;

namespace BLL.Services
{
    public class MessagesService : Service , IService<Message, MessageModel>
    {
        public MessagesService(Db db) : base(db)
        {
        }
}