﻿using System;
using System.Collections.Generic;
using System.Text;

using Library.Utils;
using Shared.Classes;

namespace Library.BOL.HashTags
{
    [Serializable]
    public sealed class HashTags : BaseCollection
    {
        #region Private Members

        #endregion Private Members

        #region Static Methods


        public static void PageTagsClearCache()
        {
            DAL.DALHelper.InternalCache.Clear();
        }


        /// <summary>
        /// Returns all hash tags for a page
        /// </summary>
        /// <param name="PageName">Name of page</param>
        /// <returns>HashTags collection</returns>
        public static HashTags GetPageTags(string PageName)
        {
            if (PageName.Length > 500)
                PageName = PageName.Substring(0, 500);

            PageName = PageName.ToUpper();

            if (DAL.DALHelper.AllowCaching)
            {
                string cacheName = String.Format("Cached Page Tags {0}", PageName);

                CacheItem item = DAL.DALHelper.InternalCache.Get(cacheName);

                if (item != null)
                    return ((HashTags)item.Value);

                HashTags Result = DAL.FirebirdDB.HashTagsGet(PageName);

                DAL.DALHelper.InternalCache.Add(cacheName, new CacheItem(cacheName, Result));

                return (Result);
            }

            return (DAL.FirebirdDB.HashTagsGet(PageName));
        }

        /// <summary>
        /// Returns all hash tags
        /// </summary>
        /// <returns>hashTags collection</returns>
        public static HashTags GetTags()
        {
            string cacheName = "Cached Hash Tags All";

            if (DAL.DALHelper.AllowCaching)
            {
                CacheItem item = DAL.DALHelper.InternalCache.Get(cacheName);

                if (item != null)
                    return ((HashTags)item.Value);
            }

            HashTags Result = DAL.FirebirdDB.HashTagsGet();

            if (DAL.DALHelper.AllowCaching && Result != null)
                DAL.DALHelper.InternalCache.Add(cacheName, new CacheItem(cacheName, Result));
            
            return (Result);
        }

        /// <summary>
        /// Creates a hash tag and adds it to the page
        /// </summary>
        /// <param name="PageName">Name of Page</param>
        /// <param name="TagName">HashTag to create</param>
        public static void CreateHashTag(string PageName, string TagName)
        {
            if (PageName.Length > 500)
                PageName = PageName.Substring(0, 500);

            if (TagName.Length > 30)
                throw new Exception("Tag Name Too Long");

            TagName = Shared.Utilities.ProperCase(TagName);

            HashTag newTag = DAL.FirebirdDB.HashTagCreate(TagName);
            DAL.FirebirdDB.HashTagAdd(newTag, PageName.ToUpper());
            PageTagsClearCache();
        }

        /// <summary>
        /// Adds a HashTag to a page
        /// </summary>
        /// <param name="Tag">Tag to add</param>
        /// <param name="PageName">Page</param>
        public static void AddHashTag(HashTag Tag, string PageName)
        {
            if (PageName.Length > 500)
                PageName = PageName.Substring(0, 500);

            DAL.FirebirdDB.HashTagAdd(Tag, PageName.ToUpper());
            PageTagsClearCache();
        }

        /// <summary>
        /// Removes a hashtag from a page
        /// </summary>
        /// <param name="Tag">Tag to remove</param>
        /// <param name="PageName">Page</param>
        public static void RemoveHashTag(HashTag Tag, string PageName)
        {
            if (PageName.Length > 500)
                PageName = PageName.Substring(0, 500);
            
            DAL.FirebirdDB.HashTagRemove(Tag, PageName.ToUpper());
            PageTagsClearCache();
        }

        #endregion Static Methods

        #region Public Methods

        public string HashTagList(bool useComma)
        {
            string Result = String.Empty;

            foreach (HashTag tag in this)
            {
                if (useComma)
                    Result += String.Format("{0}, ", tag.Tag);
                else
                    Result += String.Format(" {0} ", tag.Tag);
            }

            //trim and remove last comma
            Result = Result.TrimEnd();

            if (Result.EndsWith(","))
                Result = Result.Substring(0, Result.Length - 1);

            return (Result);
        }

        #endregion Public Methods

        #region Generic CollectionBase Code

        #region Properties

        public HashTag this[int Index]
        {
            get
            {
                return ((HashTag)this.InnerList[Index]);
            }

            set
            {
                this.InnerList[Index] = value;
            }
        }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Adds an item to the collection
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Add(HashTag value)
        {
            return (List.Add(value));
        }

        /// <summary>
        /// Returns the index of an item within the collection
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int IndexOf(HashTag value)
        {
            return (List.IndexOf(value));
        }

        /// <summary>
        /// Determines wether a tag exists based on it's ID
        /// </summary>
        /// <param name="tagID">ID of tag</param>
        /// <returns>bool, true if the tag exists, otherwise false</returns>
        public bool IndexOf(Int64 tagID)
        {
            bool Result = false;

            foreach (HashTag tag in this)
            {
                if (tag.ID == tagID)
                {
                    Result = true;
                    break;
                }
            }

            return (Result);
        }

        /// <summary>
        /// Inserts an item into the collection
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void Insert(int index, HashTag value)
        {
            List.Insert(index, value);
        }


        /// <summary>
        /// Removes an item from the collection
        /// </summary>
        /// <param name="value"></param>
        public void Remove(HashTag value)
        {
            List.Remove(value);
        }


        /// <summary>
        /// Indicates the existence of an item within the collection
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(HashTag value)
        {
            // If value is not of type OBJECT_TYPE, this will return false.
            return (List.Contains(value));
        }

        #endregion Public Methods

        #region Private Members

        private const string OBJECT_TYPE = "Library.BOL.HashTags.HashTag";
        private const string OBJECT_TYPE_ERROR = "Must be of type HashTag";


        #endregion Private Members

        #region Overridden Methods

        /// <summary>
        /// When Inserting an Item
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        protected override void OnInsert(int index, Object value)
        {
            if (value.GetType() != Type.GetType(OBJECT_TYPE))
                throw new ArgumentException(OBJECT_TYPE_ERROR, "value");
        }


        /// <summary>
        /// When removing an item
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        protected override void OnRemove(int index, Object value)
        {
            if (value.GetType() != Type.GetType(OBJECT_TYPE))
                throw new ArgumentException(OBJECT_TYPE_ERROR, "value");
        }


        /// <summary>
        /// When Setting an Item
        /// </summary>
        /// <param name="index"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        protected override void OnSet(int index, Object oldValue, Object newValue)
        {
            if (newValue.GetType() != Type.GetType(OBJECT_TYPE))
                throw new ArgumentException(OBJECT_TYPE_ERROR, "newValue");
        }


        /// <summary>
        /// Validates an object
        /// </summary>
        /// <param name="value"></param>
        protected override void OnValidate(Object value)
        {
            if (value.GetType() != Type.GetType(OBJECT_TYPE))
                throw new ArgumentException(OBJECT_TYPE_ERROR);
        }


        #endregion Overridden Methods

        #endregion Generic CollectionBase Code
    }
}