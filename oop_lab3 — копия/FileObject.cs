using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace oop_lab3
{
    internal class FileObject
    {
        private static FileObject _instance;

        public string FilePath { get; set; }
        public string FileContent { get; set; }

        public event Action OnArticleUpdated;

        public ObservableCollection<Article> Data { get; set; }
        public ObservableCollection<Article> ArticlesBuffer { get; set; }
        public int index { get; set; }

        private FileObject()
        {
            this.index = 0;
            this.Data = new ObservableCollection<Article>();
        }

        public static FileObject GetInstance()
        {
            if (_instance == null)
            {
                _instance = new FileObject();
            }
            return _instance;
        }

        public ObservableCollection<Article> Search(ISearch searchStrategy, string searchCriterion, string searchText)
        {
            this.ArticlesBuffer = this.Data;
            var filteredArticles = searchStrategy.Search(searchCriterion, searchText);
            this.Data = ArticlesBuffer;
            return filteredArticles;
        }

        public void UpdateDataToBuffer()
        {
            this.Data = this.ArticlesBuffer;
        }

        public void AddArticle(string TitleText, string AnnotationText, string AuthorText, string FilePathText, ObservableCollection<Comment> comments, int Id)
        {
            Article newArticle = new Article
            {
                Title = TitleText,
                Annotation = AnnotationText,
                Author = AuthorText,
                FilePath = FilePathText,
                Comments = comments,
                Id = Id,
            };
            this.Data.Add(newArticle);
        }

        public void EditArticle(string TitleText, string AnnotationText, string AuthorText, string FilePathText, ObservableCollection<Comment> comments, int Id)
        {
            Article newArticle = new Article
            {
                Title = TitleText,
                Annotation = AnnotationText,
                Author = AuthorText,
                FilePath = FilePathText,
                Comments = comments,
                Id = Id
            };

            this.Data[this.index] = newArticle;
            OnArticleUpdated?.Invoke();
        }

        public void DeleteComment(Comment comment)
        {
            this.Data[this.index].Comments.Remove(comment);
            OnArticleUpdated?.Invoke();
        }

        public void DeleteArticle()
        {
            this.FindIndex();
            Debug.WriteLine(this.index);
            if (this.index >= 0 && this.index < this.Data.Count)
            {
                this.Data.Remove(this.Data[this.index]);
            }
        }

        public bool IsOpened()
        {
            return this.FilePath != null;
        }

        public void FindIndex()
        {
            for (int i = 0; i < this.Data.Count; i++)
            {
                if (this.Data[i].IsSelected)
                {
                    this.index = i;
                    break;
                }
            }
        }
    }
}
