using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace oop_lab3
{
    class FileSerializer
    {
        public ObservableCollection<Article> Deserialize(string json)
        {
            return JsonSerializer.Deserialize<ObservableCollection<Article>>(json);
        }

        public string Serialize(ObservableCollection<Article> articles)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            return JsonSerializer.Serialize(articles,options);
        }
    }
}
