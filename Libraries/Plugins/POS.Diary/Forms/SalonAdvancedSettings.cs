﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Languages;

namespace POS.Diary.Forms
{
    public partial class SalonAdvancedSettings : POS.Base.Forms.BaseForm
    {
        public SalonAdvancedSettings(POS.Diary.Controls.SalonCalendarControl cal)
        {
            InitializeComponent();
            options1.SalonDiary = cal.salonDiary1;
            options1.OnTabChanged += options1_OnTabChanged;
        }

        void options1_OnTabChanged(object sender, EventArgs e)
        {
            switch (options1.ActiveTabID)
            {
                case 0: // general
                    POS.Base.Classes.AppController.ActiveHelpTopic = POS.Base.Classes.HelpTopics.DiarySettingsGeneral;
                    break;
                case 1: // colours
                    POS.Base.Classes.AppController.ActiveHelpTopic = POS.Base.Classes.HelpTopics.DiarySettingsColours;
                    break;
                case 2: // image overlays
                    POS.Base.Classes.AppController.ActiveHelpTopic = POS.Base.Classes.HelpTopics.DiarySettingsImageOverlay;
                    break;
                case 3: // autohide
                    POS.Base.Classes.AppController.ActiveHelpTopic = POS.Base.Classes.HelpTopics.DiarySettingsAutoHide;
                    break;
                case 4: // times
                    POS.Base.Classes.AppController.ActiveHelpTopic = POS.Base.Classes.HelpTopics.DiarySettingsTimes;
                    break;
                case 5: // advanced
                    POS.Base.Classes.AppController.ActiveHelpTopic = POS.Base.Classes.HelpTopics.DiarySettingsAdvanced;
                    break;
                default:
                    POS.Base.Classes.AppController.ActiveHelpTopic = POS.Base.Classes.HelpTopics.DiarySettings;
                    break;
            }
        }
        
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            options1_OnTabChanged(this, e);
        }

        protected override void LanguageChanged(System.Globalization.CultureInfo culture)
        {
            this.Text = LanguageStrings.AppDiaryAdvancedSettings;
        }
    }
}