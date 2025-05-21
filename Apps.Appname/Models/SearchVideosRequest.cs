using Apps.Appname.Handlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Appname.Models
{
    public class SearchVideosRequest
    {
        [Display("Source")]
        [StaticDataSource(typeof(SourceDataHandler))]
        public string? Source { get; set; }
    }
}
