using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab3
{
    public class Comment
    {
        public string Author { get; set; }
        public string Content { get; set; }

        public Comment(string author, string content)
        {
            Author = author;
            Content = content;
        }
    }
}
