﻿
namespace oop_lab3
{
    class FileWriter
    {
        public void WriteFile(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
        }
    }
}
