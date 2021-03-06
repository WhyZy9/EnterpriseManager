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
 *  File: AdminProductGroupEdit.cs
 *
 *  Purpose:  
 *
 *  Date        Name                Reason
 *  
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
using System;
using System.IO;
using System.Windows.Forms;

using Languages;

using SharedBase;
using SharedBase.BOL.Products;

using POS.Base.Classes;
using POS.Base.Plugins;

#pragma warning disable IDE1006

namespace POS.Administration.Forms.Products
{
    public partial class AdminProductGroupEdit : POS.Base.Forms.BaseForm
    {
        #region Private Members

        private ProductGroup _group;

        #endregion Private Members

        #region Constructors

        public AdminProductGroupEdit(ProductGroup Group)
        {
            _group = Group;

            InitializeComponent();

            txtGroupName.Text = _group.Description;
            spnSortOrder.Value = Shared.Utilities.ValueWithin((int)spnSortOrder.Minimum, (int)spnSortOrder.Maximum, _group.SortOrder);
            txtTagLine.Text = _group.TagLine;
            cbShowOnWebsite.Checked = _group.ShowOnWebsite;
            txtURL.Text = _group.URL;
            lblMemberLevel.Visible = true;
            lblGroupType.Visible = true;
            cmbGroupType.Visible = true;
            cmbMemberLevel.Visible = true;
            txtPrimaryHeader.Text = _group.MainHeader;
            txtSubHeader.Text = _group.SubHeader;
            cbShowOnMobilePage.Checked = _group.MobileWebsite;
            LoadImages();

            cmbMobileImage.SelectedIndex = cmbMobileImage.Items.IndexOf(_group.MobileImage);

            this.Text = LanguageStrings.AppProductGroupEditAdministration;

            LoadGroupTypes();
            LoadMemberLevels();

            cmbMemberLevel.SelectedIndex = Shared.Utilities.CheckMinMax((int)_group.MemberLevel, 0, 10);

            if (AppController.ApplicationRunning)
            {
                NotificationEventArgs args = new NotificationEventArgs(StringConstants.PLUGIN_EVENT_WEBSITE_NAME);
                PluginManager.RaiseEvent(args);

                if (String.IsNullOrEmpty((string)args.Result))
                {
                    tabControlMain.TabPages.Remove(tabPageSEO);
                }
                else
                {
                    try
                    {
                        string url = (string)args.Result;

                        if (!url.StartsWith(StringConstants.BASE_WEB_HTTP) && !url.StartsWith(StringConstants.BASE_WEB_HTTPS))
                            url = StringConstants.BASE_WEB_HTTP + url;

                        Uri uri = new Uri(url + _group.URL);
                        seoSettings1.Url = uri;
                    }
                    catch
                    {
                        tabControlMain.TabPages.Remove(tabPageSEO);
                    }
                }
            }
        }

        #endregion Constructors

        #region Overridden Methods

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            tabControl1_SelectedIndexChanged(this, e);
        }

        protected override void LanguageChanged(System.Globalization.CultureInfo culture)
        {
            this.Text = LanguageStrings.AppProductGroup;
            tabPageSEO.Text = LanguageStrings.SEO;
            tabPageGeneral.Text = LanguageStrings.AppGeneral;
            tabPageMobile.Text = LanguageStrings.AppMobileSettings;

            btnCancel.Text = LanguageStrings.AppMenuButtonCancel;
            btnDelete.Text = LanguageStrings.AppMenuButtonDelete;
            btnSave.Text = LanguageStrings.AppMenuButtonSave;

            cbShowOnWebsite.Text = LanguageStrings.AppShowOnWebsite;

            lblGroupName.Text = LanguageStrings.AppProductGroupName;
            lblMemberLevel.Text = LanguageStrings.AppMemberLevel;
            lblGroupType.Text = LanguageStrings.AppPrimaryGroup;
            lblSortOrder.Text = LanguageStrings.AppSortOrder;
            lblTagLine.Text = LanguageStrings.AppProductGroupTagLine;
            lblURL.Text = LanguageStrings.AppURL;
            lblSubHeader.Text = LanguageStrings.AppSubHeader;
            lblPrimaryHeader.Text = LanguageStrings.AppHeader;
        }

        protected override void SetPermissions()
        {
            btnSave.Enabled = AppController.ActiveUser.MemberLevel > SharedBase.MemberLevel.AdminReadOnly;
            btnDelete.Enabled = AppController.ActiveUser.MemberLevel >= SharedBase.MemberLevel.AdminUpdateDelete;
        }

