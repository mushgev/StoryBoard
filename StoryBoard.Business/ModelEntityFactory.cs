using StoryBoard.Business.Helpers;
using StoryBoard.Data;
using StoryBoard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryBoard.Business
{
    public class ModelEntityFactory
    {
        public UserModel GetModel(User entity)
        {
            if(entity == null)
            {
                return null;
            }

            return new UserModel
            {
                UserId = entity.UserId,
                Name = entity.Name
            };
        }

        public User GetEntity(UserModel model)
        {
            if (model == null)
            {
                return null;

            }

            return new User
            {
                UserId = model.UserId,
                Name = model.Name
            };
        }

        public User GetEntity(UserRegisterModel model)
        {
            if (model == null)
            {
                return null;

            }
            return new User
            {
                Name = model.Name,
                Password = Security.HashSHA1(model.Password)
            };
        }

        public GroupModel GetModel(Group entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new GroupModel
            {
                GroupId = entity.GroupId,
                OwnerId = entity.OwnerId,
                Name = entity.Name,
                Description = entity.Description
            };
        }

        public Group GetEntity(GroupModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new Group
            {
                GroupId = model.GroupId,
                OwnerId = model.OwnerId,
                Name = model.Name,
                Description = model.Description
            };
        }

        public StoryModel GetModel(Story entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new StoryModel
            {
                StoryId = entity.StoryId,
                UserId = entity.UserId,
                Title = entity.Title,
                Description = entity.Description,
                Content = entity.Content,
                PostedOn = entity.PostedOn,
                Groups = entity.Groups.Select(g => g.GroupId).ToList()
            };
        }

        public Story GetEntity(StoryModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new Story
            {
                StoryId = model.StoryId,
                UserId = model.UserId,
                Title = model.Title,
                Description = model.Description,
                Content = model.Content,
                PostedOn = model.PostedOn
            };
        }
    }
}
