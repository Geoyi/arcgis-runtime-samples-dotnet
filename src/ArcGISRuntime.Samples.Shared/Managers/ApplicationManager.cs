﻿//Copyright 2015 Esri.
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.
using ArcGISRuntime.Samples.Models;

namespace ArcGISRuntime.Samples.Managers
{
    public class ApplicationManager
    {
        #region Constructor and unique instance management

        // Private constructor
        private ApplicationManager() { }

        // Static initialization of the unique instance 
        private static readonly ApplicationManager SingleInstance = new ApplicationManager();

        /// <summary>
        /// Gets the single <see cref="MapViewController"/> instance.
        /// </summary>
        public static ApplicationManager Current
        {
            get { return SingleInstance; }
        }

        public void Initialize(Language language)
        {
            SelectedLanguage = language;
        }

        #endregion // Constructor and unique instance management

        public Language SelectedLanguage { get; private set; }
    }
}