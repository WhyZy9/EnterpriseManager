﻿using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

using Library;
using POS.Base.Classes;

namespace PointOfSale.Classes
{
    internal class ValidateSettings
    {
        private static UserSettings _settings = null;

        /// <summary>
        /// Initialises the validation routines
        /// </summary>
        internal static void Validate()
        {
            _settings = new UserSettings();

            try
            {
                ValidateAdobe();
            }
            catch (Exception err)
            {
                ErrorHandling.LogError(MethodBase.GetCurrentMethod(), err);
            }
            finally
            {
                _settings = null;
            }


        }

        #region Private Methods

        /// <summary>
        /// Validates Adobe Settings
        /// </summary>
        private static void ValidateAdobe()
        {
            AppController.UpdateSplashScreen(String.Format(Languages.LanguageStrings.AppValidating, StringConstants.SETTINGS_ADOBE));
            string adobe = GetXMLValue(StringConstants.SETTINGS_APPLICATION, StringConstants.SETTINGS_ADOBE);

            if (!File.Exists(adobe))
            {
                if (AskQuestion(StringConstants.SETTINGS_ADOBE, String.Format(Languages.LanguageStrings.AppProductMissingFindIt, StringConstants.SETTINGS_ADOBE, Application.ProductName)))
                {
                    string[] installs = FindFile(String.Format(StringConstants.FOLDER_ADOBE, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)), StringConstants.FILE_ADOBE);

                    if (installs.Length > 0)
                    {
                        SetXMLValue(StringConstants.SETTINGS_APPLICATION, StringConstants.SETTINGS_ADOBE, installs[0]);
                        ShowMessage(StringConstants.SETTINGS_ADOBE, String.Format(Languages.LanguageStrings.AppProductConfigured, StringConstants.SETTINGS_ADOBE));
                    }
                    else
                    {
                        installs = FindFile(String.Format(StringConstants.FOLDER_ADOBE, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)), StringConstants.FILE_ADOBE);

                        if (installs.Length > 0)
                        {
                            SetXMLValue(StringConstants.SETTINGS_APPLICATION, StringConstants.SETTINGS_ADOBE, installs[0]);
                            ShowMessage(StringConstants.SETTINGS_ADOBE, String.Format(Languages.LanguageStrings.AppProductConfigured, StringConstants.SETTINGS_ADOBE));
                        }
                        else
                        {
                            ShowWarning(StringConstants.SETTINGS_ADOBE, String.Format(Languages.LanguageStrings.AppProductNotFoundInstall, StringConstants.SETTINGS_ADOBE));
                        }
                    }
                }
            }
        }

        #endregion Private Methods

        #region Internal Properties

        internal static string CurrentPath
        {
            get
            {
                string Result = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                Result = Path.GetDirectoryName(Result);
                return (Result.Substring(6));
            }
        }

        #endregion Internal Properties

        #region Internal Methods

        internal static string[] FindFile(string path, string fileName)
        {
            string[] Result = Directory.GetFiles(path, fileName, SearchOption.AllDirectories);

            return (Result);
        }

        internal static bool AskQuestion(string title, string question)
        {
            return (MessageBox.Show(question, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes);
        }

        internal static void ShowWarning(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        internal static void ShowMessage(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        internal static void SetXMLValue(string ParentName, string KeyName, string Value)
        {
            XmlDocument xmldoc = new XmlDocument();
            string ConfigFile = CurrentPath + StringConstants.FILE_CONFIG;
            xmldoc.Load(ConfigFile);
            XmlNode Root = xmldoc.DocumentElement;
            bool FoundParent = false;
            bool Found = false;
            XmlNode xmlParentNode = null;

            if (Root != null & Root.Name == StringConstants.XML_ROOT_NODE_NAME)
            {
                for (int i = 0; i <= Root.ChildNodes.Count - 1; i++)
                {
                    XmlNode Child = Root.ChildNodes[i];

                    if (Child.Name == ParentName)
                    {
                        FoundParent = true;
                        xmlParentNode = Child;

                        for (int j = 0; j <= Child.ChildNodes.Count - 1; j++)
                        {
                            XmlNode Item = Child.ChildNodes[j];

                            if (Item.Name == KeyName)
                            {
                                Item.InnerText = Value;
                                Found = true;
                                xmldoc.Save(ConfigFile);
                            }
                        }
                    }
                }
            }

            if (!Found)
            {
                if (!FoundParent)
                {
                    xmlParentNode = xmldoc.CreateNode(XmlNodeType.Element, String.Empty, ParentName, null);
                    //XmlElement appendedParentName = xmldoc.CreateElement(ParentName);
                    Root.AppendChild(xmlParentNode);
                }

                XmlElement appendedKeyName = xmldoc.CreateElement(KeyName);
                XmlText xmlKeyName = xmldoc.CreateTextNode(Value);
                appendedKeyName.AppendChild(xmlKeyName);
                xmlParentNode.AppendChild(appendedKeyName);



                //XmlNode menu = xmldoc.SelectSingleNode(StringConstants.XML_ROOT_NODE_NAME);
                //XmlNode newNode = xmldoc.CreateNode(XmlNodeType.Element, ParentName, KeyName);
                //XmlNode newNodea = xmldoc.CreateNode(XmlNodeType.Element, KeyName, null);
                //newNodea.InnerText = Value;
                xmldoc.Save(ConfigFile);
            }
        }

        internal static string GetXMLValue(string ParentName, string KeyName)
        {
            string Result = String.Empty;


            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(CurrentPath + StringConstants.FILE_CONFIG);
            XmlNode Root = xmldoc.DocumentElement;

            if (Root != null & Root.Name == StringConstants.XML_ROOT_NODE_NAME)
            {
                for (int i = 0; i <= Root.ChildNodes.Count - 1; i++)
                {
                    XmlNode Child = Root.ChildNodes[i];

                    if (Child.Name == ParentName)
                    {
                        for (int j = 0; j <= Child.ChildNodes.Count - 1; j++)
                        {
                            XmlNode Item = Child.ChildNodes[j];

                            if (Item.Name == KeyName)
                            {
                                Result = Item.InnerText;
                                return (Result);
                            }
                        }
                    }
                }
            }

            return (Result);
        }

        internal static string GetXMLValue(string ParentName, string KeyName, string fileName)
        {
            string Result = String.Empty;


            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(fileName);
            XmlNode Root = xmldoc.DocumentElement;

            if (Root != null & Root.Name == StringConstants.XML_ROOT_NODE_NAME)
            {
                for (int i = 0; i <= Root.ChildNodes.Count - 1; i++)
                {
                    XmlNode Child = Root.ChildNodes[i];

                    if (Child.Name == ParentName)
                    {
                        for (int j = 0; j <= Child.ChildNodes.Count - 1; j++)
                        {
                            XmlNode Item = Child.ChildNodes[j];

                            if (Item.Name == KeyName)
                            {
                                Result = Item.InnerText;
                                return (Result);
                            }
                        }
                    }
                }
            }

            return (Result);
        }

        #endregion Internal Methods

    }
}
