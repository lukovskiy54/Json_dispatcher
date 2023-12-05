using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab3
{
    public class Article : INotifyPropertyChanged
    {
        public string Title { get; set; }
        public string Annotation { get; set; }
        public string Author { get; set; }
        public string FilePath { get; set; }
        public ObservableCollection<Comment> Comments { get; set; }
        public int Id { get; set; }

        public bool IsSelected { get; set; }

        public Article() { }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }

        public void RemoveComment(Comment comment)
        {
            Comments.Remove(comment);
        }
    }
}
