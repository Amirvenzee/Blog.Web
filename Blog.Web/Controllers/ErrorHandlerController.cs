﻿using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Blog.Web.Controllers
{
    public class ErrorHandlerController : Controller
    {
        [Route("ErrorHandler/{code}")]
        public IActionResult Index(int code)
        {
            switch (code)
            {
                case 404:
                    return View("NotFound");
                case 500:
                    return View("ServerError");

            }
            return View("NotFound");
        }
    }
}