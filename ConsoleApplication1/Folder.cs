using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApplication1
{
    public class Folder 
    {
        private string FolderPath;
        public string FolderName { get; private set; }
        public List<Files> FolderFiles { get; private set;}
        public List<Folder> SubFolders { get; private set; }

        public Folder(string folderPath)
        {
            this.FolderPath = folderPath;
            this.FolderName = Path.GetFileName(folderPath);
            this.FolderFiles = new List<Files>();
            this.SubFolders = new List<Folder>();
            getAllFiles();
        }
        
        public Folder() {}

        public List<Files> getAllFiles() { // getting all files from main folder by recursion
            if (FolderPath == null) return null; 
            foreach (string filePaths in Directory.GetFiles((FolderPath))){
                FolderFiles.Add(new Files(filePaths));
            }

            foreach (string SubFolderPath in Directory.GetDirectories(FolderPath)){
                SubFolders.Add(new Folder(SubFolderPath));
            }

            foreach (Folder folder in SubFolders)
            {
                FolderFiles.AddRange(folder.getAllFiles());
            }

            return FolderFiles;
        }   
        
        public List<string> getUniqueExtensions(){
            return FolderFiles.Select(files => files.FileExtension).Distinct().ToList();
        }
    }
}