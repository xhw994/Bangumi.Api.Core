using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Bangumi.Api.Core.Model.Subjects
{
    /// <summary>
    /// 条目类型: 1 - Book, 2 - Anime, 3 - Music, 4 - Game, 5 - Real
    /// </summary>
    [DataContract]
    public enum SubjectType
    {
        [Description("book")]
        Book = 1,
        [Description("anime")]
        Anime = 2,
        [Description("music")]
        Music = 3,
        [Description("game")]
        Game = 4,
        [Description("real")]
        Real = 6
    }
}
