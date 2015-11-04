using System;
using System.Collections.Generic;

namespace vk_Api_groups
{
    public class CompareByNumberLikes : IComparer<Post>
    {
        public int Compare(Post x, Post y)
        {
            if (Convert.ToInt32(x.GetLikes()) < Convert.ToInt32(y.GetLikes()))
                return 1;
            if (Convert.ToInt32(x.GetLikes()) > Convert.ToInt32(y.GetLikes()))
                return -1;
            else
                return 0;
        }
    }
}