﻿using System;
using System.Windows.Forms;

using Languages;
using SharedControls.WizardBase;
using POS.Administration.Classes;

namespace POS.Administration.Controls.Wizards.Coupons
{
    public partial class Step1 : BaseWizardPage
    {
        #region Private Members

        private CouponSettings _settings;

        #endregion Private Members

        #region Constructors

        public Step1()
        {
            InitializeComponent();
        }

        public Step1(CouponSettings settings)
            : this ()
        {
            _settings = settings;
            
            LoadCouponDetails();

            dtpStarts.CustomFormat = Shared.Utilities.DateFormat(true, true);
            dtpExpires.CustomFormat = Shared.Utilities.DateFormat(true, true);
            dtpStarts.Format = DateTimePickerFormat.Custom;
            dtpExpires.Format = DateTimePickerFormat.Custom;
        }

        #endregion Constructors

        #region Overridden Methods

        public override void LanguageChanged(System.Globalization.CultureInfo culture)
        {
            lblActive.Text = LanguageStrings.AppActive;
            lblCouponName.Text = LanguageStrings.AppName;
            lblExpiresDateTime.Text = LanguageStrings.AppExpires;
            lblStartDateTime.Text = LanguageStrings.AppStartDate;
            lblFreePostage.Text = LanguageStrings.AppDiscountCouponFreePostage;
            //lblFreeProduct.Text = LanguageStrings.AppDiscountCouponFreeProduct;
            //lblMainProduct.Text = LanguageStrings.AppDiscountCouponMainProduct;
            lblMaximumUsage.Text = LanguageStrings.AppMaximumUsage;
            lblMinimumSpend.Text = LanguageStrings.AppDiscountCouponMinimumSpend;
            //lblVoucherType.Text = LanguageStrings.AppDiscountCouponVoucherType;
            lblRestrictUsage.Text = LanguageStrings.AppCouponRestrictUsage;
        }

        public override bool NextClicked()
        {
            // verify name
            if (_settings.Coupon.ID == -1 && !ValidNewDiscountCode(txtName.Text))
            {
                return (false);
            }

            // verify start date


            // verify end date

            decimal minSpend = 0.00m;

            if (!decimal.TryParse(txtMinSpend.Text, out minSpend))
            {
                ShowError(LanguageStrings.AppError, LanguageStrings.AppDiscountCouponSaveMinimumSpend);
                txtMinSpend.Focus();
                return (false);
            }


            _settings.Coupon.Name = txtName.Text;
            _settings.Coupon.IsActive = cbActive.Checked;
            _settings.Coupon.Expires = dtpExpires.Value;
            _settings.Coupon.StartDateTime = dtpStarts.Value;
            _settings.Coupon.FreePostage = cbFreePostage.Checked;
            _settings.Coupon.MinimumSpend = minSpend;
            _settings.Coupon.MaxUsage = (int)numMaxUsage.Value;
            _settings.Coupon.RestrictUserUsage = cbRestrictUsage.Checked;

            return base.NextClicked();
        }

        public override void PageShown()
        {
            LoadCouponDetails();
            POS.Base.Classes.AppController.ActiveHelpTopic = POS.Base.Classes.HelpTopics.DiscountCouponStep1;
        }

        public override bool SkipPage()
        {
            return base.SkipPage();
        }

        #endregion Overridden Methods

        #region Private Methods

        private void LoadCouponDetails()
        {
            txtName.Text = _settings.Coupon.Name;
            cbActive.Checked = _settings.Coupon.IsActive;
            dtpExpires.Value = _settings.Coupon.Expires;
            dtpStarts.Value = _settings.Coupon.StartDateTime;
            cbFreePostage.Checked = _settings.Coupon.FreePostage;
            txtMinSpend.Text = _settings.Coupon.MinimumSpend.ToString();
            numMaxUsage.Value = _settings.Coupon.MaxUsage;
            cbRestrictUsage.Checked = _settings.Coupon.RestrictUserUsage;
        }

        /// <summary>
        /// Determines wether a new coupon code name is valid or not
        /// </summary>
        /// <param name="couponCode">name of coupon</param>
        /// <returns>true if valid, false otherwise</returns>
        private bool ValidNewDiscountCode(string couponCode)
        {
            bool Result = false;

            if (String.IsNullOrEmpty(couponCode))
            {
                ShowError(LanguageStrings.AppDiscountCouponNewCoupon, LanguageStrings.AppDiscountCouponNewEmpty);
                return (Result);
            }

            Result = Library.BOL.Coupons.Coupons.Get(couponCode) == null;

            if (!Result)
                ShowError(LanguageStrings.AppDiscountCouponNewCoupon, String.Format(LanguageStrings.AppDiscountCouponNewExists, couponCode));

            return (Result);
        }

        #endregion Private Methods
    }
}
