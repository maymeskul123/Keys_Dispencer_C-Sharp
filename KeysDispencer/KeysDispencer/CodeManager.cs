using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace KeysDispencer
{
    class CodeManager
    {
        string currentDirectory;       
        string usedDir;
        string[] fUnused; 
        
        public CodeManager(string curDir, string[] filesUnused)
        {
            this.currentDirectory = curDir;            
            this.usedDir = currentDirectory.Insert(currentDirectory.Length, "\\used");
            this.fUnused = filesUnused;
        }

        public void getCode(string fileName)
        {
            string delCode = "";
            string path = "";            
            foreach(string s in fUnused)
            {
                if (s.IndexOf(fileName) > -1)
                {
                    path = s;                    
                    break;
                }
            }
            string pathUsed = usedDir.Insert(usedDir.Length, "\\" + fileName + "_used.txt");            
            try
            {
                string[] readText = File.ReadAllLines(path);
                var textList = readText.ToList();
                delCode = textList.ElementAt(0);
                textList.RemoveAt(0);
                string[] text = textList.ToArray();
                File.WriteAllLines(path, text);
                delCode = normalString(delCode);                
                string fullString = delCode + "-" + Environment.UserName + "-" + DateTime.Now + "\n";
                if (!File.Exists(pathUsed))
                {
                    File.WriteAllText(pathUsed, fullString);
                }
                if (!checkUsedFile(delCode, pathUsed))
                {
                    File.AppendAllText(pathUsed, fullString);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            string normalCode = normalString(delCode);
        }
        public string normalString(string code)
        {
            var replacement = code.Replace("-", " ");           
            return replacement;
        }

        public bool checkUsedFile (string code, string pathUsed)
        {
            string[] readText = File.ReadAllLines(pathUsed);
            bool foundKey = false;
            foreach(string s in readText)
            {
                if (s.IndexOf(code) > -1)
                {
                    foundKey = true;
                    break;
                }
            }
            return foundKey;
        }
    }
}
