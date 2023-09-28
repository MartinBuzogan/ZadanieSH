using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace ConsoleApplication1
{
    public class ConsoleUi
    {
        private static string inputMess = "Please Provide a folder or a JSON with folder information: ";
        private static string extensionMess = "Extension found in folder: ";
        private static string saveJsonMess = "\nSave to JSON ? ";
        private static string jsonPathMess = "Please provide the JSON file location : ";
        private static string jsonExtension = ".json";
        private string path;
        public ConsoleUi(){}

        public void ui()
        {
            Console.WriteLine(inputMess);
            this.path = Console.ReadLine();
            bool exitValue = path.ToLower().Equals("exit");
            if (exitValue)
                return;
            bool valid = new DirectoryInfo(path).Exists;
            bool isJson = Path.GetExtension(path).Equals(jsonExtension);
            if (!valid && !isJson) {
                Console.WriteLine(" Wrong path ");
                return;
            }

            if (isJson)
            {   //Given Json File
                string jsonString = File.ReadAllText(path);
                if (string.IsNullOrEmpty(jsonString))
                {
                    Console.WriteLine("Invalid json file");
                    return;
                }
                Console.WriteLine("Reading Json file: ");
                List<string> fromJson = new List<string>();
                fromJson = JsonConvert.DeserializeObject<List<string>>(jsonString);
                printExtension(fromJson);
                saveJson(fromJson);
            }
            else if (valid)
            {
                Folder folder1 = new Folder(path);
                Console.Write("Extension found in folder: ");
                printExtension(folder1.getUniqueExtensions());
                saveJson(folder1.getUniqueExtensions());
            }
        }

        private void saveJson(List<string> listOfExtensions)
        {
            Console.WriteLine(saveJsonMess);
            string savejson = Console.ReadLine().ToLower();
            if (savejson.Equals("y") || savejson.Equals("yes"))
            {   // Saving extensions to json file 
                Console.WriteLine(jsonPathMess);
                string jsonPath = Console.ReadLine();
                string jsonString = JsonConvert.SerializeObject(listOfExtensions);
                File.WriteAllText(jsonPath, jsonString);
            }
        }
        //printing and formatting extensions from list
        private void printExtension(List<string> folder)
        {
            int count = folder.Count;
            int number = 0;
            foreach (string file in folder)
            {
                Console.Write(" "+file);
                if(number < count-1)
                    Console.Write(",");
                number++;
            }
        }
    }
}