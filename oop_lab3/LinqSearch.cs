using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab3
{
    class LinqSearch: ISearch
    {
        public ObservableCollection<Article> Search(string searchCriterion, string searchText) 
        {
            var filteredArticles = new ObservableCollection<Article>();
            var file = FileObject.GetInstance();
            switch (searchCriterion)
            {
                case "Name":
                    filteredArticles = new ObservableCollection<Article>(
                        (from article_object in file.Data
                         where (article_object?.Title?.Contains(searchText, StringComparison.OrdinalIgnoreCase) == true)
                         select article_object).ToList());
                    break;
                case "Author":
                    filteredArticles = new ObservableCollection<Article>(
                        (from article_object in file.Data
                         where (article_object?.Author?.Contains(searchText, StringComparison.OrdinalIgnoreCase) == true)
                         select article_object).ToList());
                    break;
                case "Count of readers":
                    if (int.TryParse(searchText, out int readersCount))
                    {
                        filteredArticles = new ObservableCollection<Article>(
                            (from article_object in file.Data
                             where (article_object?.Comments?.Count == readersCount)
                             select article_object).ToList());
                    }
                    break;
            }
            return filteredArticles;
        }
    }
}
