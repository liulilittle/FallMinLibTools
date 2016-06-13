using System;
using System.IO;
using System.Windows.Forms;

namespace FallMinLibTools
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnComplie_Click(object sender, EventArgs args)
        {
            try
            {
                string strLibFileName = txtLibFileName.Text;
                string strDefLibValue = DefFactory.Create(strLibFileName);
                if (File.Exists(DefFactory.Compile(strDefLibValue, strLibFileName)) != true)
                    MessageBox.Show("Dll to Lib, has not been properly compiled.");
                else
                    MessageBox.Show("Dll to Lib, has been properly compiled.");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void txtLibFileName_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void txtLibFileName_DragDrop(object sender, DragEventArgs e)
        {
            object objFileDrop = e.Data.GetData(DataFormats.FileDrop);
            if(objFileDrop != null && objFileDrop is string[])
                txtLibFileName.Text = (objFileDrop as string[])[0];
        }
    }
}
