using Bangumi.Api.Core.Model.Subjects;
using Bangumi.Api.Core.Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Bangumi.Api.Core.Extension
{
    public static class EnumExtensions
    {
        public static string ToDescriptionString(this Enum val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
                .GetType()
                .GetField(val.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        public static string ToCnName(this SubjectType subjectType)
        {
            switch (subjectType)
            {
                case SubjectType.Book:
                    return "书籍";
                case SubjectType.Anime:
                    return "动画";
                case SubjectType.Music:
                    return "音乐";
                case SubjectType.Game:
                    return "游戏";
                case SubjectType.Real:
                    return "三次元";
                default:
                    throw new ArgumentException($"The given {nameof(SubjectType)} value is out of range");
            }
        }
    }
}
