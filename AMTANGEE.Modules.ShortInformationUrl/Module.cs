using System;
using System.Collections.Generic;
using System.Text;

namespace AMTANGEE.Modules.ShortInformationUrl
{
    public class Module:AMTANGEE.Interfaces.IModule, AMTANGEE.Interfaces.Contacts.IContactShortInformationPlugin, AMTANGEE.Interfaces.ISettingsFormPlugin
    {
        public string Caption
        {
            get { return ""; }
        }

        public bool CheckRequirements()
        {
            return true;
        }

        public System.Drawing.Image Icon
        {
            get { return null; }
        }

        public System.Windows.Forms.UserControl ModuleControl
        {
            get { return null; }
        }

        public string Name
        {
            get { return "Kurzinformationen-Url"; }
        }

        public bool NeedsLicense
        {
            get { return false; }
        }

        public void ProcessEvent(string theEvent)
        {
        }

        public string StatusBarText
        {
            get { return ""; }
        }

        public void UpdateDatabase()
        {
        }

        public bool Visible
        {
            get { return false; }
        }

        List<Interfaces.Contacts.ContactShortInformationEntry> Interfaces.Contacts.IContactShortInformationPlugin.ContactShortInformationEntries(SDK.Contacts.ContactBase contact, bool isForCallWindow)
        {
            List<Interfaces.Contacts.ContactShortInformationEntry> result = new List<Interfaces.Contacts.ContactShortInformationEntry>();

            AMTANGEE.Interfaces.Contacts.ContactShortInformationEntryExtended csie = new Interfaces.Contacts.ContactShortInformationEntryExtended(AMTANGEE.SDK.Resources.Images[SDK.Resources.ImageIndexes.Website],AMTANGEE.Style.FontBold,System.Drawing.Color.Black, AMTANGEE.SDK.Settings.Global["AMTANGEE.MODULES.SHORTINFORMATIONURL"]["TEXT"], "", contact,true);
            result.Add(csie);

            return result;
        }

        void Interfaces.Contacts.IContactShortInformationPlugin.ContactShortInformationEntryDoubleClicked(Interfaces.Contacts.ContactShortInformationEntry entry)
        {
            AMTANGEE.SDK.Contacts.ContactBase cb = (AMTANGEE.SDK.Contacts.ContactBase)entry.Data;

            string url = AMTANGEE.SDK.Settings.Global["AMTANGEE.MODULES.SHORTINFORMATIONURL"]["URL"];


            url = url.Replace("@@GUID", cb.Guid.ToString());
            if(cb.DefaultEmailAddress != null && cb.DefaultEmailAddress.ExistsAndLoadedAndRights)
                url = url.Replace("@@EMAIL", cb.DefaultEmailAddress.Address);
            else
                url = url.Replace("@@EMAIL", "");

            if (cb.DefaultPhoneNumber != null && cb.DefaultPhoneNumber.ExistsAndLoadedAndRights)
                url = url.Replace("@@PHONENUMBER", cb.DefaultPhoneNumber.Comparison);
            else
                url = url.Replace("@@PHONENUMBER", "");

            if (cb is AMTANGEE.SDK.Contacts.Contact)
                url = url.Replace("@@CUSTOMERNO", ((AMTANGEE.SDK.Contacts.Contact)cb).CustomerNo);
            else
            {
                url = url.Replace("@@CUSTOMERNO", ((AMTANGEE.SDK.Contacts.ContactPerson)cb).Parent.CustomerNo);
            }

            if (cb is AMTANGEE.SDK.Contacts.Contact)
                url = url.Replace("@@VENDORNO", ((AMTANGEE.SDK.Contacts.Contact)cb).VendorNo);
            else
            {
                url = url.Replace("@@VENDORNO", ((AMTANGEE.SDK.Contacts.ContactPerson)cb).Parent.VendorNo);
            }

            System.Diagnostics.Process.Start(url);

        } 

        string Interfaces.ISettingsFormPlugin.SettingsFormPluginCaption
        {
            get { return "Kurzinformationen-Url"; }
        }

        Dictionary<string, SettingsControl> windows = new Dictionary<string, SettingsControl>();
        void Interfaces.ISettingsFormPlugin.SettingsFormPluginClose(string windowId)
        {
            if (windows.ContainsKey(windowId))
                windows.Remove(windowId);
        }

        System.Windows.Forms.UserControl Interfaces.ISettingsFormPlugin.SettingsFormPluginControl(string windowId)
        {
            SettingsControl uc1 = null;
            if (windows.ContainsKey(windowId))
            {
                uc1 = windows[windowId];
                uc1.LoadData();
            }

            return uc1;
        }

        void Interfaces.ISettingsFormPlugin.SettingsFormPluginCreate(string windowId)
        {
            if (!windows.ContainsKey(windowId))
            {
                SettingsControl uc1 = new SettingsControl();
                windows.Add(windowId, uc1);
            }
        }

        System.Drawing.Image Interfaces.ISettingsFormPlugin.SettingsFormPluginIcon
        {
            get { return AMTANGEE.SDK.Resources.Images[SDK.Resources.ImageIndexes.Website]; }
        }

        void Interfaces.ISettingsFormPlugin.SettingsFormPluginSave(string windowId)
        {
            SettingsControl sc1 = null;
            if (windows.ContainsKey(windowId))
            {
                sc1 = windows[windowId];
                sc1.SaveData();
            }
        }

        bool Interfaces.ISettingsFormPlugin.SettingsFormPluginVisible(string windowId)
        {
            return AMTANGEE.SDK.Global.CurrentUser.IsAdministrator;
        }
    }
}
