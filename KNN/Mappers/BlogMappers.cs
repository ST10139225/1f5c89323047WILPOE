using KNN.DTOs;
using KNN.Models;

namespace KNN.Mappers
{
    public static class BlogMappers
    {
        public static Blog BlogDTOtoModel(BlogCreateDTO DTO)
        {
            Blog blog = new Blog
            {
                Content = DTO.Content, 
                Posts = DTO.Posts,
                Published = DTO.Published,
                Title = DTO.Title

            };

            return blog;

        }
    }
}
