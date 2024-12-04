#nullable disable
using BLL.DAL;

namespace BLL.Services.Bases
{
    public abstract class Service
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }

        protected readonly Db _db;
        
        // This is a protected constructor. Using it like this is a good practice
        // while developing and abstract class. It is used to initialize the _db field.
        protected Service(Db db)
        {
            _db = db;
        }
        
        // This is a method that returns a Service object with IsSuccessful set to true.
        public Service Success(string message = "")
        {
            IsSuccessful = true;
            Message = message;
            return this;
        }
        
        // This is a method that returns a Service object with IsSuccessful set to false.
        public Service Error(string message = "")
        {
            IsSuccessful = false;
            Message = message;
            return this;
        }
    }
}