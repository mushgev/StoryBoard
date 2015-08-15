﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryBoard.Data
{
    public class Story
    {
        public int StoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime PostedOn { get; set; }

        public virtual User User { get; set; }
        public virtual List<Group> Groups { get; set; }
    }
}
