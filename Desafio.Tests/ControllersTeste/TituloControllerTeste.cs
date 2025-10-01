using AutoMapper;
using Desafio.Controllers;
using Desafio.Data;
using Desafio.Models;
using Desafio.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Tests
{
    public class TituloControllerTestes
    {
        private IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TituloViewModel, TituloModel>();
                cfg.CreateMap<TituloModel, TituloViewModel>();
                cfg.CreateMap<ParcelaViewModel, ParcelaModel>();
                cfg.CreateMap<ParcelaModel, ParcelaViewModel>();
            });
            return config.CreateMapper();
        }

        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Banco isolado para teste
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task Inserir_TituloValido_RetornaOk()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new TituloController(context, GetMapper());

            var vm = new TituloViewModel
            {
                NomeDevedor = "João Silva",
                CpfDevedor = "01234567890",
                NumeroTitulo = "222",
                PercentualJuros = 1,
                PercentualMulta = 3,
                Parcelas = new List<ParcelaViewModel>
                {
                    new() { NumeroParcela = 1, ValorParcela = 100, DataVencimento = DateTime.Now.AddDays(60) },
                    new() { NumeroParcela = 2, ValorParcela = 200, DataVencimento = DateTime.Now.AddDays(90) }
                }
            };

            // Act
            var result = await controller.Inserir(vm);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var retorno = Assert.IsType<TituloViewModel>(okResult.Value);
            Assert.Equal("João Silva", retorno.NomeDevedor);
            Assert.Equal(2, retorno.Parcelas.Count);
        }

        [Fact]
        public async Task Inserir_TituloComCamposInvalidos_RetornaBadRequest()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new TituloController(context, GetMapper());

            // Criando um TituloViewModel inválido (NomeDevedor vazio)
            var vmInvalido = new TituloViewModel
            {
                NomeDevedor = "", // inválido
                CpfDevedor = "01234567890",
                NumeroTitulo = "999",
                PercentualJuros = 1,
                PercentualMulta = 2,
                Parcelas = new List<ParcelaViewModel>() // vazio também
            };

            // Act
            var result = await controller.Inserir(vmInvalido);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var mensagem = Assert.IsType<string>(badRequestResult.Value);
            Assert.Contains("Erro:", mensagem); // Confirma que a mensagem contém o prefixo de erro do helper
        }

        [Fact]
        public async Task Listar_SemFiltro_RetornaTodosTitulos()
        {
            // Arrange
            using var context = GetDbContext();
            var mapper = GetMapper();

            // Inserir dados de teste
            await context.Titulos.AddRangeAsync(
                new TituloModel
                {
                    NomeDevedor = "João Silva",
                    CpfDevedor = "01234567890",
                    NumeroTitulo = "222",
                    PercentualJuros = 1,
                    PercentualMulta = 3,
                    Parcelas = new List<ParcelaModel>
                    {
                        new() { NumeroParcela = 1, ValorParcela = 525, DataVencimento = DateTime.Now.AddDays(90) }
                    }
                },
                new TituloModel
                {
                    NomeDevedor = "Artur Silva",
                    CpfDevedor = "01234567890",
                    NumeroTitulo = "333",
                    PercentualJuros = 2,
                    PercentualMulta = 6,
                    Parcelas = new List<ParcelaModel>
                    {
                        new() { NumeroParcela = 2, ValorParcela = 2223, DataVencimento = DateTime.Now.AddDays(90) }
                    }
                }
            );
            await context.SaveChangesAsync();

            var controller = new TituloController(context, mapper);

            // Act
            var result = await controller.Listar(null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var titulos = Assert.IsType<List<TituloViewModel>>(okResult.Value);
            Assert.Equal(2, titulos.Count);
        }

        [Fact]
        public async Task Listar_ComFiltro_RetornaApenasTitulosFiltrados()
        {
            // Arrange
            using var context = GetDbContext();
            var mapper = GetMapper();

            // Inserir dados de teste
            await context.Titulos.AddRangeAsync(
                new TituloModel { NomeDevedor = "João Silva", Parcelas = new List<ParcelaModel>() },
                new TituloModel { NomeDevedor = "Artur Silva", Parcelas = new List<ParcelaModel>() }
            );
            await context.SaveChangesAsync();

            var controller = new TituloController(context, mapper);

            // Act
            var result = await controller.Listar("joão");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var titulos = Assert.IsType<List<TituloViewModel>>(okResult.Value);
            Assert.Single(titulos);
            Assert.Contains("João", titulos[0].NomeDevedor);
        }
    }
}