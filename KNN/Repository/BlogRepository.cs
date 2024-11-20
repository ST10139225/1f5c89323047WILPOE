using KNN.Data;
using KNN.Interfaces;
using KNN.Mappers;
using KNN.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using KNN.DTOs;

using Humanizer;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Reflection.Metadata;
using Google.Apis.YouTube.v3.Data;
using Google;
using System.ComponentModel.Design;


namespace KNN.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly ApplicationDbContext _blogRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public BlogRepository(ApplicationDbContext context)
        {
            _blogRepo = context;
        }

    

        public async Task DeleteBlog(int? Id)
        {
            if (Id == null || Id == 0)
            {

            }
            Blog article_to_delete = await GetById(Id);

            _blogRepo.Blogs.Remove(article_to_delete);

            await SaveChangesAsync();
        }

        public   IEnumerable<Blog> GetAllAsync(ApplicationUser applicationUser)
        {
            return   _blogRepo.Blogs.Include(blog=> blog.Creator)
                .Include(blog => blog.Posts)
                .Where(blog => blog.Creator == applicationUser);
        } 
        
        //For searchse
        public   IEnumerable<Blog> GetAllAsync(string searchString)
        {
            return   _blogRepo.Blogs.Include(blog=> blog.Creator)
                .Include(blog => blog.Posts)
                .Where(blog => blog.Title.Contains(searchString)|| blog.Content.Contains(searchString));
        }

        public async Task<Blog> UpdateBlog(int Id, BlogCreateDTO updatedBlog)
        {
            var existingBlog = await _blogRepo.Blogs.SingleOrDefaultAsync(x => x.Id == Id);

                if (existingBlog == null)
                return null;

            _blogRepo.Entry(existingBlog).CurrentValues.SetValues(updatedBlog);

            await SaveChangesAsync();

            return existingBlog;



        }


        public async Task<Blog> GetById(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
                return await _blogRepo.Blogs.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _blogRepo.SaveChangesAsync();
        }

        public async Task<Blog> Add(Blog newBlog)
        {
            await _blogRepo.Blogs.AddAsync(newBlog);
            await SaveChangesAsync();


            return newBlog;
        }

        public async Task<Blog> Update(Blog newBlog)
        {
            _blogRepo.Update(newBlog);
            await _blogRepo.SaveChangesAsync();
            return newBlog;
        }

        public  Blog GetBlog(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
                return   _blogRepo.Blogs.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Blog> GetAllAsyncHome()
        {

            return _blogRepo.Blogs.Include(blog => blog.Creator)
                .Include(blog => blog.Posts)
                .Where(blog => blog.Published == true);
        }


        // Comments funcitonality

        public async Task<Post> Add(Post comment)
        {
            _blogRepo.Add(comment);
            await _blogRepo.SaveChangesAsync();
            return comment;
        }

        public Post GetComment(int commentId)
        {
            return _blogRepo.Posts
                .Include(comment => comment.poser)
                .Include(comment => comment.blog)
                .Include(comment => comment.Parent)
                .FirstOrDefault(comment => comment.Id == commentId);
        }

    }
}