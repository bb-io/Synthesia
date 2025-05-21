using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Synthesia.Models
{
    public class ListTemplatesResponse
    {
        [JsonProperty("templates")]
        public List<Template> Templates { get; set; }
    }

    public class Template
    {
        [JsonProperty("createdAt")]
        public long CreatedAt { get; set; }

        [JsonProperty("created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("lastUpdatedAt")]
        public long LastUpdatedAt { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("variables")]
        public List<TemplateVariable> Variables { get; set; }
    }

    public class TemplateVariable
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        // `value` може бути відсутнім, тому робимо nullable
        [JsonProperty("value")]
        public string? Value { get; set; }
    }
}
