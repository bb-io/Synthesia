using Apps.Synthesia.Handlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Synthesia.Models
{
    public class CreateVideoRequest
    {
        [Display("Test video")]
        public bool? Test { get; set; }

        [Display("Title")]
        public string Title { get; set; }

        [Display("Description")]
        public string? Description { get; set; }

        [Display("Visibility")]
        [StaticDataSource(typeof(VisibilityDataHandler))]
        public string? Visibility { get; set; }

        [Display("Aspect ratio")]
        [StaticDataSource(typeof(AspectRatioDataHandler))]
        public string? AspectRatio { get; set; }

        [Display("Script texts")]
        public IEnumerable<string> InputScriptTexts { get; set; }

        [Display("Avatars")]
        [StaticDataSource(typeof(AvatarDataHandler))]
        public IEnumerable<string>? InputAvatars { get; set; }

        [Display("Avatar horizontal aligns")]
        [StaticDataSource(typeof(AvatarHorizontalAlignDataHandler))]
        public IEnumerable<string>? InputAvatarSettingsHorizontalAligns { get; set; }

        [Display("Avatar scales")]
        public IEnumerable<double>? InputAvatarSettingsScales { get; set; }

        [Display("Avatar styles")]
        public IEnumerable<string>? InputAvatarSettingsStyles { get; set; }

        [Display("Avatar seamless flags")]
        public IEnumerable<bool>? InputAvatarSettingsSeameless { get; set; }

        [Display("Backgrounds")]
        [StaticDataSource(typeof(BackgroundDataHandler))]
        public IEnumerable<string>? InputBackgrounds { get; set; }

        [Display("Short background content match modes")]
        public IEnumerable<string>? InputBgShortMatchModes { get; set; }

        [Display("Long background content match modes")]
        public IEnumerable<string>? InputBgLongMatchModes { get; set; }
    }

}
