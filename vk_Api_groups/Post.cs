namespace vk_Api_groups
{
    public class Post
    {
        public Post(string idUser, string idPost, string likes)
        {
            _idUser = idUser;
            _idPost = idPost;
            _likes = likes;
        }
      
        public string GetLikes()
        {
            return _likes;
        }

        public string GetPost()
        {
            return _idPost;
        }
        public string GetUser()
        {
            return _idUser;
        }
        private readonly string _idUser;
        private readonly string _idPost;
        private readonly string _likes;

        public override string ToString()
        {
            return "User id: " + _idUser + " Post id: " + _idPost + " Likes: " + _likes;
        }
    }
}