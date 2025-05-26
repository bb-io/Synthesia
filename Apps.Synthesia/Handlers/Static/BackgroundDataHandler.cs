using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Synthesia.Handlers.Static
{
   public class BackgroundDataHandler : IStaticDataSourceItemHandler
    {
        public IEnumerable<DataSourceItem> GetData()
        {
            var backgrounds = new List<DataSourceItem>
            {
                new DataSourceItem("green_screen", "Transparent: Green screen"),
                new DataSourceItem("off_white", "Solid: Off White"),
                new DataSourceItem("warm_white", "Solid: Warm White"),
                new DataSourceItem("light_pink", "Solid: Light Pink"),
                new DataSourceItem("soft_pink", "Solid: Soft Pink"),
                new DataSourceItem("light_blue", "Solid: Light Blue"),
                new DataSourceItem("dark_blue", "Solid: Dark Blue"),
                new DataSourceItem("soft_cyan", "Solid: Soft Cyan"),
                new DataSourceItem("strong_cyan", "Solid: Strong Cyan"),
                new DataSourceItem("light_orange", "Solid: Light Orange"),
                new DataSourceItem("soft_orange", "Solid: Soft Orange"),
                new DataSourceItem("white_studio", "Image: White Studio"),
                new DataSourceItem("white_cafe", "Image: White Cafe"),
                new DataSourceItem("luxury_lobby", "Image: Luxury Lobby"),
                new DataSourceItem("large_window", "Image: Large Window"),
                new DataSourceItem("white_meeting_room", "Image: White Meeting Room"),
                new DataSourceItem("open_office", "Image: Open Office")
            };

            return backgrounds;
        }
    }
}
