namespace _04
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Example()
        {
            Assert.Pass();
        }
    }

    public class Calc
    {
        public void FindOverlapping(string filePath)
        {
            var lines = Utilities.FileHelper.GetFileAsRows(filePath);  
            foreach(var line in lines)
            {
                var parts = line.Split(',');

            }
        }
    }
}