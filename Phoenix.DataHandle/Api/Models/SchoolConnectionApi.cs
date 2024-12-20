﻿using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Entities;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Base.Entities;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Types;

namespace Phoenix.DataHandle.Api.Models
{
    public class SchoolConnectionApi : ISchoolConnectionApi, IModelApi
    {
        [JsonConstructor]
        public SchoolConnectionApi(int id, int tenantId, string channel,
            string channelKey, string? channelToken)
        {
            if (string.IsNullOrWhiteSpace(channel))
                channel = null!;
            if (string.IsNullOrWhiteSpace(channelKey))
                channelKey = null!;

            this.Id = id;
            this.TenantId = tenantId;
            this.ChannelKey = channelKey;
            this.ChannelToken = channelToken;

            if (channel.TryToChannelProvider(out ChannelProvider channelProvider))
            {
                this.ChannelProvider = channelProvider;
                this.Channel = channelProvider.ToString();
            }
        }

        public SchoolConnectionApi(int id, int tenantId, ISchoolConnectionBase schoolConnection)
            : this(id, tenantId, schoolConnection.Channel,
                  schoolConnection.ChannelKey, schoolConnection.ChannelToken)
        {
            this.ActivatedAt = schoolConnection.ActivatedAt;
        }

        public SchoolConnectionApi(SchoolConnection schoolConnection)
            : this(schoolConnection.Id, schoolConnection.TenantId, schoolConnection)
        {
        }

        public SchoolConnection ToSchoolConnection()
        {
            return new()
            {
                Id = this.Id,
                TenantId = this.TenantId,
                Channel = this.Channel,
                ChannelKey = this.ChannelKey,
                ChannelToken = this.ChannelToken,
                ActivatedAt = this.ActivatedAt
            };
        }

        public SchoolConnection ToSchoolConnection(SchoolConnection schoolConnectionToUpdate)
        {
            schoolConnectionToUpdate.Channel = this.Channel;
            schoolConnectionToUpdate.ChannelKey = this.ChannelKey;
            schoolConnectionToUpdate.ChannelToken = this.ChannelToken;
            schoolConnectionToUpdate.ActivatedAt = this.ActivatedAt;

            return schoolConnectionToUpdate;
        }


        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("school_id", Required = Required.Always)]
        public int TenantId { get; }

        [JsonProperty("channel", Required = Required.Always)]
        public string Channel { get; } = null!;

        [JsonProperty("channel_key", Required = Required.Always)]
        public string ChannelKey { get; }

        [JsonIgnore]
        public string? ChannelToken { get; }

        [JsonProperty("activated_at")]
        public DateTime? ActivatedAt { get; }

        [JsonProperty("is_active")]
        public bool IsActive => this.ActivatedAt.HasValue;


        [JsonIgnore]
        public ChannelProvider ChannelProvider { get; set; }
    }
}
