using FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Services;
using FRIO.MAR.CROSSCUTTING.Interfaces;
using FRIO.MAR.UI.WEB.SITE.Controllers;
using FRIO.MAR.UI.WEB.SITE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.UI.WEB.SITE.TEST
{
    [TestClass]
    public class AccountControllerTest
    {
        Mock<IUserStore<IdentityUser>> mockStore;
        Mock<UserManager<IdentityUser>> userManager;
        Mock<IStorageService> storageService;
        Mock<IAccountAppService> accountAppService;
        Mock<ILogInfraServices> logInfraServices;

        //Mock<IJwtGenerator> jwt;
        Mock<SignInManager<IdentityUser>> signInManager;
        AccountController accountController;
        //IStorageService storageService, IAccountAppService accountAppService, ILogInfraServices logInfraServices


        public AccountControllerTest()
        {
            mockStore = new Mock<IUserStore<IdentityUser>>();
            storageService = new Mock<IStorageService>();
            accountAppService = new Mock<IAccountAppService>();
            logInfraServices = new Mock<ILogInfraServices>();
            userManager = new Mock<UserManager<IdentityUser>>(mockStore.Object, null, null, null, null, null, null, null, null);
            var identityUser = new Mock<IdentityUser>();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<IdentityUser>>();
            signInManager = new Mock<SignInManager<IdentityUser>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null, null);
            accountController = new AccountController(storageService.Object, accountAppService.Object, logInfraServices.Object);
        }

        [TestMethod]
        public void TryPostLogin()
        {
            //signInManager.Setup(c => c.CheckPasswordSignInAsync(
            //    It.IsAny<IdentityUser>(), It.IsAny<string>(), It.IsAny<bool>())).
            //    ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            //UsuarioLoginPageModel usr = new UsuarioLoginPageModel()
            //{
            //    Usuario = "icuadros@aforo255.com",
            //    Clave = "Aforo255#",
            //    Captcha = "",
            //    Nit = ""
            //};
            //var result = accountController.Login(usr).Result;

            //Assert.IsNotNull(result);
            //Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
    }
}
