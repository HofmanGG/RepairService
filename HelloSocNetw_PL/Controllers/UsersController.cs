using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using AutoMapper;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_PL.Infrastructure;
using HelloSocNetw_PL.Models;
using Microsoft.AspNetCore.Authorization;
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

        // POST: api/Users/
        [HttpPost]
        public async Task<IActionResult> GetUsersAsync([FromBody]int from)
        {
            var usersInfoDto = await _userInfoService.GetUsersInfoAsync(from, 10);
            var userModels = _mapper.Map<IEnumerable<UserInfoModel>>(usersInfoDto);
            return Ok(userModels);
        }

        [HttpGet]
        public async Task<IActionResult> AddFriendByFriendId(int subId)
        {
            var userId = this.GetUserId();

            await _userInfoService.AddFriendByUserIdAndSubIdAsync(userId, subId);
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

        /*[HttpPost]
        [Authorize]
        [Route("avatar")]
        public async Task<IActionResult> AddAvatarAsync()
        {
            string imageName = null;
            var httpRequest = System.Web.HttpContext.Current.Request;

            var postedFile = httpRequest.Files["Image"];

            imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
            var filePath = HttpContext.Current.Server.MapPath("~/Image/" + imageName);
            postedFile.SaveAs(filePath);
        }*/


        [HttpGet]
        [Route("{userId}/friends/count")]
        public async Task<IActionResult> GetCountOfFriendsByUserId(int userId)
        {
            return Ok(await _userInfoService.GetCountOfFriendsByUserIdAsync(userId));
        }

        [HttpGet]
        [Route("{userId}/subscribers/count")]
        public async Task<IActionResult> GetCountOfSubscribersByUserId(int userId)
        {
            return Ok(await _userInfoService.GetCountOfSubscribersByUserIdAsync(userId));
        }

        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> GetCountOfUsersAsync()
        {
            return Ok(await _userInfoService.GetCountOfUsersInfoAsync());
        }

        [HttpPost]
        [Route("{userId}/friends")]
        public async Task<IActionResult> GetFriendsAsync(int userId, int toTake)
        {
            var friendsDto = await _userInfoService.GetFriendsByUserIdAsync(userId, toTake);
            var friendModels = _mapper.Map<IEnumerable<UserInfoModel>>(friendsDto);
            return Ok(friendModels);
        }

        [HttpPost]
        [Route("{userId}/subscribers")]
        public async Task<IActionResult> GetSubsAsync(int userId, int toTake)
        {
            var subsDto = await _userInfoService.GetSubsByUserIdAsync(userId, toTake);
            var subModels = _mapper.Map<IEnumerable<UserInfoModel>>(subsDto);
            return Ok(subModels);
        }
    }   
}