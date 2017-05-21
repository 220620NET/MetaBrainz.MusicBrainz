﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Entities;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  using Interface = ISearchResults<IFoundPlace>;
  #if NETFX_GE_4_5
  using Results   = IReadOnlyList<IFoundPlace>;
  #else
  using Results   = IEnumerable<IFoundPlace>;
  #endif

  internal sealed partial class FoundPlaces : SearchResults<IFoundPlace> {

    public FoundPlaces(Query query, string queryString, int? limit = null, int? offset = null) : base(query, "place", queryString, limit, offset) { }

    public override DateTime? Created => this._currentResult?.created;

    public override Results Results => this._currentResult?.results;

    public override int TotalResults => this._currentResult?.count ?? 0;

    public override Interface Next() {
      var json = base.NextResponse(this._currentResult?.results.Length ?? 0);
      this._currentResult = JsonConvert.DeserializeObject<JSON>(json);
      return this;
    }

    public override Interface Previous() {
      var json = base.PreviousResponse();
      this._currentResult = JsonConvert.DeserializeObject<JSON>(json);
      return this;
    }

    #pragma warning disable 169
    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private sealed class JSON {
      [JsonProperty("count",   Required = Required.Always)]       public int       count;
      [JsonProperty("created", Required = Required.DisallowNull)] public DateTime? created;
      [JsonProperty("offset",  Required = Required.Always)]       public int       offset;
      [JsonProperty("places",  Required = Required.Always)]       public Place[]   results;
    }

    #pragma warning restore 169
    #pragma warning restore 649

    private JSON _currentResult;

  }

}
