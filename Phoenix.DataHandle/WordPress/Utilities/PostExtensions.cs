﻿using System.Web;
using WordPressPCL.Models;

namespace Phoenix.DataHandle.WordPress.Utilities
{
    public static class PostExtensions
    {
        public const string PrimaryDelimiter = "~_~";
        public const string SecondaryDelimiter = "~-~";

        public static string GetTitle(this Post post)
        {
            //Attention to successive dashes in WP. They are rendered as single unicode character (e.g. --- -> '\u2014')
            return HttpUtility.HtmlDecode(post.Title.Rendered);
        }
    }
}
