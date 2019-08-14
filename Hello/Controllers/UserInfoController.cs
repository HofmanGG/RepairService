using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
/*
namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserInfoService _userInfoService;
        private readonly IMapper _mapper;

        public UserInfoController(IUserInfoService userInfoService, IMapper mapper)
        {
            _userInfoService = userInfoService;
            _mapper = mapper;
        }

        [HttpGet]
        public UserInfoModel GetUserInfoById(int id)
        {
            var userInfo = _userInfoService.GetUserInfo(id);
            var userInfoModel = _mapper.Map<UserInfoModel>(userInfo);
            return userInfoModel;
        }

        // GET: api/UserInfo/5
        [HttpGet("{id}", Name = "Get")]
        public string SendFriendRequestById(int friendId)
        {
            _userInfoService.Subscribe(int userToSubscribeId, friendId);
        }

        // POST: api/UserInfo
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/UserInfo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
*/