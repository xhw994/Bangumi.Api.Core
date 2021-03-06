﻿using System;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Bangumi.Api.Core.Extension
{
    public static class StringExtension
    {
        public static string ReplacePathVariables(this string path, params string[] vars)
        {
            if (vars.Length == 0) return path;
            if (string.IsNullOrEmpty(path)) return string.Empty;

            const char sep = (char)47;
            string[] parts = path.Split(new char[] { sep }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0, vc = 0; i < parts.Length; i++)
            {
                if (parts[i][0] == '{')
                {
                    parts[i] = vars[vc++];
                    if (vc == vars.Length) break;
                }
            }

            return sep + string.Join(sep.ToString(), parts);
        }

        public static string ReplacePathVariables(this string path, params object[] vars)
        {
            return ReplacePathVariables(path, vars.Select(x => ParameterToString(x)).ToArray());
        }

        /// <summary>
        /// If parameter is DateTime, output in a formatted string (default ISO 8601), customizable with Configuration.DateTime.
        /// If parameter is a list of string, join the list with ",".
        /// Otherwise just return the string.
        /// </summary>
        /// <param name="obj">The parameter (header, path, query, form).</param>
        /// <returns>Formatted string.</returns>
        public static string ParameterToString(object obj)
        {
            switch (obj)
            {
                // Deal with this later
                case DateTime _:
                    // Return a formatted date string - Can be customized with Configuration.DateTimeFormat
                    // Defaults to an ISO 8601, using the known as a Round-trip date/time pattern ("o")
                    // https://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#Anchor_8
                    // For example: 2009-06-15T13:45:30.0000000
                    return ((DateTime)obj).ToString("o");
                case List<string> _:
                    return string.Join(",", (obj as List<string>).ToArray());
                default:
                    return Convert.ToString(obj);
            }
        }

        public static string ToUrlEncode(this string source) => HttpUtility.UrlEncode(source);

        public static bool IsHttpOrHttpsUrl(string source) => Uri.TryCreate(source, UriKind.Absolute, out Uri url) && (url.Scheme == Uri.UriSchemeHttp || url.Scheme == Uri.UriSchemeHttps);

        public static bool IsAlphaNumeric(string s) => !string.IsNullOrEmpty(s) && s.All(c => char.IsLetterOrDigit(c) && (c < 128));

        public static bool HaveSameElements(IEnumerable<string> source1, IEnumerable<string> source2)
        {
            if (source1 == null ^ source2 == null) return false;
            if (source1 == null) return true;

            HashSet<string> set = new HashSet<string>();
            foreach (string s in source1)
            {
                if (!set.Contains(s)) set.Add(s);
            }
            foreach (string s in source2)
            {
                if (!set.Contains(s)) return false;
            }
            return true;
        }
    }
}
