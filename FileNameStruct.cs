using System.IO;

namespace MauiApp1
{
    internal struct FileNameStruct
    {
        string file_name = "";

        public FileNameStruct() { }

        public string Extension { get; set; } = "";
        public string Folder { get; set; } = "";
        public string Title
        {
            get => file_name;
            set { file_name = ReplaceInvalidChars(value); }
        }
       
        public string Name
        {
            get => Title + "." + Extension;
        }
        public string FullPath
        {
            get => Path.Combine(Folder, Name);
        }
        public string FullPathWithoutExt
        {
            get => Path.Combine(Folder, Title);
        }
        static string ReplaceInvalidChars(string filename)
        {
            return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        }
    }
}
