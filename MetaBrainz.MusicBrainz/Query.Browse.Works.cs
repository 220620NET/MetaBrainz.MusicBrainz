using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using MetaBrainz.Common;
using MetaBrainz.MusicBrainz.Interfaces;
using MetaBrainz.MusicBrainz.Interfaces.Browses;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Browses;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

  /// <summary>Returns the works associated with the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose works should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested works.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IWork> BrowseAllArtistWorks(Guid mbid, int? pageSize = null, int? offset = null,
                                                            Include inc = Include.None)
    => new BrowseWorks(this, Query.BuildExtraText(inc, "artist", mbid), pageSize, offset).AsStream();

  /// <summary>Returns the works in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained works should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested works.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IWork> BrowseAllCollectionWorks(Guid mbid, int? pageSize = null, int? offset = null,
                                                                Include inc = Include.None)
    => new BrowseWorks(this, Query.BuildExtraText(inc, "collection", mbid), pageSize, offset).AsStream();

  /// <summary>Returns the works associated with the given artist.</summary>
  /// <param name="artist">The artist whose works should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested works.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IWork> BrowseAllWorks(IArtist artist, int? pageSize = null, int? offset = null,
                                                      Include inc = Include.None)
    => new BrowseWorks(this, Query.BuildExtraText(inc, "artist", artist.Id), pageSize, offset).AsStream();

  /// <summary>Returns the works in the given collection.</summary>
  /// <param name="collection">The collection whose contained works should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested works.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IWork> BrowseAllWorks(ICollection collection, int? pageSize = null, int? offset = null,
                                                      Include inc = Include.None)
    => new BrowseWorks(this, Query.BuildExtraText(inc, "collection", collection.Id), pageSize, offset).AsStream();

  /// <summary>Returns (the specified subset of) the works associated with the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose works should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IWork> BrowseArtistWorks(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None)
    => AsyncUtils.ResultOf(this.BrowseArtistWorksAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the works associated with the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose works should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IWork>> BrowseArtistWorksAsync(Guid mbid, int? limit = null, int? offset = null,
                                                            Include inc = Include.None,
                                                            CancellationToken cancellationToken = default)
    => new BrowseWorks(this, Query.BuildExtraText(inc, "artist", mbid), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the works in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained works should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IWork> BrowseCollectionWorks(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None)
    => AsyncUtils.ResultOf(this.BrowseCollectionWorksAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the works in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained works should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IWork>> BrowseCollectionWorksAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                Include inc = Include.None,
                                                                CancellationToken cancellationToken = default)
    => new BrowseWorks(this, Query.BuildExtraText(inc, "collection", mbid), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the works associated with the given artist.</summary>
  /// <param name="artist">The artist whose works should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IWork> BrowseWorks(IArtist artist, int? limit = null, int? offset = null, Include inc = Include.None)
    => AsyncUtils.ResultOf(this.BrowseWorksAsync(artist, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the works in the given collection.</summary>
  /// <param name="collection">The collection whose contained works should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IWork> BrowseWorks(ICollection collection, int? limit = null, int? offset = null,
                                           Include inc = Include.None)
    => AsyncUtils.ResultOf(this.BrowseWorksAsync(collection, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the works associated with the given artist.</summary>
  /// <param name="artist">The artist whose works should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IWork>> BrowseWorksAsync(IArtist artist, int? limit = null, int? offset = null,
                                                      Include inc = Include.None, CancellationToken cancellationToken = default)
    => new BrowseWorks(this, Query.BuildExtraText(inc, "artist", artist.Id), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the works in the given collection.</summary>
  /// <param name="collection">The collection whose contained works should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IWork>> BrowseWorksAsync(ICollection collection, int? limit = null, int? offset = null,
                                                      Include inc = Include.None, CancellationToken cancellationToken = default)
    => new BrowseWorks(this, Query.BuildExtraText(inc, "collection", collection.Id), limit, offset).NextAsync(cancellationToken);

}
