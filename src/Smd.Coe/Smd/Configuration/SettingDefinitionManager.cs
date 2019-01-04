﻿using Smd.Configuration.Startup;
using Smd.Dependency;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Smd.Configuration
{
    /// <summary>
    /// Implements <see cref="ISettingDefinitionManager"/>.
    /// </summary>
    public class SettingDefinitionManager : ISettingDefinitionManager, ISingletonDependency
    { 
        private readonly ISettingsConfiguration _settingsConfiguration;
        private readonly IDictionary<string, SettingDefinition> _settings;

        /// <summary>
        /// Constructor.
        /// </summary>
        public SettingDefinitionManager( ISettingsConfiguration settingsConfiguration)
        { 
            _settingsConfiguration = settingsConfiguration;
            _settings = new Dictionary<string, SettingDefinition>();
        }

        public void Initialize()
        {
            var context = new SettingDefinitionProviderContext(this);

            //foreach (var providerType in _settingsConfiguration.Providers)
            //{
            //    using (var provider = CreateProvider(providerType))
            //    {
            //        foreach (var settings in provider.Object.GetSettingDefinitions(context))
            //        {
            //            _settings[settings.Name] = settings;
            //        }
            //    }
            //}
        }

        public SettingDefinition GetSettingDefinition(string name)
        {
            SettingDefinition settingDefinition;
            if (!_settings.TryGetValue(name, out settingDefinition))
            {
                throw new SmdException("There is no setting defined with name: " + name);
            }

            return settingDefinition;
        }

        public IReadOnlyList<SettingDefinition> GetAllSettingDefinitions()
        {
            return _settings.Values.ToImmutableList();
        }

        //private IDisposableDependencyObjectWrapper<SettingProvider> CreateProvider(Type providerType)
        //{
        //    return _iocManager.ResolveAsDisposable<SettingProvider>(providerType);
        //}
    }
}
