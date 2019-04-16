using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Bangumi.Api.Core.Model.Definitions
{
    /// <summary>
    /// 条目类型: 1 - Book, 2 - Anime, 3 - Music, 4 - Game, 5 - Real
    /// </summary>
    [DataContract]
    public enum SubjectType
    {
        Book = 1,
        Anime = 2,
        Music = 3,
        Game = 4,
        Real = 5
    }
}
