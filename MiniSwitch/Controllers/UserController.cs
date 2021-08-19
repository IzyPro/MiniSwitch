using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniSwitch.DTOs;
using MiniSwitch.Models;
using MiniSwitch.Services;
using MiniSwitch.Services.TransactionsServices;
using MiniSwitch.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniSwitch.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private IUserServices _userService;
        private IMapper _mapper;
        private ITransactionService _transactionService;
        private UserManager<User> _userManager;

        public UserController(IUserServices userService, IMapper mapper, ITransactionService transactionService, UserManager<User> userManager)
        {
            _userService = userService;
            _mapper = mapper;
            _transactionService = transactionService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var currentUserID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(currentUserID);
            var totaltransactionAmount = await _transactionService.FetchAll();
            ViewBag.TotalTransactionAmount = totaltransactionAmount.Sum(a => a.Amount);
            return View(user);
        }


        [HttpGet]
        public IActionResult MiniSwitchs()
        {
            return View();
        }

        public async Task<IActionResult> GetUserProfile()
        {
            var (result, user) = await _userService.GetUserProfileAsync();
            if (result.isSuccess && user != null)
            {
                var userProfile = _mapper.Map<UserProfileResponseDTO>(user);
                return View(userProfile);
            }
            else
                return View(result);
        }

        public async Task<IActionResult> UpdateProfile([FromForm] UserProfileViewModel model)
        {
            var (result, user) = await _userService.UpdateProfileAsync(model);
            if (result.isSuccess && user != null)
            {
                var userProfile = _mapper.Map<UserProfileResponseDTO>(user);
                return View(userProfile);
            }
            else
                return View(result);
        }
    }
}
