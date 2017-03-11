﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  #if NETFX_LT_4_5
  using AliasList        = IEnumerable<IAlias>;
  using RelationshipList = IEnumerable<IRelationship>;
  using TagList          = IEnumerable<ITag>;
  using UserTagList      = IEnumerable<IUserTag>;
  #else
  using AliasList        = IReadOnlyList<IAlias>;
  using RelationshipList = IReadOnlyList<IRelationship>;
  using TagList          = IReadOnlyList<ITag>;
  using UserTagList      = IReadOnlyList<IUserTag>;
  #endif

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Event : IEvent {

    public EntityType EntityType => EntityType.Event;

    [JsonProperty("id", Required = Required.Always)]
    public Guid MbId { get; private set; }

    public AliasList Aliases => this._aliases;

    [JsonProperty("aliases", Required = Required.DisallowNull)]
    private Alias[] _aliases = null;

    [JsonProperty("annotation", Required = Required.Default)]
    public string Annotation { get; private set; }

    [JsonProperty("cancelled", Required = Required.Always)]
    public bool Cancelled { get; private set; }

    [JsonProperty("disambiguation", Required = Required.Always)]
    public string Disambiguation { get; private set; }

    public ILifeSpan LifeSpan => this._lifeSpan;

    [JsonProperty("life-span", Required = Required.DisallowNull)]
    private LifeSpan _lifeSpan = null;

    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; private set; }

    public IRating Rating => this._rating;

    [JsonProperty("rating", Required = Required.DisallowNull)]
    private Rating _rating = null;

    public RelationshipList Relationships => this._relationships;

    [JsonProperty("relations", Required = Required.DisallowNull)]
    private Relationship[] _relationships = null;

    [JsonProperty("setlist", Required = Required.Always)]
    public string Setlist { get; private set; }

    public TagList Tags => this._tags;

    [JsonProperty("tags", Required = Required.DisallowNull)]
    private Tag[] _tags = null;

    [JsonProperty("time", Required = Required.Always)]
    public string Time { get; private set; }

    [JsonProperty("type", Required = Required.AllowNull)]
    public string Type { get; private set; }

    [JsonProperty("type-id", Required = Required.AllowNull)]
    public Guid? TypeId { get; private set; }

    public IUserRating UserRating => this._userRating;

    [JsonProperty("user-rating", Required = Required.DisallowNull)]
    private UserRating _userRating = null;

    public UserTagList UserTags => this._userTags;

    [JsonProperty("user-tags", Required = Required.DisallowNull)]
    private UserTag[] _userTags = null;

    public override string ToString() {
      var text = this.Name ?? string.Empty;
      if (!string.IsNullOrEmpty(this.Disambiguation))
        text += " (" + this.Disambiguation + ")";
      if (this.Type != null)
        text += " (" + this.Type + ")";
      return text;
    }

  }

}