using Newtonsoft.Json;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Utilities;
using System;
using System.Linq.Expressions;

namespace Phoenix.DataHandle.WordPress.Models
{
    public class SchoolACF : IModelACF<School>
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "slug")]
        public string Slug { get => slug; set => slug = string.IsNullOrWhiteSpace(value) ? null : value; }
        private string slug;

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "comments")]
        public string Comments { get => comments; set => comments = string.IsNullOrWhiteSpace(value) ? null : value; }
        private string comments;

        public string FacebookPageId { get; set; }

        public Expression<Func<School, bool>> MatchesUnique => s => s != null && s.Name == this.Name && s.City == this.City;
        
        public School ToContext()
        {
            return new School
            {
                Name = this.Name?.Substring(0, Math.Min(this.Name.Length, 200)),
                Slug = (this.Slug ?? this.Name)?.Substring(0, Math.Min((this.Slug ?? this.Name).Length, 64)),
                City = this.City?.Substring(0, Math.Min(this.City.Length, 200)),
                AddressLine = this.Address?.Substring(0, Math.Min(this.Address.Length, 255)),
                Info = this.Comments,
                FacebookPageId = this.FacebookPageId.Substring(0, Math.Min(this.FacebookPageId.Length, 20)),
                CreatedAt = DateTimeOffset.Now
            };
        }

        public IModelACF<School> WithTitleCase()
        {
            return new SchoolACF
            {
                Name = this.Name,
                Slug = this.Slug,
                City = this.City?.UpperToTitleCase(),
                Address = this.Address?.UpperToTitleCase(),
                Comments = this.Comments
            };
        }
    }
}
