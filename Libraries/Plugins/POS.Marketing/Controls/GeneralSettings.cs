/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 *  Enterprise Manager is distributed under the GNU General Public License version 3 and  
 *  is also available under alternative licenses negotiated directly with Simon Carter.  
 *  If you obtained Enterprise Manager under the GPL, then the GPL applies to all loadable 
 *  Enterprise Manager modules used on your system as well. The GPL (version 3) is 
 *  available at https://opensource.org/licenses/GPL-3.0
 *
 *  This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 *  without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 *  See the GNU General Public License for more details.
 *
 *  The Original Code was created by Simon Carter (s1cart3r@gmail.com)
 *
 *  Copyright (c) 2010 - 2019 Simon Carter.  All Rights Reserved.
 *
 *  Product:  Enterprise Manager
 *  
 *  File: GeneralSettings.cs
 *
 *  Purpose:  
 *
 *  Date        Name                Reason
 *  
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using POS.Base.Classes;

namespace POS.Marketing.Controls
{
    public partial class GeneralSettings : SharedControls.BaseSettings
    {
        public GeneralSettings()
        {
            InitializeComponent();
        }

        #region Overridden Methods

        public override void LanguageChanged(System.Globalization.CultureInfo culture)
        {
            cbAutoUpdateMarketingFiles.Text = Languages.LanguageStrings.AppUpdateMarketingFilesAtStart;
        }

        public override bool SettingsSave()
        {
            AppController.LocalSettings.UpdateMarketingFilesAtStart = cbAutoUpdateMarketingFiles.Checked;

            return base.SettingsSave();
        }

        public override void SettingsLoaded()
        {
            cbAutoUpdateMarketingFiles.Checked = AppController.LocalSettings.UpdateMarketingFilesAtStart;
        }

        #endregion Overridden Methods

    }
}
