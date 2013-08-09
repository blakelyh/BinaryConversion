// Author: Hunter J. Blakely
// DrgDrpFileCnvrtBinary Copyright 7/14/2013 @5:22pm 
// Enjoy...

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace DrgDrpFileCnvrtBinary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        } // end Form1



        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            String[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string file in files)
                if (listBox1.Items.Contains(file) == false)
                    listBox1.Items.Add(file);
        } // end listBox1_DragDrop



        private void clearAll(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                listBox1.Items.Clear();
            if (radioButton2.Checked)
                for (int i = listBox1.SelectedIndices.Count - 1; i >= 0; i--)
                {
                    listBox1.Items.RemoveAt(listBox1.SelectedIndices[i]);
                } // fi
        } // end clearAll



        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        } // end listBox1_DragEnter



        private void button2_Click(object sender, EventArgs e)
        {
            string outputFolder = textBox1.Text;
            try { Directory.CreateDirectory(outputFolder); }
            catch (Exception ex) { Console.WriteLine("\nMessage ---\n{0}", ex.Message); }

            if (radioButton1.Checked)
                procAll(outputFolder);      
            else if (radioButton2.Checked)
                procSelect(outputFolder);
        } // end button2_Click



        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        } // end textBox1_DragEnter



        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            String file = (string)e.Data.GetData(DataFormats.FileDrop, false);
            textBox1.Text = file;
        } // end textBox1_DragDrop

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        } // end textBox1_TextChanged



        private void Form1_Load(object sender, EventArgs e)
        {
            string user = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string[] split = user.Split('\\');
            user = split[split.Length - 1];
            AcceptButton = button2;
            radioButton1.Checked = true;
            checkBox1.Checked = true;
            textBox1.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"\\test";
        } // end Form1_Load



        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        } // end button3_Click



        private void procAll(string folder)
        {
            ListBox.ObjectCollection a = listBox1.Items;
            try
            {
                foreach (string path in a)
                {
                    write(folder, path);
                } // end foreach
            }
            catch (Exception ex) { Console.WriteLine("\nMessage ---\n{0}", ex.Message); }
        } // end procAll

        

        private void procSelect(string folder)
        {
            ListBox.SelectedObjectCollection b = listBox1.SelectedItems;
            try
            {
                foreach (string path in b)
                {
                    write(folder, path);
                }// end for
            }
            catch (Exception ex) { Console.WriteLine("\nMessage ---\n{0}", ex.Message); }
        } //end procSelect




        public void write(string folder, string path)
        {
            string oFile = Path.Combine(folder, Path.GetFileName(path));
            string bN = "";         // binary output
            string hN = "";         // hexedecimal output
            if (checkBox1.Checked)
            {
                bN = oFile + ".binary";
                if (!File.Exists(bN))
                {
                    byte[] fileBytes = File.ReadAllBytes(path);
                    StringBuilder sb = new StringBuilder();

                    foreach (byte b in fileBytes)
                    {
                        sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
                    }
                    File.WriteAllText(bN, sb.ToString());
                }// fi
            }// fi
            if (checkBox2.Checked)
            {
                hN = oFile + ".hexadecimal";
                if (!File.Exists(hN))
                {
                    byte[] fileBytes = File.ReadAllBytes(path);
                    string hex = BitConverter.ToString(fileBytes);
                    hex = hex.Replace("-", "");
                    File.WriteAllText(hN, hex.ToString());
                }// fi
            }// fi
        } // end write


    }// End Class Form1
} // End Namespace
