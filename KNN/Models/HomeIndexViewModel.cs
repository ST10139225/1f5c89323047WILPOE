using PageList;

namespace KNN.Models
{
    public class HomeIndexViewModel
    {

        public PageList<Blog> Blogs{ get; set; }
        public string  searchString { get; set; }
        public int PageNumber { get; set; }
    }
}
