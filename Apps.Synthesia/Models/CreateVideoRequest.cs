using Blackbird.Applications.Sdk.Common;

namespace Apps.Synthesia.Models
{
    public class CreateVideoRequest
    {
        [Display("Test video")]
        public bool Test { get; set; }

        [Display("Title")]
        public string Title { get; set; }

        [Display("Description")]
        public string Description { get; set; }

        [Display("Visibility")]
        public string Visibility { get; set; }

        [Display("Aspect ratio")]
        public string AspectRatio { get; set; }

        [Display("Script texts")]
        public List<string> InputScriptTexts { get; set; }

        [Display("Avatars")]
        public List<string> InputAvatars { get; set; }

        [Display("Avatar horizontal aligns")]
        public List<string>? InputAvatarSettingsHorizontalAligns { get; set; }

        [Display("Avatar scales")]
        public List<double>? InputAvatarSettingsScales { get; set; }

        [Display("Avatar styles")]
        public List<string>? InputAvatarSettingsStyles { get; set; }

        [Display("Avatar seamless flags")]
        public List<bool>? InputAvatarSettingsSeameless { get; set; }

        [Display("Backgrounds")]
        public List<string> InputBackgrounds { get; set; }

        [Display("Short background content match modes")]
        public List<string> InputBgShortMatchModes { get; set; }

        [Display("Long background content match modes")]
        public List<string> InputBgLongMatchModes { get; set; }
    }
}
