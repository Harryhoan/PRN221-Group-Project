﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using PetSpaBussinessObject;
using PetSpaService.AccountService;
using PRN211GroupProject.Utilities;
using System.Security.Claims;

namespace PRN211GroupProject.Handler
{
    public class CustomAccessDeniedHandler : IAuthorizationHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        [TempData]
        public string errorMessage { get; set; }

        public CustomAccessDeniedHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null)
            {
                // Check if user is logged in
                if (!context.User.Identity.IsAuthenticated)
                {
                     errorMessage = "You must login first";
                    httpContext.Response.Redirect("/Accounts/Login");
                    return Task.CompletedTask;
                }

                // Get required roles from policies
                var requiredRoles = context.PendingRequirements
                    .OfType<RolesAuthorizationRequirement>()
                    .SelectMany(r => r.AllowedRoles)
                    .Distinct()
                    .ToList();

                // Check if user has any of the required roles
                var userRoles = context.User.Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value)
                    .ToList();

                if (requiredRoles.Any(role => userRoles.Contains(role)))
                {
                    httpContext.Response.Redirect("/Errors/AccessDenied"); 
                }
                else
                {
                    httpContext.Response.Redirect("/Errors/Unauthorized"); 
                }
            }

            return Task.CompletedTask;
        }

    }
}