        #endregion Overridden Methods

        #region Private Methods

        private void LoadImages()
        {
            cmbMobileImage.Items.Clear();
            cmbMobileImage.Items.Add(String.Empty);

            string imageRoot = AppController.POSFolder(ImageTypes.Products);

            string[] files = Directory.GetFiles(imageRoot, StringConstants.IMAGE_SEARCH_PRODUCTS);

            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                int idx = cmbMobileImage.Items.Add(fileName);

                if (file.EndsWith(_group.MobileImage))
                {
                    cmbMobileImage.SelectedIndex = idx;
                }
            }
        }

        private void LoadGroupTypes()
        {
            cmbGroupType.Items.Clear();

            foreach (ProductGroupType groupType in ProductGroupTypes.Get())
            {
                int idx = cmbGroupType.Items.Add(groupType);

                if (groupType.ID == _group.GroupType.ID)
                    cmbGroupType.SelectedIndex = idx;
            }
        }

        private void LoadMemberLevels()
        {
            cmbMemberLevel.Items.Clear();

            foreach (MemberLevel member in (MemberLevel[])Enum.GetValues(typeof(MemberLevel)))
            {
                cmbMemberLevel.Items.Add(member);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _group.Description = txtGroupName.Text;
                _group.SortOrder = (int)spnSortOrder.Value;
                _group.TagLine = txtTagLine.Text;
                _group.ShowOnWebsite = cbShowOnWebsite.Checked;
                _group.URL = txtURL.Text;
                //_Group.ProductType = PrimaryProductType.Professional; // outdated now
                _group.MemberLevel = (MemberLevel)cmbMemberLevel.Items[cmbMemberLevel.SelectedIndex];
                _group.MainHeader = txtPrimaryHeader.Text;
                _group.SubHeader = txtSubHeader.Text;
                _group.GroupType = (ProductGroupType)cmbGroupType.Items[cmbGroupType.SelectedIndex];
                _group.MobileWebsite = cbShowOnMobilePage.Checked;
                _group.MobileImage = (string)cmbMobileImage.Items[cmbMobileImage.SelectedIndex];

                _group.Save(AppController.ActiveUser);
                seoSettings1.Save();
                DialogResult = DialogResult.OK;
            }
            catch (Exception error)
            {
                string msg = LanguageStrings.AppErrorUnexpectedDescription;

                if (error.Message.Contains(StringConstants.ERROR_LOCK_CONFLICT))
                    msg = LanguageStrings.AppErrorProductGroupConflict;

                ShowError(LanguageStrings.AppProductGroupSave, msg);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ShowHardConfirm(LanguageStrings.AppProductGroupDelete, 
                LanguageStrings.AppProductGroupDeletePrompt))
            {
                if (_group.GroupType.GetProducts().Count > 1)
                    ShowInformation(LanguageStrings.AppProductGroupDelete, 
                        LanguageStrings.AppProductGroupDeleteContainsProducts);
                else
                {
                    _group.Delete(AppController.ActiveUser);
                    DialogResult = System.Windows.Forms.DialogResult.Abort;
                }
            }
        }

        private void cmbGroupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblGroupName.Enabled = cmbGroupType.SelectedIndex == 0;
            txtGroupName.Enabled = cmbGroupType.SelectedIndex == 0;

            lblPrimaryHeader.Enabled = cmbGroupType.SelectedIndex > 0;
            txtPrimaryHeader.Enabled = cmbGroupType.SelectedIndex > 0;
            lblSubHeader.Enabled = cmbGroupType.SelectedIndex > 0;
            txtSubHeader.Enabled = cmbGroupType.SelectedIndex > 0;
        }

        private void cmbGroupType_Format(object sender, ListControlConvertEventArgs e)
        {
            ProductGroupType item = (ProductGroupType)e.ListItem;
            e.Value = item.Description;
        }

        private void cmbMobileImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string imageRoot = AppController.POSFolder(ImageTypes.Products);
            pbMobileImage.ImageLocation = imageRoot + cmbMobileImage.SelectedItem;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlMain.SelectedTab == tabPageGeneral)
                HelpTopic = POS.Base.Classes.HelpTopics.ProductGroupEditGeneral;
            else if (tabControlMain.SelectedTab == tabPageMobile)
                HelpTopic = POS.Base.Classes.HelpTopics.ProductGroupEditMobile;
        }

        #endregion Private Methods
    }
}
