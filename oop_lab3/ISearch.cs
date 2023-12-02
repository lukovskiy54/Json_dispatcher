using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab3
{
    internal interface ISearch
    {
        ObservableCollection<Article> Search(string searchCriterion, string searchText); 
    }
}
