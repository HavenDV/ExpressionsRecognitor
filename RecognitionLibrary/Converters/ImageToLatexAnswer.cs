using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RecognitionLibrary.Converters
{
    public partial class Answer
    {
        [JsonProperty("latex")]
        public string Latex { get; set; }

        [JsonProperty("latex_confidence_rate")]
        public double LatexConfidenceRate { get; set; }

        [JsonProperty("detection_map")]
        public DetectionMap DetectionMap { get; set; }

        [JsonProperty("detection_list")]
        public List<string> DetectionList { get; set; }

        [JsonProperty("position")]
        public Position Position { get; set; }

        [JsonProperty("latex_confidence")]
        public double LatexConfidence { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("latex_list")]
        public List<object> LatexList { get; set; }
    }

    public class DetectionMap
    {
        [JsonProperty("is_not_math")]
        public long IsNotMath { get; set; }

        [JsonProperty("is_inverted")]
        public long IsInverted { get; set; }

        [JsonProperty("contains_chart")]
        public long ContainsChart { get; set; }

        [JsonProperty("contains_table")]
        public long ContainsTable { get; set; }

        [JsonProperty("contains_graph")]
        public long ContainsGraph { get; set; }

        [JsonProperty("contains_diagram")]
        public long ContainsDiagram { get; set; }

        [JsonProperty("is_blank")]
        public long IsBlank { get; set; }

        [JsonProperty("is_printed")]
        public double IsPrinted { get; set; }
    }

    public class Position
    {
        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("top_left_x")]
        public long TopLeftX { get; set; }

        [JsonProperty("top_left_y")]
        public long TopLeftY { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }
    }

    public partial class Answer
    {
        public static Answer FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Answer>(json, Converter.Settings);
        }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
