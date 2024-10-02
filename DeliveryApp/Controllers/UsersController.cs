﻿using DeliveryApp.DTO.UserDTO;
using DeliveryApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace DeliveryApp.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("getAllUsers")]
        [Authorize(Roles = "Admin")]
        public ActionResult GetUsers()
        {
            try
            {
                return Ok(_userService.GetUsers());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("currentProfile")]
        public ActionResult GetProfile()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("User ID not found in token.");
                }

                var user = _userService.FindById(long.Parse(userId));

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest("An error occurred while retrieving the profile.");
            }
        }

        [HttpPost]
        [Route("editProfile")]
        [Authorize]
        public ActionResult EditProfile([FromBody] UserDto user)
        {
            if (_userService.ModifyUser(user))
                return Ok(true);

            return BadRequest();
        }

        [HttpGet]
        [Route("getUnactivated")]
        [Authorize(Roles = "Admin")]
        public ActionResult Unactivated()
        {
            List<UserDto> ret = _userService.Unactivated();
            return Ok(ret);
        }

        [HttpPost]
        [Route("verifyUser")]
        [Authorize(Roles = "Admin")]
        public ActionResult VerifyUser(long id)
        {
            bool ret = _userService.VerifyUser(id);
            return Ok(ret);

        }

        [HttpPost]
        [Route("rejectUser")]
        [Authorize(Roles = "Admin")]
        public ActionResult RejectUser(long id)
        {
            bool ret = _userService.DismissUser(id);
            return Ok(ret);

        }


    }
}
