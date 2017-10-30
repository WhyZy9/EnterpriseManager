﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Languages;
using Library.BOL.Staff;
using Library.BOL.Therapists;
using POS.Staff.Classes;

namespace POS.Staff.Controls.Wizards.Commission.PoolPayCommission
{
    public partial class Step5 : SharedControls.WizardBase.BaseWizardPage
    {
        #region Private Members

        private PayCommissionSettings _settings;

        #endregion Private Members

        #region Constructors

        public Step5(PayCommissionSettings settings)
        {
            InitializeComponent();

            _settings = settings;
        }

        #endregion Constructors

        #region Overridden Methods

        public override void LanguageChanged(System.Globalization.CultureInfo culture)
        {
            base.LanguageChanged(culture);

            lblDescription.Text = LanguageStrings.AppCommissionFinalise;
            lblMayTakeTime.Text = LanguageStrings.AppMayTakeTime;
        }

        public override void PageShown()
        {

        }

        public override bool BeforeFinish()
        {
            MainWizardForm.Cursor = Cursors.WaitCursor;
            try
            {
                _settings.Save(POS.Base.Classes.AppController.ActiveUser, Library.CommissionPaymentType.Pool);
            }
            finally
            {
                MainWizardForm.Cursor = Cursors.Arrow;
            }
            return base.BeforeFinish();
        }

        public override bool CanGoFinish()
        {
            return base.CanGoFinish();
        }

        #endregion Overridden Methods

        #region Private Methods

        #endregion Private Methods
    }
}