using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryBoard.Model
{
    public class GroupModel
    {
        public int GroupId { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsSubscribed { get; set; }
        public int SubscribersCount { get; set; }
        public int StoriesCount { get; set; }
    }
}
