using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using vk_Api_groups;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class GetterTopPostsTests
    {
        //проверяем не всех друзей т к вбивать лень всех
        //тест возможно не отработает если произойдут изменения в друзьях
        private readonly string[] expectArr =
            {
                "1067158", "15124320", "17790509", "18006281", "18171633", "19351792", "19435679",
                "19605288", "19701897"
            };

        [TestMethod]
        public void GetFreindsIdTest()
        {

            var getterPosts = new vk_Api_groups.GetterTopPosts("", "21447539");
            string[] idArr = getterPosts.GetFreindsId("21447539");

            for (var i = 0; i < 9; i++)
                Assert.AreEqual(idArr[i], expectArr[i]);
        }

        [TestMethod]
        public void GetFriendsPostTest()
        {
            var getterPosts2 = new vk_Api_groups.GetterTopPosts("", "21447539");
            List<Post> Posts = getterPosts2.GetFriendsPosts(expectArr, "1");

            List<Post> expectPost = new List<Post>();
            expectPost.Add(new Post("15124320", "10162", "0"));
            expectPost.Add(new Post("17790509", "585", "0"));
            expectPost.Add(new Post("18006281", "2739", "8"));
            expectPost.Add(new Post("18171633", "4054", "150"));
            expectPost.Add(new Post("19351792", "1324", "0"));
            expectPost.Add(new Post("19435679", "1163", "0"));
            expectPost.Add(new Post("19701897", "2943", "30"));


            for (int i = 0; i < expectPost.Count; i++)
            {
                Assert.AreEqual(Posts[i].GetLikes(), expectPost[i].GetLikes());
                Assert.AreEqual(Posts[i].GetPost(), expectPost[i].GetPost());
                Assert.AreEqual(Posts[i].GetUser(), expectPost[i].GetUser());
            }


        }

        [TestMethod]
        public void GetLikesFromHtmlTest()
        {
            var getterPosts = new vk_Api_groups.GetterTopPosts("", "21447539");

            var resp =
            HttpGetter.GET_http("https://api.vk.com/method/wall.get.xml?count=" + "1" + "&owner_id=" +
                                "19351792");//+ "&access_token=" + _token);

            var doc = new HtmlDocument();
            doc.LoadHtml(resp);


            var likes = doc.DocumentNode.SelectNodes("//likes");
            var likesArr = vk_Api_groups.GetterTopPosts.GetLikesArrFromHtml(likes);

            Assert.AreEqual(likesArr[0], "0");
        }

    }
}
