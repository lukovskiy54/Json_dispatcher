using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab3
{
    class LinqSearch: ISearch
    {
        internal ISearch ISearch
        {
            get => default;
            set
            {
            }
        }

        public ObservableCollection<Article> Search(string searchCriterion, string searchText) 
        {
            var filteredArticles = new ObservableCollection<Article>();
            var file = FileObject.GetInstance();
            Debug.WriteLine(searchCriterion,searchText);
            Debug.WriteLine(String.IsNullOrEmpty(searchText));
            switch (searchCriterion)
            {
                case "Name":
                    filteredArticles = new ObservableCollection<Article>(
                        (from articleObject in file.Data
                         where String.IsNullOrEmpty(searchText)
                               ? articleObject?.Title == null
                               : articleObject?.Title?.Contains(searchText, StringComparison.OrdinalIgnoreCase) == true
                         select articleObject).ToList());
                    break;
                case "Author":
                    filteredArticles = new ObservableCollection<Article>(
                        (from articleObject in file.Data
                         where String.IsNullOrEmpty(searchText)
                               ? articleObject?.Author == null
                               : articleObject?.Author?.Equals(searchText, StringComparison.OrdinalIgnoreCase) == true
                         select articleObject).ToList());
                    break;
                case "Count of comments":
                    if (int.TryParse(searchText, out int readersCount))
                    {
                        filteredArticles = new ObservableCollection<Article>(
                            (from articleObject in file.Data
                             where articleObject?.Comments?.Count == readersCount
                             select articleObject).ToList());
                    }
                    break;
            }
            Debug.WriteLine("filtered:");

            return filteredArticles;
        }
    }
}
