using Microsoft.Maui.Controls;
using System.Text.Json;
using System;
using System.IO;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.ComponentModel;



namespace oop_lab3
{
    public partial class MainPage : ContentPage
    {

        private FileManager fileManager;

        private FileObject fileObject;
        public ObservableCollection<Article> Articles { get; set; }

        

        private bool MainPageLocker = false;


        public MainPage()
        {

            InitializeComponent();

            fileManager = new FileManager();

            fileObject = FileObject.GetInstance();

            this.BindingContext = fileObject.Data;

        }

        private void SaveClicked(object sender, EventArgs e)
        {
            fileManager.saveFile(fileObject.FilePath);
        }
        private void EditClicked(object sender, EventArgs e)
        {
            if (MainPageLocker && fileObject.Data.Count > 0)
            {
                fileObject.findIndex();
                Window editWindow = new Window(new EditView());
                Application.Current.OpenWindow(editWindow);
                this.BindingContext = fileObject.Data;
            }
            else
            {
                DisplayAlert("Error", "Cant edit empty file", "OK");
            }
        }
        private void DeleteClicked(object sender, EventArgs e)
        {
            if (MainPageLocker)
            {
                OnPropertyChanged(nameof(fileObject.Data));
                fileObject.deleteArticle();
            }
        }
        private void UpdateArticlesView()
        {
            if (fileObject.Data.Count > 0 && fileObject.index < fileObject.Data.Count)
            {
                OnPropertyChanged(nameof(fileObject.Data));
            }
            else
            {
                this.BindingContext = null;

            }
        }
        private void OnArticleAdded(object sender, EventArgs e)
        {
            UpdateArticlesView();
        }
        private void AddClicked(object sender, EventArgs e)
        {
            if (MainPageLocker)
            {

                var addWindow = new AddView();
                addWindow.ArticleAdded += OnArticleAdded;
                Application.Current.OpenWindow(new Window(addWindow));

            }
            else
            {
                DisplayAlert("Error", "Open file first", "OK");
            }
        }
        private void AboutClicked(object sender, EventArgs e)
        {
            var aboutView = new AboutView();
            var aboutWindow = new Window(aboutView)
            {
                Width = 400,
                Height = 200
            };
            Application.Current.OpenWindow(aboutWindow);
        }
        private void OpenJsonFileClicked(object sender, EventArgs e)
        {
            try
            {
                if (fileManager.openFile())
                {
                    DisplayAlert("Success", "File opened successfully", "OK");
                    this.BindingContext = fileObject.Data;
                    MainPageLocker = true;
                    fileObject.findIndex();
                }
                else
                {
                    DisplayAlert("Error", "Error reading file. Choose another one", "OK");
                    fileObject.FileContent = null;
                    fileObject.FilePath = null;
                    MainPageLocker = false;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", "Cannot open file: " + ex.Message, "OK");
            }

        }

        private void SearchBackClicked(object sender, EventArgs e)
        {
            fileObject.Data = fileObject.Articles_buffer;
            OnPropertyChanged(nameof(fileObject.Data));
            this.BindingContext = fileObject.Data;
        }
        private void SearchClicked(object sender, EventArgs e)
        {
            var searchText = searchEntry.Text ?? string.Empty;
            var searchCriterion = searchPicker.SelectedItem?.ToString() ?? string.Empty;
            this.BindingContext = fileObject.linqSearch(searchCriterion,searchText);
            OnPropertyChanged(nameof(fileObject.Data));
            UpdateArticlesView();      
        }
    }
}
    