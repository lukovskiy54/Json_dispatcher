
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace oop_lab3
{
    class FileManager
    {
        public FileManager() {  }

        public bool DeserializeFile()
        {
            try
            {
                var file = FileObject.GetInstance();

                file.Data = JsonSerializer.Deserialize<ObservableCollection<Article>>(file.FileContent);

                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }

        public void saveFile(string filePath)
        {
            try
            {
                var file = FileObject.GetInstance();
                var json = JsonSerializer.Serialize(file.Data);
                File.WriteAllText(file.FilePath, json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving file: {ex.Message}");
            }
        }
        
        public bool openFile()
        {
            var file = FileObject.GetInstance();
            file.FileContent = File.ReadAllText("C:\\Users\\Максим\\Downloads\\example.json");
            file.FilePath = "C:\\Users\\Максим\\Downloads\\example.json";
            return this.DeserializeFile();
        }

    }
}
