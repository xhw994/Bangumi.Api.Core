using Bangumi.Api.Core.Model.Subject;
using Bangumi.Api.Core.Model.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Bangumi.Api.Core.Extension
{
    public static class EnumExtension
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

        public static string ToCnName(this CollectionStatus status, SubjectType subjectType)
        {
            switch (status)
            {
                case CollectionStatus.Wish:
                    switch (subjectType)
                    {
                        case SubjectType.Book:
                            return "想读";
                        case SubjectType.Anime:
                        case SubjectType.Real:
                            return "想看";
                        case SubjectType.Music:
                            return "想听";
                        case SubjectType.Game:
                            return "想玩";
                        default:
                            throw new ArgumentException($"The given {nameof(SubjectType)} value is out of range");
                    }
                case CollectionStatus.Collect:
                    switch (subjectType)
                    {
                        case SubjectType.Book:
                            return "读过";
                        case SubjectType.Anime:
                        case SubjectType.Real:
                            return "看过";
                        case SubjectType.Music:
                            return "听过";
                        case SubjectType.Game:
                            return "玩过";
                        default:
                            throw new ArgumentException($"The given {nameof(SubjectType)} value is out of range");
                    }
                case CollectionStatus.Do:
                    switch (subjectType)
                    {
                        case SubjectType.Book:
                            return "在读";
                        case SubjectType.Anime:
                        case SubjectType.Real:
                            return "在看";
                        case SubjectType.Music:
                            return "在听";
                        case SubjectType.Game:
                            return "在玩";
                        default:
                            throw new ArgumentException($"The given {nameof(SubjectType)} value is out of range");
                    }
                case CollectionStatus.OnHold:
                    return "搁置";
                case CollectionStatus.Dropped:
                    return "抛弃";
                default:
                    throw new ArgumentException($"The given {nameof(CollectionStatus)} value is out of range");
            }
        }
    }
}
