namespace Common
{
    using System.IO;
    using System.Linq;

    public static class FileUtil
    {
        public static int[] GetIntArray(string filePath)
        {
            return File.ReadAllLines(filePath).Select(l => int.Parse(l)).ToArray();
        }
    }
}
