﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Languages;

using Library.BOL.Therapists;
using Library.BOL.Users;
using Library;
using Library.Utils;

using POS.Base.Classes;

namespace POS.Staff.Forms
{
    public partial class AdminStaffEditWorkingHours : POS.Base.Forms.BaseForm
    {
        #region Private Methods

        private Therapist _therapist;

        #endregion Private Methods

        #region Constructors

        public AdminStaffEditWorkingHours(Therapist Therapist, DateTime Date)
        {
            _therapist = Therapist;
            
            InitializeComponent();

            dtWorkingHoursDate.MinDate = DateTime.Now.Date;
            dtWorkingHoursDate.Value = Date;
            LoadWorkingHoursSettings();
        }

        #endregion Construtors

        #region Overridden Methods
        
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            HelpTopic = POS.Base.Classes.HelpTopics.StaffWorkingHoursEdit;
        }

        protected override void LanguageChanged(System.Globalization.CultureInfo culture)
        {
            this.Text = LanguageStrings.AppStaffHoursTitle;

            lblEvery.Text = LanguageStrings.AppEvery;
            lblFinishHour.Text = LanguageStrings.AppFinishTime;
            lblRepeatFrequency.Text = LanguageStrings.AppRepeatFrequency;
            lblRepeatOption.Text = LanguageStrings.AppRepeatOption;
            lblStartDate.Text = LanguageStrings.AppStartDate;
            lblStartHour.Text = LanguageStrings.AppStartHour;

            cbAllowTreatments.Text = LanguageStrings.AppAllowTreatments;

            btnCancel.Text = LanguageStrings.AppMenuButtonCancel;
            btnSave.Text = LanguageStrings.AppMenuButtonSave;
        }

        #endregion Overridden Methods

        #region Private Methods

        private void LoadWorkingHoursSettings()
        {
            cmbWorkingHoursRepeatOption.Items.Clear();

            foreach (string item in Enum.GetNames(typeof(Enums.AppointmentRepeatType)))
            {
                cmbWorkingHoursRepeatOption.Items.Add(item);
            }

            cmbWorkingHoursRepeatOption.SelectedIndex = 0;
            LoadStartFinishTimes();
        }

        private void LoadStartFinishTimes()
        {
            DateTime Start = Convert.ToDateTime(String.Format(StringConstants.STAFF_FIRST_START_TIME, 
                DateTime.Now.ToShortDateString()));

            cmbWorkingHoursStart.Items.Clear();

            while (Start.Hour <= 14)
            {
                cmbWorkingHoursStart.Items.Add(Start.ToString(StringConstants.DATE_TIME_HOUR));
                Start = Start.AddMinutes(15);
            }

            cmbWorkingHoursStart.SelectedIndex = 12;

            Start = Convert.ToDateTime(String.Format(StringConstants.STAFF_FIRST_END_TIME, 
                DateTime.Now.ToShortDateString()));

            cmbWorkingHoursFinish.Items.Clear();

            while (Start.Hour <= 22)
            {
                cmbWorkingHoursFinish.Items.Add(Start.ToString(StringConstants.DATE_TIME_HOUR));
                Start = Start.AddMinutes(15);
            }

            cmbWorkingHoursFinish.SelectedIndex = 20;
        }

        private void DoSaveWorkingOverride(int iteration)
        {
            while (iteration < 11)
            {
                try
                {
                    WorkingDay day = _therapist.WorkingDays.Create(_therapist);

                    string Option = (string)cmbWorkingHoursRepeatOption.Items[cmbWorkingHoursRepeatOption.SelectedIndex];
                    day.Date = dtWorkingHoursDate.Value;
                    day.StartTime = Shared.Utilities.TimeToDouble((string)cmbWorkingHoursStart.Items[cmbWorkingHoursStart.SelectedIndex]);
                    day.FinishTime = Shared.Utilities.TimeToDouble((string)cmbWorkingHoursFinish.Items[cmbWorkingHoursFinish.SelectedIndex]);
                    day.RepeatOption = (Enums.AppointmentRepeatType)Enum.Parse(typeof(Enums.AppointmentRepeatType), Option, true);
                    day.RepeatDuration = Shared.Utilities.StrToIntDef(txtFrequency.Text, 1);
                    day.AllowTreatments = cbAllowTreatments.Checked;
                    day.Save();
                    break;
                }
                catch (Exception err)
                {
                    if (err.Message.Contains(StringConstants.ERROR_VIOLATION_APPT_OPTIONS))
                    {
                        DoSaveWorkingOverride(++iteration);
                    }
                    else
                        throw;
                }

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DoSaveWorkingOverride(0);
            }
            catch (Exception err)
            {
                if (err.Message.Contains(StringConstants.ERROR_VIOLATION_APPT_OPTIONS))
                {
                    DoSaveWorkingOverride(10);
                }
                else
                    throw;
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        #endregion Private Methods
    }
}
