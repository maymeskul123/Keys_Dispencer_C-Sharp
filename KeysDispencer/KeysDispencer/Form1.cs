using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace KeysDispencer
{
    public partial class Form1 : Form
    {
        string currentDirectory;
        string unusedDirectory;
        string[] filesUnused;
        string[] filesNameUnused;       
        CodeManager codeManager;
        public Form1()
        {            
            InitializeComponent();
            InitializeComboBox();
        }        

        private void InitializeComboBox()
        {                
            currentDirectory = Directory.GetCurrentDirectory();                
            unusedDirectory = currentDirectory.Insert(currentDirectory.Length, "\\unused");
            if (Directory.Exists(unusedDirectory))
            {                
                filesUnused = Directory.GetFiles(unusedDirectory);                
                filesNameUnused = new string[filesUnused.Count()];
                int count = 0;
                foreach(string s in filesUnused)
                {
                    string fullName = Path.GetFileName(s);
                    string fileName = Path.GetFileNameWithoutExtension(fullName);                    
                    filesNameUnused[count] = fileName;
                    count++;                    
                }
                codeManager = new CodeManager(currentDirectory, filesUnused);                
            }
            else
            {
                  MessageBox.Show("Directory Unused not found");
            }            
            comboBox1.DataSource = filesNameUnused;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            codeManager.getCode(comboBox1.SelectedItem.ToString());
            showCode(comboBox1.SelectedItem.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {            
            showCode(comboBox1.SelectedItem.ToString());
        }
        
        private void showCode(string fileName)
        {
            string path = "";
            string code = "";            
            foreach (string s in filesUnused)
            {
                if (s.IndexOf(fileName) > -1)
                {
                    path = s;
                    break;
                }
            }           
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    code = sr.ReadLine();                    
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            if (code == null)
            {
                MessageBox.Show("File Empty!");
            }
            else
            {
                if (code.Length > 2)
                {
                    richTextBox1.Text = codeManager.normalString(code);
                }
                else
                {
                    MessageBox.Show("File Empty!");
                }
            }
        }
    }
}