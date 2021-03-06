﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryBoard.Data
{
    public class Group
    {
        public int GroupId { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [ForeignKey("OwnerId")]
        public virtual User Owner { get; set; }
        public virtual List<User> Users { get; set; }
        public virtual List<Story> Stories { get; set; }
    }
}
