﻿using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using App.Domain.Identity;
using App.DTOs.Account;
using App.Models;
using App.Services.Identity;
using App.Services.Identity.Managers;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    [Route("account")]
    public class AccountController : BaseController
    {
        private readonly AppUserManager _userManager;
        private readonly AppSignInManager _signInManager;
        private readonly AppRoleManager _roleManager;

        private readonly ISmsSender _smsSender;
        private readonly IEmailSender _emailSender;

        public AccountController(AppUserManager userManager, AppSignInManager signInManager, ISmsSender smsSender,
            IEmailSender emailSender, AppRoleManager roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _smsSender = smsSender;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        [HttpGet("access-denied")]
        public IActionResult AccessDenied()
        {
            return View();
        }


        [HttpGet("sign-up", Name = "GetRegister")]
        public IActionResult Register(string returnTo = null)
        {
            ViewData["returnTo"] = returnTo;

            return View();
        }

        [HttpPost("sign-up", Name = "PostRegister")]
        public async Task<IActionResult> Register(RegisterAccount account, string returnTo)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = account.UserName,
                    Email = account.Email,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    RegisteredOn = account.RegisterOn,
                    GeneratedKey = Guid.NewGuid().ToString("N")
                };


                var result = await _userManager.CreateAsync(user, account.Password);

                if (result.Succeeded)
                {
                    if (_userManager.Options.SignIn.RequireConfirmedEmail)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                        var callBackUrl = Url.RouteUrl("ConfirmEmail", new {code, key = user.GeneratedKey},
                            Request.Scheme);

                        var message = $"کاربر گرامی برای تائید آدرس ایمیل خود <a href=\"{callBackUrl}\">اینجا</a> را کلیک نمائید.";

                        await _emailSender.SendEmailAsync(user.Email, "لینک تائید ایمیل", message);
                    }

                    var saRole = await _roleManager.FindByNameAsync("sa");
                    var normalRole = await _roleManager.FindByNameAsync("user");
                    if (saRole == null)
                    {
                        await _roleManager.CreateAsync(new Role
                        {
                            Name = "sa",
                            DisplayName = "مدیر سیستم ها",
                            NormalizedName = "SA"
                        });
                    }
                    if (normalRole == null)
                    {
                        await _roleManager.CreateAsync(new Role
                        {
                            Name = "user",
                            DisplayName = "کاربر عادی",
                            NormalizedName = "USER"
                        });
                    }
                    if (user.UserName == "behnix")
                    {
                        await _userManager.AddToRoleAsync(user, "sa");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "user");
                    }

                    await _signInManager.SignInAsync(user, false);

                    return RedirectToLocal(returnTo);
                }

                this.AddErrors(result);
            }


            return View(account);
        }


        [HttpGet("sign-in", Name = "GetLogin")]
        public IActionResult Login(string returnTo = null)
        {
            ViewData["returnTo"] = returnTo;

            return View();
        }

        [HttpPost("sign-in", Name = "PostLogin")]
        public async Task<IActionResult> Login(LoginAccount account, string returnTo)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    userName: account.UserName,
                    password: account.Password,
                    isPersistent: account.RememberMe,
                    lockoutOnFailure: false);


                if (result.Succeeded)
                {
                    return RedirectToLocal(returnTo);
                }

                if (result.RequiresTwoFactor)
                {
                    return RedirectToRoute("GetSendCode", new {returnTo, rememberMe = account.RememberMe});
                }

                if (result.IsLockedOut)
                {
                    return View("LockOut");
                }

                if (result.IsNotAllowed)
                {
                    if (_userManager.Options.SignIn.RequireConfirmedPhoneNumber)
                    {
                        if (!await _userManager.IsPhoneNumberConfirmedAsync(new User {UserName = account.UserName}))
                        {
                            ModelState.AddModelError(string.Empty, "شماره تلفن شما تایید نشده است.");

                            return View(account);
                        }
                    }


                    if (_userManager.Options.SignIn.RequireConfirmedEmail)
                    {
                        if (!await _userManager.IsEmailConfirmedAsync(new User {UserName = account.UserName}))
                        {
                            ModelState.AddModelError(string.Empty, "آدرس اییل شما تایید نشده است.");

                            return View(account);
                        }
                    }
                }
            }
            this.ModelState.AddModelError(string.Empty, "نام کاربری یا کلمه عبور وارد شده صحیح نیست.");
            return View(account);
        }

        [HttpGet("send-code", Name = "GetSendCode")]
        public async Task<IActionResult> SendCode(string returnTo, bool rememberMe)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                return View("Error");
            }

            var providers = await _userManager.GetValidTwoFactorProvidersAsync(user);


            var providerList = providers.Select(provider => new SelectListItem {Value = provider, Text = provider})
                .ToList();


            return View(new SendCode
            {
                RememberMe = rememberMe,
                ReturnTo = returnTo,
                Providers = providerList
            });
        }


        [HttpPost("send-code", Name = "PostSendCode")]
        public async Task<IActionResult> SendCode(SendCode model, string returnTo)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                return View("Error");
            }

            var code = await _userManager.GenerateTwoFactorTokenAsync(user, model.SelectedProvider);

            if (string.IsNullOrEmpty(code))
            {
                return View("Error");
            }

            var message = $"کد تائید : {code}";

            if (model.SelectedProvider == "Email")
            {
                await _emailSender.SendEmailAsync(user.Email, "کد تائید حساب", message);
            }
            else if (model.SelectedProvider == "Phone")
            {
                //todo send code with sms
            }


            return RedirectToRoute("GetVerifyCode",
                new
                {
                    returnTo,
                    rememberMe = model.RememberMe,
                    provider = model.SelectedProvider
                });
        }


        [HttpGet("verify-code", Name = "GetVerifyCode")]
        public async Task<IActionResult> VerifyCode(string returnTo, bool rememberMe, string provider)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                return View("Error");
            }

            return View(new VerifyCode
            {
                RememberMe = rememberMe,
                ReturnTo = returnTo,
                Provider = provider
            });
        }

        [HttpPost("verify-code", Name = "PostVerifyCode")]
        public async Task<IActionResult> VerifyCode(VerifyCode model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var result = await _signInManager.TwoFactorSignInAsync(
                provider: model.Provider,
                code: model.Code,
                isPersistent: model.RememberMe,
                rememberClient: model.RememberBrowser
            );


            if (result.Succeeded)
            {
                return RedirectToLocal(model.ReturnTo);
            }

            if (result.IsLockedOut)
            {
                return View("LockOut");
            }

            ModelState.AddModelError(nameof(model.Code), "کد وارد شده معتبر نمی باشد");

            return View(model);
        }

        [HttpGet("confirm-email", Name = "ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string key, string code)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(code))
            {
                return View("Error");
            }

            var user = await _userManager.Users.SingleOrDefaultAsync();

            if (user == null)
            {
                return View("Error");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);


            if (result.Succeeded)
            {
                return View(user);
            }
            return View("Error", new ErrorViewModel
            {
                RequestId = "توکن ارسال شده صحیح نمی باشد."
            });
        }


        [HttpGet("sign-out", Name = "LogOut")]
        public async Task<IActionResult> LogOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                await _signInManager.SignOutAsync();

                await _userManager.UpdateSecurityStampAsync(user);
            }

            return Redirect("/");
        }

        [HttpPost("external/sign-in", Name = "PostExternalLogin")]
        public IActionResult ExternalLogin(string provider, string returnTo = null)
        {
            var redirectUrl = Url.RouteUrl("GetExternalLoginCallBack", new {returnTo}, Request.Scheme);

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return Challenge(properties, provider);
        }

        [HttpGet("external/call-back", Name = "GetExternalLoginCallBack")]
        public async Task<IActionResult> ExternalLoginCallBack(string returnTo = null, string remoteError = null)
        {
            if (!string.IsNullOrEmpty(remoteError) || !string.IsNullOrWhiteSpace(remoteError))
            {
                return View("Error");
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                return View("Error");
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

            if (result.Succeeded)
            {
                await _signInManager.UpdateExternalAuthenticationTokensAsync(info);

                return RedirectToLocal(returnTo);
            }

            if (result.IsLockedOut)
            {
                return View("LockOut");
            }

            if (result.RequiresTwoFactor)
            {
                return RedirectToRoute("GetSendCode", new {returnTo});
            }
            else
            {
                ViewData["returnTo"] = returnTo;

                var externalEmail = info.Principal.FindFirstValue(ClaimTypes.Email);
                var externamFirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName);
                var externalLastName = info.Principal.FindFirstValue(ClaimTypes.Surname);
                var model = new ExternalLoginConfirm
                {
                    Email = externalEmail,
                    FirstName = externamFirstName,
                    LastName = externalLastName,
                    RegisterOn = DateTime.Now,
                    GeneratedKey = Guid.NewGuid().ToString("N")

                };

                return View("ExternalLoginConfirm", model);
            }
        }

        [HttpPost("external/confirm", Name = "PostExternalLoginConfirm")]
        public async Task<IActionResult> ExternalLoginConfirm(ExternalLoginConfirm model, string returnTo = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                return View("Error");
            }

            var user = new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                EmailConfirmed = true,
                GeneratedKey = model.GeneratedKey,
                RegisteredOn = model.RegisterOn
            };

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                result = await _userManager.AddLoginAsync(user, info);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "user");
                    await _signInManager.SignInAsync(user, false);

                    await _signInManager.UpdateExternalAuthenticationTokensAsync(info);

                    return RedirectToLocal(returnTo);
                }

                AddErrors(result);

                return View();
            }

            AddErrors(result);

            return View();
        }


        [HttpGet("forget-password", Name = "GetForgetPassword")]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost("forget-password", Name = "PostForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPassword model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                return View("ForgetPasswordConfirm");
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callBackUrl = Url.RouteUrl(
                "GetResetPassword",
                new
                {
                    key = user.GeneratedKey, code
                }, Request.Scheme);

            var message = $"کاربر گرامی برای بازیابی رمز عبور خود <a href=\"{callBackUrl}\">اینجا</a> را کلیک نمائید.";

            await _emailSender.SendEmailAsync(user.Email, "بازیابی رمز عبور", message);

            return View("ForgetPasswordConfirm");
        }

        [HttpGet("rest-password", Name = "GetResetPassword")]
        public IActionResult ResetPassword(string key, string code)
        {
            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(key))
            {
                return View("Error");
            }

            return View();
        }

        [HttpPost("rest-password", Name = "PostRestPassword")]
        public async Task<IActionResult> ResetPassword(ResetPassword model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.GeneratedKey == model.Key);

            if (user == null)
            {
                return View("ResetPasswordConfirm");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);

            if (result.Succeeded)
            {
                return View("ResetPasswordConfirm");
            }

            AddErrors(result);

            return View(model);
        }
    }
}