using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab3
{
    internal class SearchCriteria
    {
        public string SearchText { get; set; }
        public string SearchCriterion { get; set; }

        public MainPage MainPage
        {
            get => default;
            set
            {
            }
        }
    }

}
