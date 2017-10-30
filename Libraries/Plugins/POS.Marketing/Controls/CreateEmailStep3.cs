﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Library;
using Library.Utils;

using Languages;
using POS.Base.Classes;

namespace POS.Marketing.Controls
{
    public partial class CreateEmailStep3 : EmailWizardBase
    {
        #region Private Members

        private EmailWizardSettings _settings;

        #endregion Private Members

        #region Constructors

        public CreateEmailStep3()
        {
            InitializeComponent();
        }

        public CreateEmailStep3(EmailWizardSettings settings)
        {
            InitializeComponent();
            _settings = settings;

            LoadSettings();
        }

        #endregion Constructors

        #region Overridden Methods

        public override void LanguageChanged(System.Globalization.CultureInfo culture)
        {
            lblHeader.Text = LanguageStrings.AppCampaignColorTitle;
            btnSelectColor.Text = LanguageStrings.AppMenuButtonSelectColor;


        }

        public override bool SkipPage()
        {
            if (!XML.GetXMLValue(XMLFile(), _settings.Template, StringConstants.SETTINGS_COLOR_SET, true))
                return (true);

            return (base.SkipPage());
        }

        public override void LoadFromFile(string fileName)
        {
            _settings.TextColor = Color.FromArgb(
                XML.GetXMLValue(fileName, StringConstants.SETTINGS_STEP_3, StringConstants.SETTINGS_COLOR_R, 128),
                XML.GetXMLValue(fileName, StringConstants.SETTINGS_STEP_3, StringConstants.SETTINGS_COLOR_G, 128),
                XML.GetXMLValue(fileName, StringConstants.SETTINGS_STEP_3, StringConstants.SETTINGS_COLOR_B, 128));
        }

        public override void SaveToFile(string fileName)
        {
            XML.SetXMLValue(fileName, StringConstants.SETTINGS_STEP_3, StringConstants.SETTINGS_COLOR_R, _settings.TextColor.R.ToString());
            XML.SetXMLValue(fileName, StringConstants.SETTINGS_STEP_3, StringConstants.SETTINGS_COLOR_G, _settings.TextColor.G.ToString());
            XML.SetXMLValue(fileName, StringConstants.SETTINGS_STEP_3, StringConstants.SETTINGS_COLOR_B, _settings.TextColor.B.ToString());
        }

        public override bool NextClicked()
        {
            return base.NextClicked();
        }

        public override void PageShown()
        {
            colorDialog1.CustomColors = AppController.LocalSettings.CustomColors;
            lblTitle.Text = _settings.Title;
            lblStrapLine.Text = _settings.StrapLine;
            lblTitle.ForeColor = _settings.TextColor;
            lblStrapLine.ForeColor = _settings.TextColor;

            POS.Base.Classes.AppController.ActiveHelpTopic = POS.Base.Classes.HelpTopics.MarketingStep4;
        }

        public override bool PreviousClicked()
        {
            return (true);
        }

        #endregion Overridden Methods

        #region Private Methods

        private void LoadSettings()
        {
            colorDialog1.Color = _settings.TextColor;
        }

        private void btnSelectColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                AppController.LocalSettings.CustomColors = colorDialog1.CustomColors;
                _settings.TextColor = colorDialog1.Color;
            }

            lblTitle.ForeColor = _settings.TextColor;
            lblStrapLine.ForeColor = _settings.TextColor;
        }

        #endregion Private Methods
    }
}