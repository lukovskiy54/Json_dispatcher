using System.Collections.ObjectModel;

namespace oop_lab3;

public partial class EditView : ContentPage
{
    public string TitleText { get; set; }
    public int Id { get; set; }    public string AnnotationText { get; set; }
    public string AuthorText { get; set; }
    public string FilePathText { get; set; }
    public string CommentAuthorText { get; set; }
    public string ContentText { get; set; }
    public ObservableCollection<Comment> AddedComments { get; set; }

    public EditView()
	{
		InitializeComponent();     

        FileObject file = FileObject.GetInstance();

        file.FindIndex();

        if (file.Data.Count > 0 && file.index < file.Data.Count)
        {
            this.TitleText = file.Data[file.index].Title;

            this.Id = file.Data[file.index].Id;

            this.AnnotationText = file.Data[file.index].Annotation;

            this.AuthorText = file.Data[file.index].Author;

            this.FilePathText = file.Data[file.index].FilePath;

            this.AddedComments = file.Data[file.index].Comments;

            CommentsCollectionView.ItemsSource = AddedComments;
        }

        

        BindingContext = this;
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Application.Current.CloseWindow(this.Window);
    }

    private void OkClicked(object sender, EventArgs e)
    {
        FileObject file = FileObject.GetInstance();
        file.EditArticle(TitleText, AnnotationText, AuthorText, FilePathText, AddedComments, Id);
        Application.Current.CloseWindow(this.Window);
    }

    private void DeleteCommentClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button != null)
        {
            var comment = button.BindingContext as Comment;
            if (comment != null)
            {
                FileObject file = FileObject.GetInstance();
                file.DeleteComment(comment);
            }
        }
    }

    private void OpenChildWindowButtonClicked(object sender, EventArgs e)
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
}