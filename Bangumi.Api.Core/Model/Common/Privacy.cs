using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Bangumi.Api.Core.Model
{
    /// <summary>
    /// 收藏隐私
    /// </summary>
    [DataContract]
    public enum Privacy
    {
        [Description("public")]
        Public = 0,
        [Description("private")]
        Private = 1
    }
}
