using System.Collections.ObjectModel;
using System.Diagnostics;


namespace oop_lab3
{
    internal class FileObject
    {

        private static FileObject _instance;

        public string FilePath { get; set; }
        public string FileContent { get; set; }

        public event Action OnArticleUpdated;

        private ISearch searchStrategy;

        public ObservableCollection<Article> Data { get; set; }

        public ObservableCollection<Article> Articles_buffer { get; set; }
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


        public ObservableCollection<Article> linqSearch(string searchCriterion, string searchText)
        {
            this.Articles_buffer = this.Data;
            var linqSearchStrategy = new LinqSearch();
            var filteredArticles = linqSearchStrategy.Search(searchCriterion, searchText);
            this.Data = Articles_buffer;
            return filteredArticles;
        }

        public void addArticle(string TitleText, string AnnotationText, string AuthorText, string FilePathText, ObservableCollection<Comment> comments, int Id)
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

        public void editArticle(string TitleText, string AnnotationText, string AuthorText, string FilePathText, ObservableCollection<Comment> comments, int Id)
        {

            Article newarticle = new Article { Title = TitleText, Annotation = AnnotationText, Author = AuthorText, FilePath = FilePathText, Comments = comments, Id = Id };


            this.Data[this.index] = newarticle;

            OnArticleUpdated?.Invoke();
        }

        public void deleteComment(Comment comment)
        {
            this.Data[this.index].Comments.Remove(comment);
            OnArticleUpdated?.Invoke();
        }

        public void deleteArticle()
        {
            this.findIndex();
            Debug.WriteLine(this.index);
            if (this.index >= 0 && this.index < this.Data.Count)
            {
                this.Data.Remove(this.Data[this.index]);
            }
        }

        public bool isOpened()
        {
            return this.FilePath != null;
        }

        public void findIndex()
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
