using System;
using System.IO;

namespace Binaries
{
    public class UserClass
    {
        #region Config directory & path

        private string dirpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\steelzero_soft\\frameR";
        private string cfgpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\steelzero_soft\\frameR\\Preference.txt";

        #endregion

        private int amountOfProps = 2; //amount of properties, changes with amount of settings manually

        /// <summary>
        /// Creates path to directory & file
        /// </summary>
        public void createPath()
        {
            //Initialie information about file and directory
            DirectoryInfo dirInf = new DirectoryInfo(dirpath);
            FileInfo fileInf = new FileInfo(cfgpath);

            #region Exist cases
            if (!dirInf.Exists) //Check if directory exists
                dirInf.Create();

            if (!fileInf.Exists) //Check if file exists
                fileInf.Create();

            #endregion

            if (fileInf.Exists && dirInf.Exists) //If fiile & directory exist, initiate launch settings
                InitiateFirstSettings(cfgpath);
        }

        public void InitiateFirstSettings(string filepath) //Writing all "true" launch settings
        {
            using (StreamWriter sw = new StreamWriter(filepath))
            {
                for (int i = 0; i < amountOfProps; i++)
                {
                    sw.WriteLine(true.ToString());
                }
            }
        }

        /// <summary>
        /// Sets preferences of user
        /// </summary>
        /// <param name="props"></param>
        public void setprops(bool[] props)
        {
            using (StreamWriter sw = new StreamWriter(cfgpath, false))
            {
                for (int i = 0; i < amountOfProps; i++)
                {
                    sw.WriteLine(props[i].ToString());
                }
            }
        }

        /// <summary>
        /// Reads preferences of user
        /// </summary>
        /// <returns>Boolean array of settings</returns>
        public bool[] Reader()
        {
            bool[] std = new bool[amountOfProps];
            using (StreamReader sr = new StreamReader(cfgpath))
            {
                for (int i = 0; i < std.Length; i++)
                {
                    std[i] = bool.Parse(sr.ReadLine());
                }
            }

            return std;
        }
    }
}
