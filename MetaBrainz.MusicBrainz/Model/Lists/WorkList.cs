﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class WorkList : ItemList, IResourceList<IWork> {

    [XmlElement("work")] public Work[] Items;

    #region IResourceList<IWork>

    uint? IResourceList<IWork>.Count => this.ListCount;

    uint? IResourceList<IWork>.Offset => this.ListOffset;

    IEnumerable<IWork> IResourceList<IWork>.Items => this.Items;

    #endregion

  }

}
