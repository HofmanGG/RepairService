using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_PL.Infrastructure;
using HelloSocNetw_PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static HelloSocNetw_PL.Infrastructure.ControllerExtensions;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserInfoService _userInfoService;
        private readonly IMapper _mapper;
        private readonly string _picturesDirectory;

        public UsersController(IUserInfoService userInfoService, IMapper mapper)
        {
            _userInfoService = userInfoService;
            _mapper = mapper;
        }

        //GET: api/Users/5
        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetUserByIdAsync(int userId)
        {
            var userInfoDto = await _userInfoService.GetUserInfoByIdAsync(userId);
            var userInfoModel = _mapper.Map<UserInfoModel>(userInfoDto);
            return Ok(userInfoModel);
        }

        // GET: api/Users/
        [HttpGet]
        public async Task<IActionResult> GetUsersAsync(int from)
        {
            var usersInfoDto = await _userInfoService.GetUsersInfoAsync(from, 10);
            var userModels = _mapper.Map<IEnumerable<UserInfoModel>>(usersInfoDto);
            return Ok(userModels);
        }

        [HttpGet]
        public async Task<IActionResult> AddFriendByFriendId(int friendId)
        {
            var userId = this.GetUserId();

            await _userInfoService.AddFriendByUsersIdAsync(userId, friendId);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFriendByFriendId(int friendId)
        {
            var userId = this.GetUserId();

            await _userInfoService.DeleteFriendshipByUserIdAndFriendIdAsync(userId, friendId);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> UnsubscribeByUserId(int userId)
        {
            var subId = this.GetUserId();

            await _userInfoService.DeleteSubscriptionByUserIdAndSubIdAsync(userId, subId);
            return Ok();
        }

        // DELETE: api/Users/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{userId}")]
        public IActionResult Delete(int userId)
        {
            _userInfoService.DeleteUserInfoByUserIdAsync(userId);
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("pictures")]
        public async Task<IActionResult> AddPictureAsync([FromForm]PictureModel pictureModel)
        {
            if (pictureModel != null)
            {
                string ext = Path.GetExtension(pictureModel.Path);
                if (ext == ".jpg" || ext == ".png" || ext == ".bmp")
                {
                    pictureModel.Path = Path.Combine(_picturesDirectory, Path.GetFileName(pictureModel.Path));

                    var userId = this.GetUserId();

                    var pictureDto = _mapper.Map<PictureDTO>(pictureModel);
                    await _userInfoService.AddPictureByUserIdAsync(userId, pictureDto);

                    return Ok();
                }

                return BadRequest();
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("{userId}/pictures/{pictureId}")]
        public async Task<IActionResult> GetPictureByUserIdAndPictureIdAsync(int userId, int pictureId)
        {
            var pictureDto = await _userInfoService.GetPictureByUserIdAndPictureIdAsync(userId, pictureId);
            var pictureModel = _mapper.Map<PictureModel>(pictureDto);
            return Ok(pictureModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("{userId}/pictures/count")]
        public async Task<IActionResult> GetCountOfPicturesByUserIdAsync(int userId)
        {
            return Ok(await _userInfoService.GetCountOfPicturesByUserIdAsync(userId));
        }
    }
}