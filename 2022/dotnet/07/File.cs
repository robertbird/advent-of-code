namespace _07
{
    public partial class DirectoryManager
    {
        /* DESIGN PLANNING NOTES
         * 
         * Object types
         *  Q, Single class/struct or have folder/file as diff objects
         *  A. Different types (because only folders need to go on stack, maybe have a collection of files)
         * 
         * classes:
         * Files - name/size/parent-folder
         * Folders - name/total-size/parent-folder/child-folders/files
         * 
         * structures
         * stack for traversing while parsing

         * Lines
         * 6 types of lines:
            $ "cd /" -> create root object - create item on stack, store link to it
            $ "cd abc" -> look for "abc" in current folder on stack. add to stack, return item
            $ "ls" -> do nothing
            "dir a" -> create dir obj "a", add to collection of current item on the stack, set parent, return it
            "14848514 b.txt" -> create file item "b.txt" with size, add to current item on stack. 
            "$ cd .." -> pop stack
        use cases:
         1. find all folders where total size < x
            recursive method (depth first) in folder class to sum child folders and files and add itself to list if less than x
        */

        public class File
        {
            public string Name { get; set; } = "";
            public Folder InFolder { get; set; } = new Folder();
            public int Size { get; set; }

            public override string ToString()
            {
                return $"- {Name} (file, size={Size})\r\n";
            }
        }

    }
}
