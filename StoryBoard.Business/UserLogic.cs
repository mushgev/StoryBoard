using StoryBoard.Business.Helpers;
using StoryBoard.Data;
using StoryBoard.Localization;
using StoryBoard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryBoard.Business
{
    public class UserLogic : BaseLogic
    {
        public UserLogic(ModelEntityFactory factory)
            : base(factory)
        {

        }

        public async Task Register(UserRegisterModel model)
        {
            using (var db = new StoryBoardContext())
            {
                if(db.Users.Any(u => u.Name.ToLower() == model.Name.ToLower()))
                {
                    throw new ArgumentException(Resources.UserNameExists, "Name");
                }

                var user = _factory.GetEntity(model);
                db.Users.Add(user);
                await db.SaveChangesAsync();
            }
        }

        public void Login(UserLoginModel model)
        {
            using (var db = new StoryBoardContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Name == model.Name);
                if(user == null)
                {
                    throw new ArgumentException(Resources.IncorrectNameOrPassword, "");
                }

                if(Security.HashSHA1(model.Password) != user.Password)
                {
                    throw new ArgumentException(Resources.IncorrectNameOrPassword, "");
                }
            }
        }

        public UserModel Get(string name)
        {
            using(var db = new StoryBoardContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Name == name);
                return _factory.GetModel(user);
            }
        }
    }
}
