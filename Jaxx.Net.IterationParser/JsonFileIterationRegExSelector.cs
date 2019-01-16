using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaxx.Net.IterationParser
{
    public class JsonFileIterationRegExSelector : IIterationRegExSelector
    {        
        private IConfigurationRoot Configuration { get; }
        
        public JsonFileIterationRegExSelector(string jsonFile)
        {
            var builder = new ConfigurationBuilder()
               .AddJsonFile(jsonFile, optional: false, reloadOnChange: false);

            Configuration = builder.Build();
        }

        public RegExSelector TestIterationCountSelector
        {
            get
            {
                return GetSelectorFromConfig("TestIterationCountSelector");
            }

            set => throw new NotImplementedException();
        }

        private RegExSelector GetSelectorFromConfig(string path)
        {            
            return new RegExSelector
            {
                Selector = Configuration.GetSection($"RegularExpressionFilters:IterationRegExSelector:{path}:RegExSelector:Selector").Value,
                SelectedMatchGroup = int.Parse(Configuration.GetSection($"RegularExpressionFilters:IterationRegExSelector:{path}:RegExSelector:SelectedMatchGroup").Value)
            };
        }

        public RegExSelector TestIterationDateSelector
        {
            get
            {
                return GetSelectorFromConfig("TestIterationDateSelector");
            }

            set => throw new NotImplementedException();
        }

        public RegExSelector TestIterationResultSelector
        {
            get
            {
                return GetSelectorFromConfig("TestIterationResultSelector");
            }

            set => throw new NotImplementedException();
        }

        public RegExSelector TestIterationTypeSelector
        {
            get
            {
                return GetSelectorFromConfig("TestIterationTypeSelector");
            }

            set => throw new NotImplementedException();
        }

        public RegExSelector TestIterationLineSelector
        {
            get
            {
                return GetSelectorFromConfig("TestIterationLineSelector");
            }

            set => throw new NotImplementedException();
        }

        public RegExSelector SingleLineSelector
        {
            get
            {
                return new RegExSelector
                {
                    Selector = Configuration.GetSection($"RegularExpressionFilters:SingleLineSelector:RegExSelector:Selector").Value
                };
            }

            set => throw new NotImplementedException();
        }
    }
}
