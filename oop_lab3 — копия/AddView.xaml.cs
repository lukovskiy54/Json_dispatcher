using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Xml.Linq;

namespace oop_lab3
{
    public partial class AddView : ContentPage
    {
        public string TitleText { get; set; }
        public int Id { get; set; }
        public string AnnotationText { get; set; }
        public string AuthorText { get; set; }
        public string FilePathText { get; set; }
        public string CommentAuthorText { get; set; }
        public string ContentText { get; set; }
        public string DateText { get; set; }

        public ObservableCollection<Comment> AddedComments { get; set; }

        public event EventHandler ArticleAdded;

        public AddView()
        {
            InitializeComponent();

            BindingContext = this;
            AddedComments = new ObservableCollection<Comment>();
            CommentsCollectionView.ItemsSource = AddedComments;
        }

        private void OpenCommentWindowButtonClicked(object sender, EventArgs e)
        {
            AddComment addCommentWindow = new AddComment();
            addCommentWindow.InputValuesSubmitted += ChildPageInputValuesSubmitted;
            Window thirdWindow = new Window(addCommentWindow);
            Application.Current.OpenWindow(thirdWindow);
        }

        private void ChildPageInputValuesSubmitted(object sender, string inputValues)
        {
            string[] values = inputValues.Split(',');
            string author = values[0];
            string content = values[1];
            Comment comment = new Comment(author, content);
            AddedComments.Add(comment);
            CommentsCollectionView.ItemsSource = null;
            CommentsCollectionView.ItemsSource = AddedComments;
        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            FileObject file = FileObject.GetInstance();
            file.AddArticle(TitleText, AnnotationText, AuthorText, FilePathText, AddedComments, Id);
            file.index = file.Data.Count - 1;
            ArticleAdded?.Invoke(this, EventArgs.Empty);
            Application.Current.CloseWindow(this.Window);
        }
    }
}
