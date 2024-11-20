using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using KNN.Data;
using KNN.Interfaces;
using KNN.Models;
using KNN.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KNN.Controllers
{
    [AllowAnonymous]
    public class YoutubeController : Controller 
    {

        private readonly IYouTubeBusinessManager _youtubeBusinessManager;

        public YoutubeController(IYouTubeBusinessManager adminBusinessManager)
        {
            _youtubeBusinessManager = adminBusinessManager;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _youtubeBusinessManager.GetYoubeVideos());
        } 


        [HttpGet]
        public async Task<IActionResult> GetChannelVideos(string? pageToken, int maxResults=4)
        {
            var YouTubeService = new YouTubeService(new BaseClientService.Initializer
            {
                ApiKey = "AIzaSyAJUI6xRbSeESp6lHGWlCqD3QCqEPcqyS4",
                ApplicationName = "WIL POE"
            });


            var SearchReq = YouTubeService.Search.List("snippet");
            SearchReq.ChannelId = "UCXqfkW3aBdT4xRh3v_Un6cw";
            SearchReq.Order= SearchResource.ListRequest.OrderEnum.Date;
            SearchReq.MaxResults =maxResults; 
            SearchReq.PageToken = pageToken;    

            var SearchRes = await SearchReq.ExecuteAsync();

            var video_List =  SearchRes.Items.Select(video => new VideoModel
            {
                Title = video.Snippet.Title,
                Link = $"https://www.youtube.com/watch?v={video.Id.VideoId}",
                Thumbnail = video.Snippet.Thumbnails.Medium.Url,
                PublishedDate = video.Snippet.PublishedAtDateTimeOffset
                

            })
                .OrderByDescending(video=>video.PublishedDate)
                .ToList();

            var response = new VideoResultModel
            {
                Videos = video_List,
                NextPageToken = SearchRes.NextPageToken,
                PrevPageToken = SearchRes.PrevPageToken
            };

            foreach (var video in video_List)
            {
                if(_youtubeBusinessManager.VideoExists(video.Link)==false)
                await _youtubeBusinessManager.Add(video); ;
            }

            return RedirectToAction("Index");


        }
    }
}
