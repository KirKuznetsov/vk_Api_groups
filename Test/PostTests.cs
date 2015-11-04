using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using vk_Api_groups;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class PostTests
    {
        [TestMethod]
        public void CounstructorTest()
        {
            Post post = new Post("15124320", "10162", "0");
            Assert.AreEqual(post.GetLikes(), "0");
            Assert.AreEqual(post.GetPost(), "10162");
            Assert.AreEqual(post.GetUser(), "15124320");
        }
        [TestMethod]
        public void PostToStringTest()
        {
            Post post = new Post("15124320","10162","0");
            Assert.AreEqual(post.ToString(), "User id: 15124320 Post id: 10162 Likes: 0");
        }

    }
}
