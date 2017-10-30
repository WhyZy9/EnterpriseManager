﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using SieraDelta.Library;

namespace SieraDelta.Library.Utils
{
    /// <summary>
    /// Performs basic validation
    /// </summary>
    public class Validation
    {
        private const string ALLOWED_CHARS_NAME = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-' ";
        private const string ALLOWED_CHARS_ALPHANUMERIC = "abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const string ALLOWED_CHARS_FILENAME = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.";
        private const string ALLOWED_CHARS_NUMBER = "0123456789";
        private const string ALLOWED_CHARS_CARD_DATE = "0123456789/";
        private const string CARD_TYPE_MASTERCARD = "^(51|52|53|54|55)";
        private const string CARD_TYPE_VISA = "^(4)";

        #region Public Static Methods

        /// <summary>
        /// Validates string against ValidationType
        /// 
        /// If the text does not match the ValidationType then an error is raised
        /// </summary>
        /// <param name="validationText">string to validate</param>
        /// <param name="validationType">type of validation</param>
        /// <returns></returns>
        public static string Validate(string validationText, Enums.ValidationTypes validationType)
        {
            string Result = validationText;

                switch (validationType)
                {
                    case Enums.ValidationTypes.AlphaNumeric:
                        Result = RemoveInvalidChars(Result, ALLOWED_CHARS_ALPHANUMERIC);

                        break;
                    case Enums.ValidationTypes.CreditCard:
                        Result = RemoveInvalidChars(Result, ALLOWED_CHARS_NUMBER);
                        ValidateCreditCard(Result);

                        break;
                    case Enums.ValidationTypes.IsNumeric:
                        Result = ValidateNumeric(RemoveInvalidChars(Result, ALLOWED_CHARS_NUMBER));

                        break;
                    case Enums.ValidationTypes.Name:
                        Result = RemoveInvalidChars(Result, ALLOWED_CHARS_NAME);

                        break;
                    case Enums.ValidationTypes.CardValidFrom:
                        Result = RemoveInvalidChars(Result, ALLOWED_CHARS_CARD_DATE);
                        CardDateValid(Result, false);

                        break;

                    case Enums.ValidationTypes.CardValidTo:
                        Result = RemoveInvalidChars(Result, ALLOWED_CHARS_CARD_DATE);
                        CardDateValid(Result, true);
                        break;

                    case Enums.ValidationTypes.FileName:
                        Result = RemoveInvalidChars(Result, ALLOWED_CHARS_FILENAME);

                        break;

                    default:
                        throw new ArgumentException("Invalid ValidationType, or validationType not handled");
                }

            //assume if it's zero length then error
                if (String.IsNullOrEmpty(Result))
                    throw new FormatException(String.Format("{0} is not of type {1}", validationText, validationType));

            return (Result);
        }

        public static SieraDelta.Shared.AcceptedCreditCardTypes CardType(string cardNumber)
        {
            if (Regex.IsMatch(cardNumber, CARD_TYPE_VISA))
                return (SieraDelta.Shared.AcceptedCreditCardTypes.Visa);

            if (Regex.IsMatch(cardNumber, CARD_TYPE_MASTERCARD))
                return (SieraDelta.Shared.AcceptedCreditCardTypes.MasterCard);

            throw new Exception("Could not determine credit card type");
        }

        #endregion Public Static Methods


        #region Private Static Methods

        private static void CardDateValid(string s, bool futureDate)
        {
            if (s.Length != 5)
                throw new Exception("Invalid Credit/Debit Valid Date");

            string[] parts = s.Split('/');

            DateTime cardDate = new DateTime(Convert.ToInt32("20" + parts[1]), Convert.ToInt32(parts[0]), 1);

            if (futureDate)
            {
                if (cardDate.Date < DateTime.Now.Date)
                    throw new Exception("Valid To Date can not be in the past.");
            }
            else
            {
                if (cardDate.Date > DateTime.Now.Date)
                    throw new Exception("Valid From Date can not be in the future.");
            }
        }

        private static string RemoveInvalidChars(string s, string validChars)
        {
            string Result = "";

            foreach (char c in s)
            {
                if (validChars.Contains(c.ToString()))
                    Result += c.ToString();
            }

            return (Result);
        }

        private static string ValidateNumeric(string validationText)
        {
            if (String.IsNullOrEmpty(validationText))
                throw new Exception("Invalid Number");

            Convert.ToInt64(validationText);

            return (validationText);
        }

        private static void ValidateCreditCard(string cardNumber)
        {
            int i, checkSum = 0;

            // Compute checksum of every other digit starting from right-most digit
            for (i = cardNumber.Length - 1; i >= 0; i -= 2)
                checkSum += (cardNumber[i] - '0');

            // Now take digits not included in first checksum, multiple by two,
            // and compute checksum of resulting digits
            for (i = cardNumber.Length - 2; i >= 0; i -= 2)
            {
                int val = ((cardNumber[i] - '0') * 2);
                while (val > 0)
                {
                    checkSum += (val % 10);
                    val /= 10;
                }
            }

            // Number is valid if sum of both checksums MOD 10 equals 0
            if ((checkSum % 10) != 0)
                throw new Exception("Credit Card number does not appear to be valid");
        }

        #endregion Private Static Methods
    }
}