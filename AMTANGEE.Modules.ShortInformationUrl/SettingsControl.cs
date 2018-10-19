using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace AMTANGEE.Modules.ShortInformationUrl
{
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            edtURL.Text = SettingsClass.URL;
            edtText.Text = SettingsClass.TEXT;
            btnImage.Image = SettingsClass.IMAGE;
            btnResetImage.Image = AMTANGEE.SDK.Resources.Images[SDK.Resources.ImageIndexes.Delete];
        }
        public void SaveData()
        {
            SettingsClass.URL = edtURL.Text;
            SettingsClass.TEXT = edtText.Text;
            SettingsClass.IMAGE = btnImage.Image;
        }
        private static string ImageToText(Image Image)
        {
            if (Image != null)
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    Image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    byte[] imageBytes = ms.ToArray();
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            else
            {
                return null;
            }
        }
        private static Image TextToImage(string newData)
        {
            Image _return = AMTANGEE.SDK.Resources.Images[SDK.Resources.ImageIndexes.Website];
            try
            {
                if (newData.Length > 0)
                {
                    byte[] byteumrechner = Convert.FromBase64String(newData);
                    if (byteumrechner != null && byteumrechner.Length > 0)
                    {
                        var _with2 = new System.Drawing.ImageConverter();
                        _return = (Image)_with2.ConvertFrom(byteumrechner);
                    }
                }
            }
            catch
            {
                _return = null;
            }
            return _return;
        }
        private void btnImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string GraphicFileFilter = "Alle Bilddateien|*.jpg;*.jpeg;*.jpe;*.jfif;*.bmp;*.gif;*.png;*.ico";
            string AllFileFilter = "Alle Dateien|*.*";
            openFileDialog.Title = "Bild öffnen";
            openFileDialog.Multiselect = false;
            openFileDialog.CheckFileExists = true;
            openFileDialog.Filter = GraphicFileFilter + "|" + AllFileFilter;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog.FilterIndex = 1;
            openFileDialog.FileName = "";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                try
                {
                    if (btnImage.Image != null)
                    {
                        btnImage.Image = AMTANGEE.SDK.Resources.Images[SDK.Resources.ImageIndexes.Website];
                    }
                    btnImage.Image= Image.FromFile(fileName);
                }
                catch (Exception ex)
                {
                    string messageText = String.Format("Die Datei '{0}' konnte nicht geladen werden:{1}{2}", fileName, Environment.NewLine, ex.Message);
                    MessageBox.Show(this, messageText, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void btnResetImage_Click(object sender, EventArgs e)
        {
            btnImage.Image = AMTANGEE.SDK.Resources.Images[SDK.Resources.ImageIndexes.Website];
        }
    }
}
