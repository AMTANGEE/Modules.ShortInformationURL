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
            edtURL.Text = AMTANGEE.SDK.Settings.Global["AMTANGEE.MODULES.SHORTINFORMATIONURL"]["URL"];
            edtText.Text = AMTANGEE.SDK.Settings.Global["AMTANGEE.MODULES.SHORTINFORMATIONURL"]["TEXT"];
        }


        public void SaveData()
        {
            AMTANGEE.SDK.Settings.Global["AMTANGEE.MODULES.SHORTINFORMATIONURL"]["URL"] = edtURL.Text;
            AMTANGEE.SDK.Settings.Global["AMTANGEE.MODULES.SHORTINFORMATIONURL"]["TEXT"] = edtText.Text;
        }
    }
}
