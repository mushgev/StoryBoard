using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryBoard.Data
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        [InverseProperty("Owner")]
        public virtual List<Group> GroupsOwn { get; set; }
        public virtual List<Group> Groups { get; set; }
        public virtual List<Story> Stories { get; set; }
    }
}
