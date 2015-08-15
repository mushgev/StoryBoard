using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryBoard.Data
{
    public class StoryBoardContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<Group> Groups { get; set; }
    }
}
