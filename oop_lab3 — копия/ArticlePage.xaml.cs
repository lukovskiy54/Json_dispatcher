using System.Collections.ObjectModel;

namespace oop_lab3;

public partial class ArticlePage : ContentPage
{
    FileManager fileManager;
    public ObservableCollection<Article> Articles { get; set; }

    public ArticlePage()
    {
        var file = FileObject.GetInstance();
        InitializeComponent();
        Articles = file.Data;
        BindingContext = this;
    }
}