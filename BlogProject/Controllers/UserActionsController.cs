using BlogProject.Data;
using BlogProject.Entites;
using BlogProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogProject.Controllers
{
    //[Authorize]
    public class UserActionsController : Controller
    {
        private readonly TagRepository _tagRepository;
        private readonly PostRepository _postRepository;
        private readonly UserRepository _userRepository;
        private readonly CommentRepository _commentRepository;
        private readonly IWebHostEnvironment _env;
        public UserActionsController(TagRepository tagRepository, UserRepository userRepository, IWebHostEnvironment env, PostRepository postRepository, CommentRepository commentRepository)
        {
            _tagRepository = tagRepository;
            _userRepository = userRepository;
            _env = env;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> SharePost()
        {

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }


            int userId = int.Parse(userIdClaim);


            var currentUser = await _userRepository.GetById(userId);
            if (currentUser == null)
            {
                return NotFound();
            }

            var tags = await _tagRepository.GetAllAsync();

            var model = new SharePostViewModel
            {
                User = currentUser,
                AllTags = tags,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SharePost(SharePostViewModel sharePostViewModel, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                throw new Exception("Model is not found");
            }

            string filePath = await UploadFileAsync(image);
            List<Tag> tags = new List<Tag>();
            foreach (var item in sharePostViewModel.SelectedTagIds)
            {
                var tag = await _tagRepository.GetById(item);
                tags.Add(tag);
            }
            sharePostViewModel.newPost.Tags = tags;
            sharePostViewModel.newPost.Image = filePath;
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var allUsers = await _userRepository.GetAllAsync();
            var findedUser = allUsers.FirstOrDefault(u => u.Id == int.Parse(userIdClaim));
            sharePostViewModel.User = findedUser;
            if (findedUser == null)
            {
                throw new Exception("Kullanıcı yok");
            }
            sharePostViewModel.newPost.UserId = int.Parse(userIdClaim);
            findedUser.Posts.Add(sharePostViewModel.newPost);
            await _userRepository.UpdateAsync(findedUser);

            return RedirectToAction(actionName: "Profile", controllerName: "Account", new { userId = findedUser.Id });
        }

        [HttpGet]
        public async Task<IActionResult> ContentOfPost(int receivedId)
        {

            var post = await _postRepository.GetById(receivedId);
            var allComments = _commentRepository.GetAllCommentsOfPost(receivedId);
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> SendComment(Comment receivedComment)
        {

            await _commentRepository.CreateAsync(receivedComment);
            return RedirectToAction(actionName: "ContentOfPost", controllerName: "UserActions", new { receivedId = receivedComment.PostId });
        }


        [HttpGet]
        public async Task<IActionResult> UpdatePost(int receivedPostId)
        {
            var post = await _postRepository.GetById(receivedPostId);
            var tags = await _tagRepository.GetAllAsync();
            var model = new UpdatePostViewModel
            {
                Post = post,
                AllTags = tags
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePost(UpdatePostViewModel updatePostViewModel, IFormFile ImageFile)
        {


            var existingPost = await _postRepository.GetById(updatePostViewModel.Post.Id);
            if (existingPost == null)
            {
                return NotFound();
            }

            if (updatePostViewModel.SelectedTagIds!=null)
            {
                var allTags = await _tagRepository.GetAllAsync();
                List<Tag> welcomeTags = new List<Tag>();
                foreach (var item in updatePostViewModel.SelectedTagIds)
                {
                    var tag = allTags.Where(t => t.Id == item).FirstOrDefault();
                    welcomeTags.Add(tag);
                }
                existingPost.Tags = welcomeTags;
            }
            
            existingPost.Header = updatePostViewModel.Post.Header;
            existingPost.Content = updatePostViewModel.Post.Content;
            if (ImageFile!=null)
            {
                existingPost.Image = await UploadFileAsync(ImageFile);

            }
            await _postRepository.UpdateAsync(existingPost);

            return RedirectToAction("Index", "Home");


        }


        private async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads/essays");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return filePath;
            }
            return null;
        }
    }

}
