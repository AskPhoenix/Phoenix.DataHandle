﻿using Newtonsoft.Json;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Utilities;
using Phoenix.DataHandle.DataEntry.Models.Uniques;
using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Phoenix.DataHandle.DataEntry.Models.Extensions;
using Phoenix.DataHandle.Main.Entities;
using System.Collections.Generic;

namespace Phoenix.DataHandle.DataEntry.Models
{
    public class SchoolACF : IModelACF, ISchool
    {
        private SchoolACF()
        {
            this.Classrooms = new List<IClassroom>();
            this.Courses = new List<ICourse>();
            this.Broadcasts = new List<IBroadcast>();
            this.SchoolLogins = new List<ISchoolLogin>();
            this.Users = new List<IAspNetUser>();
        }

        [JsonConstructor]
        public SchoolACF(int? code, string name, string? slug, string city, string address, string? comments, string language)
            : this()
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentNullException(nameof(city));
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentNullException(nameof(address));
            if (string.IsNullOrWhiteSpace(language))
                throw new ArgumentNullException(nameof(language));

            this.Code = code ?? 0;
            this.Name = name.Trim().Truncate(200);
            this.Slug = (string.IsNullOrWhiteSpace(slug) ? name : slug).Trim().Truncate(64);
            this.City = city.Trim().Truncate(200).ToTitleCase();
            this.AddressLine = address.Trim().Truncate(255);
            this.Description = string.IsNullOrWhiteSpace(comments) ? null : comments.Trim();
            this.Language = language;

            this.Locale = CultureInfo.GetCultures(CultureTypes.NeutralCultures).
                First(c => c.EnglishName.Equals(this.Language, StringComparison.InvariantCultureIgnoreCase)).
                TwoLetterISOLanguageName;

            this.SchoolInfo = new SchoolInfo
            {
                PrimaryLanguage = this.Language,
                PrimaryLocale = this.Locale
            };
        }

        public Expression<Func<School, bool>> GetUniqueExpression() => s => s.Code == this.Code;

        public SchoolUnique GetSchoolUnique() => new(this.Code);

        
        [JsonProperty(PropertyName = "code")]
        public int Code { get; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; } = null!;

        [JsonProperty(PropertyName = "slug")]
        public string Slug { get; } = null!;

        [JsonProperty(PropertyName = "city")]
        public string City { get; } = null!;

        [JsonProperty(PropertyName = "address")]
        public string AddressLine { get; } = null!;

        [JsonProperty(PropertyName = "comments")]
        public string? Description { get; }

        [JsonProperty(PropertyName = "language")]
        public string Language { get; } = null!;


        [JsonProperty(PropertyName = "locale")]
        public string Locale { get; } = null!;

        [JsonProperty(PropertyName = "school_info")]
        // TODO: Check in repository what happens with CreatedAt and other properties when creating/updating the object
        public ISchoolInfo SchoolInfo { get; } = null!;


        [JsonIgnore]
        public IEnumerable<IBroadcast> Broadcasts { get; }

        [JsonIgnore]
        public IEnumerable<IClassroom> Classrooms { get; }

        [JsonIgnore]
        public IEnumerable<ICourse> Courses { get; }

        [JsonIgnore]
        public IEnumerable<ISchoolLogin> SchoolLogins { get; }

        [JsonIgnore]
        public IEnumerable<IAspNetUser> Users { get; }
    }
}
