﻿using System.Collections.Generic;
using System.Linq;
using WordPressPCL.Models;

namespace Phoenix.DataHandle.WordPress.Wrappers
{
    public static class CategoriesWrapper
    {
        private static IEnumerable<Category> Categories { get; set; }

        static CategoriesWrapper() => Categories = WordPressClientWrapper.GetCategoriesAsync().Result;

        public static int GetCategoryId(PostCategory category) => Categories.Single(c => c.Name == category.GetName()).Id;
    }
}