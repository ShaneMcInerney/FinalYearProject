using System.IO;

namespace FYP_Droid.Business
{
    public class FileHelperAndroid
    {
        /// <summary>
        /// Gets personal file path
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public string GetLocalFilePath(string filename)
        {
            //gettng path
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            //return path and file name
            return Path.Combine(path, filename);
        }
    }
}

