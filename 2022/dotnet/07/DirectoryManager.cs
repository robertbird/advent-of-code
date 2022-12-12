using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using static _07.DirectoryManager;

namespace _07
{
    public partial class DirectoryManager
    {

        public Stack<Folder> Folders { get; private set; } = new Stack<Folder>();

        public Folder RootFolder { get; private set; } = new Folder();

        public void ParseInput(string filePath)
        {
            var lines = FileHelper.GetFileAsRows(filePath);
            foreach(var line in lines)
            {
                ProcessLine(line);
            }
            RootFolder.SetFolderSize();
        }

        public override string ToString()
        {
            return RootFolder.ToString();
        }

        public void ProcessLine(string line)
        {
           /* 6 types of lines:
                $ "cd /" -> create root object - create item on stack, store link to it
                $ "cd abc" -> look for "abc" in current folder on stack. add to stack, return item
                $ "ls" -> do nothing
                "dir a" -> create dir obj "a", add to collection of current item on the stack, set parent, return it
                "14848514 b.txt" -> create file item "b.txt" with size, add to current item on stack. 
                "$ cd .." -> pop stack
            */
            if(line == "$ cd /") // initialise
            {
                RootFolder = new Folder
                {
                    Name = "/",
                    Depth = 0
                };
                Folders.Push(RootFolder);
                return;
            }
            else if(Folders.Count == 0)
            {
                throw new InvalidOperationException("Failed to initialise");
            }

            var currentFolder = Folders.Peek();
            
            if (Char.IsNumber(line[0])) // file
            {
                var lineParts = line.Split(" ");
                currentFolder.Files.Add(new File
                {
                    Name = lineParts[1],
                    Size = int.Parse(lineParts[0]),
                    InFolder = currentFolder
                });
            }
            else if (line.StartsWith("dir "))
            {
                line = line.Replace("dir ", "");
                currentFolder.Folders.Add(new Folder
                {
                    Name = line,
                    InFolder = currentFolder,
                    Depth = currentFolder.Depth + 1
                });
            }
            else if (line == "$ cd ..")
            {
                Folders.Pop();
            }
            else if (line.StartsWith("$ cd ")) // Navigate into folder
            {
                line = line.Replace("$ cd ", "");
                var folder = currentFolder.Folders.Find(x => x.Name == line);
                if(folder == null)
                {
                    throw new InvalidOperationException("Changing into folder that doesn't exist");
                }
                Folders.Push(folder);
            }
            else if (line == "$ ls") // list contents
            {
                // Do nothing
            }
        }


        public List<Folder> FindAllFoldersWithinSize(Func<int, bool> folderCheck) 
        {
            var selectedFolders = new List<Folder>();
            //1.find all folders where total size < x
            // recursive method(depth first) in folder class to sum child folders and files and add itself to list if less than x
            RootFolder.FindAllFoldersWithinSize(folderCheck, selectedFolders);
            return selectedFolders;
        }

    }
}
