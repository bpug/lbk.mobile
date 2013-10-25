//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="XmlSerializer.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Data.Utils
{
    using System;
    using System.Xml.Serialization;

    using Cirrious.CrossCore;
    using Cirrious.CrossCore.Exceptions;
    using Cirrious.CrossCore.Platform;
    using Cirrious.MvvmCross.Plugins.File;

    using Lbk.Mobile.Common;

    public static class XmlSerializer<T>
        where T : class
    {
        public static T Load(string path)
        {
            var serializableObject = LoadFromDocumentFormat(null, path);
            return serializableObject;
        }

        public static T Load(string path, IMvxFileStore fileService)
        {
            var serializableObject = LoadFromDocumentFormat(null, path, fileService);
            return serializableObject;
        }

        

         public static T LoadFromResource(string path)
        {
            var serializableObject = LoadFromResourceDocumentFormat(null, path);
            return serializableObject;
        }

         public static T LoadFromResource(string path, Type[] extraTypes)
         {
             var serializableObject = LoadFromResourceDocumentFormat(null, path);
             return serializableObject;
         }
        
        public static T Load(string path, Type[] extraTypes)
        {
            var serializableObject = LoadFromDocumentFormat(extraTypes, path);
            return serializableObject;
        }

        public static void Save(T serializableObject, string path)
        {
            SaveToDocumentFormat(serializableObject, null, path);
        }

        public static void Save(T serializableObject, string path, Type[] extraTypes)
        {
            SaveToDocumentFormat(serializableObject, extraTypes, path);
        }

        private static XmlSerializer CreateXmlSerializer(Type[] extraTypes)
        {
            var objectType = typeof(T);

            var xmlSerializer = extraTypes != null ? new XmlSerializer(objectType, extraTypes) : new XmlSerializer(objectType);

            return xmlSerializer;
        }


        private static T LoadFromResourceDocumentFormat(Type[] extraTypes, string path)
        {
            T serializableObject = null;

            var resourceLoader = Mvx.Resolve<IMvxResourceLoader>();
            
            resourceLoader.GetResourceStream(path,
                stream =>
                {
                    var xmlSerializer = CreateXmlSerializer(extraTypes);
                    try
                    {
                        serializableObject = xmlSerializer.Deserialize(stream) as T;
                    }
                    catch (Exception exception)
                    {
                        Trace.Error("Problem with deserialize to {0}. Error: {1}", typeof(T).FullName, exception.ToLongString());
                    }
                } );

            return serializableObject;
        }

        private static T LoadFromDocumentFormat(Type[] extraTypes, string path, IMvxFileStore fileService)
        {
            T serializableObject = null;

            fileService.TryReadBinaryFile(
                path,
                stream =>
                {
                    var xmlSerializer = CreateXmlSerializer(extraTypes);
                    try
                    {
                        serializableObject = xmlSerializer.Deserialize(stream) as T;
                        return true;
                    }
                    catch (Exception exception)
                    {
                        Trace.Error("Problem with deserialize to {0}. Error: {1}", typeof(T).FullName, exception.ToLongString());
                        return false;
                    }
                });

            return serializableObject;
        }

        private static T LoadFromDocumentFormat(Type[] extraTypes, string path)
        {
            var fileService = Mvx.Resolve<IMvxFileStore>();
            return LoadFromDocumentFormat(extraTypes, path, fileService);
        }

        private static void SaveToDocumentFormat(T serializableObject, Type[] extraTypes, string path)
        {
            var fileService = Mvx.Resolve<IMvxFileStore>();
            fileService.WriteFile(
                path,
                stream =>
                {
                    var xmlSerializer = CreateXmlSerializer(extraTypes);
                    try
                    {
                        xmlSerializer.Serialize(stream, serializableObject);
                    }
                    catch (Exception exception)
                    {
                        Trace.Error("Problem with serializable from {0}. Error: {1}", typeof(T).FullName, exception.ToLongString());
                        throw;
                    }
                   
                });
        }
    }
}