using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace FPSMass
{
    public partial class FormMain : Form
    {
        static string path = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        string hndb = Path.Combine(path, "data", "handling.cfg.bak");
        string objb = Path.Combine(path, "data", "object.dat.bak");
        string hnd = Path.Combine(path, "data", "handling.cfg");
        string obj = Path.Combine(path, "data", "object.dat");
        List<string> cacheHnd = new List<string>();
        List<string> cacheObj = new List<string>();

        public FormMain()
        {
            InitializeComponent();
            if (File.Exists(hndb) && File.Exists(objb))
            {
                cacheHnd.AddRange(File.ReadAllLines(hndb));
                cacheObj.AddRange(File.ReadAllLines(objb));
            }
            else
            {
                button1.Enabled = false;
                numericUpDown1.Enabled = false;
            }
        }

        void button1_Click(object sender, EventArgs e)
        {
            int count = cacheHnd.Count;
            for (int i = 0; i < count; i++)
            {
                if (!cacheHnd[i].StartsWith(";"))
                {
                    string[] settings = cacheHnd[i].Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                    if (settings.Length == 33)
                    {
                        settings[1] = (Int32.Parse(settings[1].Remove(settings[1].IndexOf("."))) * numericUpDown1.Value).ToString().Replace(",", ".");
                        cacheHnd[i] = String.Join("\t", settings);
                    }
                }
            }
            File.WriteAllLines(hnd, cacheHnd);
            count = cacheObj.Count;
            for (int i = 0; i < count; i++)
            {
                if (!cacheObj[i].StartsWith(";"))
                {
                    string[] settings = cacheObj[i].Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                    if (settings.Length == 11 && Int32.Parse(settings[1].Remove(settings[1].IndexOf("."))) < 99999 && Int32.Parse(settings[2].Remove(settings[2].IndexOf("."))) < 99999 && Int32.Parse(settings[6].Remove(settings[6].IndexOf("."))) > 0)
                    {
                        settings[1] = (Int32.Parse(settings[1].Remove(settings[1].IndexOf("."))) * numericUpDown1.Value).ToString().Replace(",", ".") + ",";
                        settings[2] = (Int32.Parse(settings[2].Remove(settings[2].IndexOf("."))) * numericUpDown1.Value).ToString().Replace(",", ".");
                        settings[6] = (Int32.Parse(settings[6].Remove(settings[6].IndexOf("."))) * numericUpDown1.Value).ToString().Replace(",", ".") + ",";
                        cacheObj[i] = String.Join("\t", settings);
                    }
                }
            }
            File.WriteAllLines(obj, cacheObj);
        }
    }
}
