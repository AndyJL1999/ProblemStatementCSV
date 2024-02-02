using Microsoft.VisualBasic.FileIO;
using System;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Windows.Forms;

namespace ProblemStatementCSV
{
    public partial class Problem : Form
    {
        public Problem()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            string csv = GetCSVFromFileExplorer();

            // Check if a csv file was returned
            if(csv != null)
            {
                ReadCSV(csv);

                statusText.ForeColor = Color.LimeGreen;
                statusText.Text = "Mission Accomplished!";
            }
            else
            {
                statusText.ForeColor = Color.Red;
                statusText.Text = "No CVS file in sight.";
            }
        }

        private void ReadCSV(string csvFilePath)
        {
            BigInteger total = 0;

            using (TextFieldParser parser = new TextFieldParser(csvFilePath))
            {
                // Set the delimiter of your csv file
                parser.SetDelimiters("'");

                while (parser.EndOfData == false)
                {
                    string[] fields = parser.ReadFields();

                    BigInteger.TryParse(fields[1], out BigInteger value);
                    total += value;
                }
            }
            
            // Display last 10 digits of sum and get absolute value to combat negtive values
            textField.Text = BigInteger.Abs(total % 10000000000).ToString();
        }


        private string GetCSVFromFileExplorer()
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "CSV files (*.csv)|*.csv";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Only return CSV files or else... null
            if (dialog.ShowDialog() == DialogResult.OK && Path.GetExtension(dialog.FileName) == ".csv")
            {
                return dialog.FileName;
            }
            else
            {
                return null;
            }
        }
    }
}
