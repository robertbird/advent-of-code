using Microsoft.VisualStudio.TestPlatform.Utilities.Helpers;
using System.Diagnostics;
using static _07.DirectoryManager;

namespace _07
{
    public class Tests
    {
        [OneTimeSetUp]
        public void StartTest()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
        }

        [OneTimeTearDown]
        public void EndTest()
        {
            Trace.Flush();
        }

        [Test]
        public void Given_example()
        {
            string filePath = "TestFiles/given-example-terminal.txt";
            var manager = new DirectoryManager();
            manager.ParseInput(filePath);
            var result = manager.ToString();
            Trace.WriteLine("FOLDERS:");
            Trace.WriteLine(result);
            var expected = Utilities.FileHelper.GetFileContents("TestFiles/given-example-print.txt");
            //Assert.That(result, Is.EqualTo(expected));
            var folders  = manager.FindAllFoldersWithinSize((s) => s < 100000);
            Assert.That(folders.Sum(f => f.SetFolderSize()), Is.EqualTo(95437));
        }

        [Test]
        public void Puzzle_input()
        {
            string filePath = "TestFiles/puzzle-input.txt";
            var manager = new DirectoryManager();
            manager.ParseInput(filePath);
            var result = manager.ToString();
            //Trace.WriteLine("FOLDERS:");
            //Trace.WriteLine(result);
            var folders = manager.FindAllFoldersWithinSize((s) => s < 100000);
            Assert.That(folders.Sum(f => f.SetFolderSize()), Is.EqualTo(1989474));
            
            
            var firstFolder = manager.FindAllFoldersWithinSize((s) => s > 1072511)
                .OrderBy(f => f.TotalSize)
            .Take(10)
            .ToList();

            Trace.WriteLine($"size: {firstFolder[0].TotalSize}");

        }




    }
}