#region License and copyright notice
/* 
 * Kaliko Content Management System
 * 
 * Copyright (c) Fredrik Schultz
 * 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3.0 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
 * Lesser General Public License for more details.
 * http://www.gnu.org/licenses/lgpl-3.0.html
 */
#endregion

namespace KalikoCMS.Services.Localization {
    using System;
    using System.Collections;
    using System.Globalization;
    using System.IO;
    using System.Xml;
    using Interfaces;
    using Logging;

    public class LocalizationService : ILocalizationService {
        private static readonly Hashtable TranslationData = new Hashtable();
        private static readonly ILog Logger = LogProvider.For<Tester>();

        public string Translate(string key, bool errorIfMissing = true) {
            if (string.IsNullOrEmpty(key)) {
                var argumentException = new ArgumentException("Argument to Translate(string key) should not be null or empty.");
                Logger.Error("Argument error", argumentException);
                throw argumentException;
            }

            var lookupTable = GetLocalizedData();

            if (lookupTable.Contains(key.ToLowerInvariant())) {
                return lookupTable[key].ToString();
            }

            if (errorIfMissing) {
                Logger.Error($"Could not locate the text for the key: {key}. The table contained {lookupTable.Count} elements");
                return $"Missing translation for '{key}'";
            }

            return string.Empty;
        }

        public string TryTranslate(string key) {
            return Translate(key, false);
        }

        private Hashtable GetLocalizedData() {
            var currentLanguage = string.Empty; // TODO: CurrentLanguage.ToLowerInvariant();

            if (TranslationData.Count == 0) {
                Logger.Error("TranslationData was empty, trying to re-read the xml.");
                ReadLanguages();
            }
            if (TranslationData.Contains(currentLanguage)) {
                return (Hashtable)TranslationData[currentLanguage];
            }
            //if (TranslationData.Contains(DefaultLanguageValue)) {
            //    return (Hashtable)TranslationData[DefaultLanguageValue];
            //}

            return new Hashtable(0);
        }

        private static void ReadLanguages() {
            // TODO: Make dynamic
            var folder = "~/LocalizationResources/"; //HttpContext.Current.Server.MapPath("/lang/");

            if (!Directory.Exists(folder)) {
                Logger.Error($"Could not find the lang xml folder: {folder}");
                return;
            }

            var xmlFilenames = Directory.GetFiles(folder, "*.xml");

            foreach (var file in xmlFilenames) {
                try {
                    ParseXmlFile(file);
                }
                catch (Exception ex) {
                    Logger.Error($"Error parsing xml-file '{file}'! {ex.Message}");
                }
            }

        }

        private static void ParseXmlFile(string file) {
            var lookupTable = GetLookupTableFromFile(file, out string languageCode);

            if (!string.IsNullOrEmpty(languageCode)) {
                languageCode = languageCode.ToLowerInvariant();

                if (TranslationData.ContainsKey(languageCode)) {
                    AddValuesToExistingLookupTable(lookupTable, languageCode);
                }
                else {
                    TranslationData.Add(languageCode, lookupTable);
                }
            }
            else {
                Logger.Error($"Error parsing xml-file '{file}'!  Couldn't find language id in file.");
            }
        }

        private static Hashtable GetLookupTableFromFile(string file, out string languageCode) {
            var xmlTextReader = new XmlTextReader(file);
            var lookupTable = new Hashtable();
            var path = string.Empty;
            languageCode = string.Empty;

            while (xmlTextReader.Read()) {
                switch (xmlTextReader.NodeType) {
                    case XmlNodeType.Element: // Start element
                        if ((xmlTextReader.Name == "language"))
                            languageCode = xmlTextReader.GetAttribute("id");
                        else
                            path += string.Format(CultureInfo.InvariantCulture, "{0}/", xmlTextReader.Name.ToLowerInvariant());
                        break;
                    case XmlNodeType.EndElement: //End element.
                        path = path.Substring(0, path.LastIndexOf('/', path.Length - 2) + 1);
                        break;
                    case XmlNodeType.Text: // Content
                        lookupTable.Add(path.TrimEnd('/'), xmlTextReader.Value);
                        break;
                }
            }

            xmlTextReader.Close();
            return lookupTable;
        }

        private static void AddValuesToExistingLookupTable(Hashtable lookupTable, string languageCode) {
            var existingTable = (Hashtable)TranslationData[languageCode];

            foreach (DictionaryEntry dictionaryEntry in lookupTable) {
                existingTable.Add(dictionaryEntry.Key, dictionaryEntry.Value);
            }
        }
    }
}