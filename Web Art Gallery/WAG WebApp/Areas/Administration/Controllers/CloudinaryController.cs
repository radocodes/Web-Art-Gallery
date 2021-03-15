using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WAG.Services.Interfaces;

namespace WAG.WebApp.Areas.Administration.Controllers
{
    public class CloudinaryController : AdministrationController
    {
        private readonly ICloudinaryService cloudinaryService;

        public CloudinaryController(ICloudinaryService cloudinaryService, IMapper mapper)
            : base(mapper)
        {
            this.cloudinaryService = cloudinaryService;
        }

        public IActionResult GenerateSignature()
        {
            var timestamp = Request.Query["data[timestamp]"];
            var source = Request.Query["data[source]"];
            var cloudFolder = Request.Query["data[folder]"];

            string signature = this.cloudinaryService.GenerateSignature(timestamp, source, cloudFolder);
            return Ok(signature);
        }
    }
}