using System.IO;

namespace ConsoleApplication1
{
    public class Files
    {
        private string FilePath;
        public string FileName { get; private set; }
        
        public string FileExtension { get; private set; }
        public Files(string filePath)
        {
            this.FilePath = filePath;
            this.FileName = Path.GetFileName(filePath);
            this.FileExtension = Path.GetExtension(filePath);
        }
        public Files(){}
    }
}