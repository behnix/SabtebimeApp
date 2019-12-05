using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using App.Core.Convertors;
using App.Domain.Entities.Post;
using App.Services;
using App.Services.Identity.Managers;
using App.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    [Route("post")]
    [Authorize(nameof(ConstantPolicies.DynamicPermission))]
    public class PostController : BaseController
    {
        private readonly IPostService _postService;
        private readonly IGroupService _groupService;
        private readonly AppUserManager _userManager;
        private readonly IPostTagService _postTagService;

        public PostController(IPostService postService, IGroupService groupService, AppUserManager userManager, IPostTagService postTagService)
        {
            _postService = postService;
            _groupService = groupService;
            _userManager = userManager;
            _postTagService = postTagService;
        }


        [Display(Name = "پست ها")]
        [HttpGet("list", Name = "GetPosts")]
        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllPosts();
            return View(posts);
        }

        [Display(Name = "افزودن پست جدید")]
        [HttpGet("create", Name = "GetCreatePost")]
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_groupService.GetAllGroup(), "GroupId", "GroupTitle");
            ViewData["Author"] = new SelectList(_userManager.Users, "Id", "UserName");
            return View();
        }

        [Display(Name = "افزودن پست جدید")]
        [HttpPost("create", Name = "PostCreatePost")]
        public async Task<IActionResult> Create(Post post, IFormFile postImageUp, string tags)
        {
            var currentUser = await _userManager.Users.SingleOrDefaultAsync(u=>u.UserName == User.Identity.Name);
            post.Author = Convert.ToInt32(currentUser.Id);
            if (!ModelState.IsValid)
                return View(post);

            if (await _postService.IsPostTitleInBrowserRepeated(TextConvertor.ReplaceLetters(TextConvertor.FixingText(post.PostTitleInBrowser), ' ', '-')))
            {
                ModelState.AddModelError("Post.PostTitleInBrowser", "قبلا مطلبی با این عنوان برای مرورگر ثبت شده است!");
                ViewData["GroupId"] = new SelectList(_groupService.GetAllGroup(), "GroupId", "GroupTitle");
                ViewData["Author"] = new SelectList(_userManager.Users, "UserId", "UserName");
                return View();
            }

            await _postService.AddPost(post, postImageUp, tags);

            return RedirectToRoute("GetPosts");
        }

        [Display(Name = "ویرایش پست")]
        [HttpGet("edit", Name = "GetEditPost")]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var post = await _postService.GetPostById(id);

            if (post == null)
            {
                return NotFound();
            }

            ViewData["PostImage"] = post.PostImage ?? "no-image.png";
            post.PostTitleInBrowser = TextConvertor.ReplaceLetters(post.PostTitleInBrowser, '-', ' ');
            ViewData["GroupId"] = new SelectList(_groupService.GetAllGroup(), "GroupId", "GroupTitle");
            var tagsPost = "";
            foreach (var tag in _postTagService.GetAllTagsByPostId(id))
                tagsPost += tag.Tag.TagTitle + ",";

            ViewData["Tags"] = tagsPost.Length == 0 ? null : tagsPost.Substring(0, tagsPost.Length - 1);
            return View(post);
        }

        [Display(Name = "ویرایش پست")]
        [HttpPost("edit", Name = "PostEditPost")]
        public async Task<IActionResult> Edit(Post post, IFormFile postImageUp, string tags, string oldImage)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }

            await _postService.UpdatePostAsync(post, postImageUp, tags, oldImage);

            try
            {
                await _postService.SaveChangeAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(post.PostId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToRoute("GetPosts");
        }

        [Display(Name = "حذف پست")]
        [HttpGet("remove",Name = "GetRemovePost")]
        public async Task<IActionResult> Remove(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postService.GetPostById(id);

            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [Display(Name = "حذف پست")]
        [HttpPost("remove", Name = "PostRemovePost")]
        public async Task<IActionResult> Remove(Post post)
        {
            var selectedPost = await _postService.GetPostById(post.PostId);

            if (selectedPost != null)
            {
                await _postService.RemovePost(post.PostId);
            }

            return RedirectToRoute("GetPosts");
        }

        [Display(Name = "آرشیو پست")]
        [HttpPost("archive", Name = "PostArchivePost")]
        public async Task<IActionResult> Archive(int id)
        {
            await _postService.ArchivePost(id);
            return RedirectToRoute("GetPosts");
        }

        [Display(Name = "انتشار پست")]
        [HttpPost("publish", Name = "PostPublishPost")]
        public async Task<IActionResult> Publish(int id)
        {
            await _postService.PublishPost(id);
            return RedirectToRoute("GetPosts");
        }

        private bool PostExists(int id)
        {
            return _postService.PostExists(id);
        }
    }
}
