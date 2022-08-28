using FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.DomainServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Services;
using FRIO.MAR.CROSSCUTTING.Interfaces;
using FRIO.MAR.UI.WEB.SITE.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.UI.WEB.SITE.TEST
{
    [TestClass]
    public class ProductosControllerTest
    {
        ProductosController productosController;
        Mock<IStorageService> storageService;
        Mock<IBodegaRepository> bodegaRepository;
        Mock<IInventarioDomainService> inventarioDomainService;
        Mock<ISucursalRepository> sucursalRepository;
        Mock<IUtilidadRepository> utilidadRepository; 
        Mock<IProductoAppService> ProductoAppService;
        Mock<ILogInfraServices> logInfraServices;  
        public ProductosControllerTest()
        {
            storageService = new Mock<IStorageService>();
            bodegaRepository = new Mock<IBodegaRepository>();
            inventarioDomainService = new Mock<IInventarioDomainService>();
            sucursalRepository = new Mock<ISucursalRepository>();
            utilidadRepository = new Mock<IUtilidadRepository>();
            ProductoAppService = new Mock<IProductoAppService>();
            logInfraServices = new Mock<ILogInfraServices>();

            productosController = new ProductosController(
                storageService.Object,
                bodegaRepository.Object,
                inventarioDomainService.Object,
                sucursalRepository.Object,
                utilidadRepository.Object,
                ProductoAppService.Object,
                logInfraServices.Object
                );
        }

        [TestMethod]
        public void TryGetProducto()
        {
            var result = productosController.Index();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IActionResult));

        }
    }
}
