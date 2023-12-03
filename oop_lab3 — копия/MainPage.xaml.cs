﻿using Microsoft.Maui.Controls;
using System.Text.Json;
using System;
using System.IO;
using System.Threading.Tasks;
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

        private bool mainPageLocker = false;

        public MainPage()
        {
            InitializeComponent();

            fileManager = new FileManager();
            fileObject = FileObject.GetInstance();

            UpdateArticlesView();
        }

        private void SaveClicked(object sender, EventArgs e)
        {
            if (mainPageLocker)
            {
                fileManager.SaveFile();
            }
        }

        private void OpenWindow(Page view)
        {
            Window editWindow = new Window(view);
            Application.Current.OpenWindow(editWindow);
        }

        private void EditClicked(object sender, EventArgs e)
        {
            UpdateArticlesView();
            if (mainPageLocker)
            {
                OpenWindow(new EditView());
                UpdateArticlesView();
            }
            else
            {
                DisplayAlert("Error", "Cant edit empty file", "OK");
            }
        }

        private void DeleteClicked(object sender, EventArgs e)
        {
            if (mainPageLocker)
            {
                fileObject.DeleteArticle();
                UpdateArticlesView();
            }
            else
            {
                DisplayAlert("Error", "No elements left", "OK");
            }
        }

        private void UpdateArticlesView()
        {
            this.BindingContext = fileObject.Data;
            if (fileObject.Data.Count > 0)
            {
                OnPropertyChanged(nameof(fileObject.Data));
                mainPageLocker = true;
            }
            else
            {
                mainPageLocker = false;
            }
        }

        private void AddClicked(object sender, EventArgs e)
        {
            if (mainPageLocker || this.fileObject.IsOpened())
            {
                OpenWindow(new AddView());
                UpdateArticlesView();
            }
            else
            {
                DisplayAlert("Error", "Open file first", "OK");
            }
        }

        private void AboutClicked(object sender, EventArgs e)
        {
            OpenWindow(new AboutView());
        }

        private void OpenJsonFileClicked(object sender, EventArgs e)
        {
            try
            {
                if (fileManager.OpenFile("C:\\Users\\Максим\\Downloads\\example.json"))
                {
                    DisplayAlert("Success", "File opened successfully", "OK");
                    UpdateArticlesView();
                    mainPageLocker = true;
                }
                else
                {
                    DisplayAlert("Error", "Error reading file. Choose another one", "OK");
                    mainPageLocker = false;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", "Cannot open file: " + ex.Message, "OK");
            }
        }

        private void SearchBackClicked(object sender, EventArgs e)
        {
            fileObject.UpdateDataToBuffer();
            UpdateArticlesView();
        }

        private SearchCriteria GetSearchCriterias()
        {
            return new SearchCriteria
            {
                SearchText = searchEntry.Text ?? string.Empty,
                SearchCriterion = searchPicker.SelectedItem?.ToString() ?? string.Empty
            };
        }

        private void SearchClicked(object sender, EventArgs e)
        {
            var criterias = GetSearchCriterias();
            var searchStrategy = new LinqSearch();
            this.BindingContext = fileObject.Search(searchStrategy, criterias.SearchCriterion, criterias.SearchText);
        }
    }
}
