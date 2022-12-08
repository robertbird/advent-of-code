namespace _07
{
    public partial class DirectoryManager
    {
        public class Folder
        {
            // Folders - name/total-size/parent-folder/child-folders/files
            public string Name { get; set; } = "";
            public Folder InFolder { get; set; }
            public int Depth { get; set; }

            public int TotalSize { get; set; }
            public List<Folder> Folders { get; set; } = new List<Folder>(); 
            public List<File> Files { get; set; } = new List<File>();

            public override string ToString()
            {
                var str = "".PadLeft(Depth*2);
                str += $"- {Name} (dir, size: {TotalSize})\r\n";
                foreach(var folder in Folders)
                {
                    str += folder.ToString();
                }
                foreach(var file in Files)
                {
                    str += "".PadLeft((Depth + 1) * 2) + file.ToString();
                }
                return str;
            }

            public int SetFolderSize()
            {
                foreach(var folder in Folders)
                {
                    folder.SetFolderSize();
                }
                var foldersTotalSize = Folders.Sum(f => f.TotalSize);
                var filesTotalSize = Files.Sum(f => f.Size);
                TotalSize = foldersTotalSize + filesTotalSize;
                return TotalSize;
            }

            public void FindAllFoldersWithinSize(Func<int, bool> folderCheck, List<Folder> selectedFolders)
            {
                // 1.find all folders where total size < x
                // recursive method(depth first) in folder class to sum child folders and files and add itself to list if less than x
                foreach(var folder in Folders)
                {
                    folder.FindAllFoldersWithinSize(folderCheck, selectedFolders);    
                }
                if (folderCheck(TotalSize)) //TotalSize < sizeLimit)
                {
                    selectedFolders.Add(this);
                }
            }
        }

    }
}
