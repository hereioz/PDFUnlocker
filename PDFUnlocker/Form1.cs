using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace PDFUnlocker
{
    public partial class Form1 : Form
    {
        int mov;
        int movX;
        int movY;
        public static string filename;

        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }

        private void decompiler()
        {
            label2.Text = "Status: Reading PDF";
            SautinSoft.PdfFocus pdf = new SautinSoft.PdfFocus();
            pdf.OpenPdf(filename);
            label2.Text = "Status: Compile PDF to Word";
            pdf.ToWord(filename.Replace("pdf", "docx"));
            label2.Text = "Status: Compile PDF to Txt";
            pdf.ToText(filename.Replace("pdf", "txt"));
            string reader = File.ReadAllText(filename.Replace("pdf", "txt"));
            textBox1.Text = reader;
            label2.Text = "Status: Running";
        }

        private void label1_Click(object sender, EventArgs e)
        {
            bool continue_ = true;

            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Users File";
            openFileDialog1.DefaultExt = "pdf";
            openFileDialog1.Filter = "pdf files (*.pdf)|*.pdf";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.ShowDialog();

            filename = openFileDialog1.FileName;
            try
            {
                File.OpenRead(filename);
            }
            catch
            {
                continue_ = false;
            }

            if (continue_ == true)
            {
                TextBox.CheckForIllegalCrossThreadCalls = false;
                Thread decmp = new Thread(decompiler);
                decmp.Start();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var _process = Process.GetCurrentProcess();
            _process.Kill();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
