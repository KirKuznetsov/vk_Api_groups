using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace vk_Api_groups
{
    public class GetterTopPosts
    {
        public GetterTopPosts(string token, string userId)
        {
            _token = token;
            _userId = userId;
        }

        public void FindTop10Posts()
        {
            var usersIdArray = GetFreindsId(_userId);
            var posts = GetFriendsPosts(usersIdArray, "1");
            posts.Sort(new CompareByNumberLikes());
            WriteTopPostToFile(@"Top10Posts.txt", posts);
        }

        private static void WriteTopPostToFile(string fileName, IList<Post> posts)
        {
            try
            {
                var textFile = new System.IO.StreamWriter(fileName);
                for (var i = 0; i < 10; i++)
                    textFile.WriteLine(posts[i]);
            
                textFile.Close();
            }
            catch (Exception)
            {
                { }
                throw;
            }
       
        }
        /// <summary>
        /// получает id всех друзей
        /// </summary>
        /// <returns></returns>
        public string[] GetFreindsId(string userId)
        {
            var resp = HttpGetter.GET_http("https://api.vk.com/method/friends.get?user_id=" + _userId + "&access_token=" + _token);
            var usersIdArr = resp.Split(',');
            //эти 2 строки некорректно отработает если id другого размера
            //надо переделать регулярками
            usersIdArr[0] = usersIdArr[0].Substring(13, 7);
            usersIdArr[usersIdArr.Length - 1] = usersIdArr[usersIdArr.Length - 1].Substring(0, 9);
            return usersIdArr;
        }

        /// <summary>
        /// парсит лайки
        /// </summary>
        /// <param name="likes"></param>
        /// <returns>кол-во лйаков записи</returns>
        public static List<string> GetLikesArrFromHtml(HtmlNodeCollection likes)
        {
            const string pattern = @"<count>(\d+)</count>";
            return likes.Select(like => Regex.Matches(like.InnerHtml, pattern, RegexOptions.IgnoreCase)).Select(match => match[0].Groups[1].Value).ToList();
        }

        /// <summary>
        /// Возвращает записи со стены пользователя
        /// </summary>
        /// <param name="friendsId">чьи записи вернуть</param>
        /// <param name="countPosts">сколько</param>
        /// <returns></returns>
        public List<Post> GetFriendsPosts(IEnumerable<string> friendsId, string countPosts)
        {
            var posts = new List<Post>();

            foreach (var frinedId in friendsId)
            {
                var resp = HttpGetter.GET_http("https://api.vk.com/method/wall.get.xml?count=" + countPosts + "&owner_id=" + frinedId + "&access_token=" + _token);

                var doc = new HtmlDocument();
                doc.LoadHtml(resp);

                var id = doc.DocumentNode.SelectNodes("//id");
                var likes = doc.DocumentNode.SelectNodes("//likes");

                if (likes == null) continue;
                var likesArr = GetLikesArrFromHtml(likes);

                var count = 0;
                foreach (var postId in id)
                {
                    posts.Add(new Post(frinedId, postId.InnerHtml, likesArr[count]));
                    count++;
                }
            }
            return posts;
        }

        private readonly string _token;
        private readonly string _userId;

    }
}