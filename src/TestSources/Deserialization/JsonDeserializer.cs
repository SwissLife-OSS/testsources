using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using TestSources.Extensions;

namespace TestSources.Deserialization;

internal class JsonDeserializer : IJsonDeserializer
{
    private static readonly JsonLoadSettings JsonLoadSettings =
        new JsonLoadSettings
        {
            CommentHandling = CommentHandling.Ignore,
            LineInfoHandling = LineInfoHandling.Ignore,
            DuplicatePropertyNameHandling = DuplicatePropertyNameHandling.Error
        };

    private static readonly JsonSerializerSettings JsonSerializerSettings =
        new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            NullValueHandling = NullValueHandling.Include,
            Culture = CultureInfo.InvariantCulture,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            Converters = new List<JsonConverter> { new StringEnumConverter() },
            ContractResolver = ChildFirstContractResolver.Instance,
            TypeNameHandling = TypeNameHandling.Auto
        };

    public T? Deserialize<T>(string jsonString)
    {
        JsonLoadSettings jsonLoadSettings = JsonLoadSettings;
        var isValidJson = jsonString.IsValidJsonFormat(jsonLoadSettings);

        if (!isValidJson)
        {
            jsonString = jsonString
                .NormalizeLineEndings()
                .EnsureLineEnding();

            jsonString = JsonConvert.ToString(jsonString);
        }

        T obj = JsonConvert.DeserializeObject<T>(jsonString, JsonSerializerSettings);

        return obj;
    }

    private class ChildFirstContractResolver : DefaultContractResolver
    {
        static ChildFirstContractResolver() { Instance = new ChildFirstContractResolver(); }

        public static ChildFirstContractResolver Instance { get; }

        protected override IList<JsonProperty> CreateProperties(
            Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);

            if (properties != null)
            {
                properties = properties.OrderBy(p =>
                {
                    IEnumerable<Type> d = ((Type)p.DeclaringType).BaseTypesAndSelf().ToList();
                    return 1000 - d.Count();
                }).ToList();
            }

            return properties;
        }
    }
}
