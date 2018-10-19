using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AMTANGEE.Modules.ShortInformationUrl
{
    class SettingsClass
    {
        static string Settings_Name = "AMTANGEE.MODULES.SHORTINFORMATIONURL";

        private static string _URL = GetSettingsString("URL");
        public static string URL
        {
            get
            {
                if (_URL == null)
                    _URL = "";
                return _URL;
            }
            set
            {
                _URL = value;
                SetSettings("URL", value);
            }
        }
        private static string _TEXT = GetSettingsString("TEXT");
        public static string TEXT
        {
            get
            {
                if (_TEXT == null)
                    _TEXT = "";
                return _TEXT;
            }
            set
            {
                _TEXT = value;
                SetSettings("TEXT", value);
            }
        }
        private static Image _DisplayImage = GetSettingsImage("IMAGE");
        public static Image IMAGE
        {
            get
            {
                if (_DisplayImage == null)
                    _DisplayImage = AMTANGEE.SDK.Resources.Images[SDK.Resources.ImageIndexes.Website];
                return _DisplayImage;
            }
            set
            {
                _DisplayImage = value;
                SetSettings("IMAGE", value);
            }
        }
        private static void SetSettings(string identifier, Image value)
        {
            try
            {
                AMTANGEE.SDK.Settings.Global[Settings_Name][identifier] = ToText(value).ToString();
            }
            catch
            {
            }
        }
        private static void SetSettings(string identifier, string value)
        {
            try
            {
                AMTANGEE.SDK.Settings.Global[Settings_Name][identifier] = value.ToString();
            }
            catch
            {
            }
        }
        private static string GetSettingsString(string identifier)
        {
            try
            {
                return AMTANGEE.SDK.Settings.Global[Settings_Name][identifier];
            }
            catch
            {
                return null;
            }
        }
        private static Image GetSettingsImage(string identifier)
        {
            try
            {
                return ToImage(Convert.ToString(AMTANGEE.SDK.Settings.Global[Settings_Name][identifier]));
            }
            catch
            {
                return null;
            }
        }
        private static string ToText(Image Image)
        {
            if (Image != null)
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            else
            {
                return null;
            }
        }
        private static Image ToImage(string newData)
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
    }
}
