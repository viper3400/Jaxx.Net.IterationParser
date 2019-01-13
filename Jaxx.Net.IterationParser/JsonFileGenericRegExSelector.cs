using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaxx.Net.IterationParser
{
    public class JsonFileGenericRegExSelector
    {
        private IConfigurationRoot Configuration { get; }

        public JsonFileGenericRegExSelector(string jsonFile)
        {
            var builder = new ConfigurationBuilder()
               .AddJsonFile(jsonFile, optional: false, reloadOnChange: false);

            Configuration = builder.Build();
        }

        public List<RegExSelector> RegExSelectors
        {
            get
            {
                var result = new List<RegExSelector>();

                var selectors = Configuration.GetSection($"RegularExpressionFilters:RegExSelectors").GetChildren();
                foreach (var selector in selectors)
                {

                    var regExSelector = new RegExSelector
                    {
                        Name = selector.GetSection("Name").Value,
                        Selector = selector.GetSection("Selector").Value,
                        SelectedMatchGroup = int.Parse(selector.GetSection("SelectedMatchGroup").Value)
                    };

                    result.Add(regExSelector);
                }
                return result;
            }
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
