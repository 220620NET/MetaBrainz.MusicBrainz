﻿namespace MetaBrainz.MusicBrainz.Resources {

  public interface IEvent : IMbEntity, IAnnotatedResource, INamedResource, IRatedResource, IRelatableResource, ITaggedResource, ITypedResource {

    byte? Cancelled { get; }

    ILifeSpan LifeSpan { get; }

    string Setlist { get; }

    string Time { get; }

  }

}