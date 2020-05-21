﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security;
using System.Diagnostics;
using System.IO;


namespace XmlReader
{
    public partial class Form1 : Form
    {
        private OpenFileDialog openFileDialog1;

        public Form1()
        {
            InitializeComponent();
            openFileDialog1 = new OpenFileDialog()
            {
                FileName = "Виберіть Xml файл",
                Filter = "Xml files (*.xml)|*.xml",
                Title = "Open xlm file"
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ShowSchemaButton_Click(object sender, EventArgs e)
        {
            System.IO.StringWriter swXML = new System.IO.StringWriter();
            XmlDataSet.WriteXmlSchema(swXML);
            textBox1.Text = swXML.ToString();
        }

        private void DownloadXmlButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = openFileDialog1.FileName;
                    using (Stream str = openFileDialog1.OpenFile())
                    {
                        
                        XmlDataSet.ReadXml(filePath);

                        dataGridView1.DataSource = XmlDataSet;
                        dataGridView1.DataMember = "Document";
                    }
                }
                catch (ApplicationException ex)
                {
                    MessageBox.Show($"Error!.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // creating Excel Application  
                Microsoft.Office.Interop.Excel._Application ExcelFile = new Microsoft.Office.Interop.Excel.Application();
                // creating new WorkBook within Excel application  
                Microsoft.Office.Interop.Excel._Workbook workbook = ExcelFile.Workbooks.Add(Type.Missing);
                // creating new Excelsheet in workbook  
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

                // store its reference to worksheet  
                worksheet = workbook.ActiveSheet;

                // storing header part in Excel  
                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                }
                // storing Each row and column value to excel sheet  
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
                // see the excel sheet behind the program 
                ExcelFile.Columns.AutoFit();
                ExcelFile.Visible = true;
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show($"Error!.\n\nError message: {ex.Message}\n\n" +
                $"Details:\n\n{ex.StackTrace}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            const string message ="Данна функція в розробці";
            const string caption = "Comming soon";
            var result = MessageBox.Show(message, caption,MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
