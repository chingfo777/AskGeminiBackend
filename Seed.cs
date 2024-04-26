using AskGemini.Data;
using AskGemini.Models;

namespace AskGemini
{
    public class Seed 
    {
        private readonly DataContext _dataContext;
        public Seed(DataContext Context)
        {
            _dataContext = Context;
        }
        
        public void SeedDataContext()
        {
            if(!_dataContext.Users.Any())
            {
                var users = new List<User>()
                {
                    new User()
                    {                        
                            Id = "Abc1",
                            Name = "Subha Shil",
                            Email = "subha@gmail.com",
                            phoneNumber = "1234567890"               
                        
                    }
                };

                _dataContext.Users.AddRange(users);
                _dataContext.SaveChanges();
            }
        }
    }
}
