//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MvxAndroidSettings.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Plugin.Settings.Droid
{
    using System;
    using System.Collections.Generic;

    using Android.App;
    using Android.Content;
    using Android.Preferences;

    public class MvxAndroidSettings : ISettings
    {
        private readonly object m_Locker = new object();

        public MvxAndroidSettings()
        {
            SharedPreferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
            SharedPreferencesEditor = SharedPreferences.Edit();
        }

        private static ISharedPreferences SharedPreferences { get; set; }
        private static ISharedPreferencesEditor SharedPreferencesEditor { get; set; }

        /// <summary>
        ///     Adds or updates a value
        /// </summary>
        /// <param name="key">key to update</param>
        /// <param name="value">value to set</param>
        /// <returns>True if added or update and you need to save</returns>
        public bool AddOrUpdateValue(string key, object value)
        {
            lock (this.m_Locker)
            {
                var typeOf = value.GetType();
                if (typeOf.IsGenericType && typeOf.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    typeOf = Nullable.GetUnderlyingType(typeOf);
                }
                var typeCode = Type.GetTypeCode(typeOf);
                switch (typeCode)
                {
                    case TypeCode.Boolean:
                        SharedPreferencesEditor.PutBoolean(key, Convert.ToBoolean(value));
                        break;
                    case TypeCode.Int64:
                        SharedPreferencesEditor.PutLong(key, Convert.ToInt64(value));
                        break;
                    case TypeCode.String:
                        SharedPreferencesEditor.PutString(key, Convert.ToString(value));
                        break;
                    case TypeCode.Int32:
                        SharedPreferencesEditor.PutInt(key, Convert.ToInt32(value));
                        break;
                    case TypeCode.Single:
                        SharedPreferencesEditor.PutFloat(key, Convert.ToSingle(value));
                        break;
                    case TypeCode.DateTime:
                        var intDateTime = ((DateTime)value).Ticks;
                        SharedPreferencesEditor.PutLong(key, Convert.ToInt64(intDateTime));
                        break;
                }
            }

            return true;
        }

        /// <summary>
        ///     Gets the current value or the default that you specify.
        /// </summary>
        /// <typeparam name="T">Vaue of t (bool, int, float, long, string)</typeparam>
        /// <param name="key">Key for settings</param>
        /// <param name="defaultValue">default value if not set</param>
        /// <returns>Value or default</returns>
        public T GetValueOrDefault<T>(string key, T defaultValue = default(T)) //where T : IComparable
        {
            lock (this.m_Locker)
            {
                var typeOf = typeof(T);
                if (typeOf.IsGenericType && typeOf.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    typeOf = Nullable.GetUnderlyingType(typeOf);
                }
                object value = null;
                var typeCode = Type.GetTypeCode(typeOf);
                switch (typeCode)
                {
                    case TypeCode.Boolean:
                        value = SharedPreferences.GetBoolean(key, Convert.ToBoolean(defaultValue));
                        break;
                    case TypeCode.Int64:
                        value = SharedPreferences.GetLong(key, Convert.ToInt64(defaultValue));
                        break;
                    case TypeCode.String:
                        value = SharedPreferences.GetString(key, Convert.ToString(defaultValue));
                        break;
                    case TypeCode.Int32:
                        value = SharedPreferences.GetInt(key, Convert.ToInt32(defaultValue));
                        break;
                    case TypeCode.Single:
                        value = SharedPreferences.GetFloat(key, Convert.ToSingle(defaultValue));
                        break;
                    case TypeCode.DateTime:
                        var ticks = SharedPreferences.GetLong(key, Convert.ToInt64(defaultValue));
                        if (ticks > 0)
                        {
                            value = new DateTime(ticks);
                        }
                        break;
                }

                return null != value ? (T)value : defaultValue;
            }
        }

        /// <summary>
        ///     Saves out all current settings
        /// </summary>
        public void Save()
        {
            lock (this.m_Locker)
            {
                SharedPreferencesEditor.Commit();
            }
        }

        public void Setup(Dictionary<string, object> defaultValues)
        {
        }
    }
}