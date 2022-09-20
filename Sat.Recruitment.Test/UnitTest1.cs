using System;
using System.Dynamic;

using Microsoft.AspNetCore.Mvc;

using Sat.Recruitment.Api.Controllers;
using SAT.Recruitment.Business.Validation;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            var request = new New.Execute();
            request.Name = "Leon Kennedy";
            request.Email = "leon.kenedy@umbrella.corp.com";
            request.Address = "Raccon CIty";
            request.Phone = "89895478";
            request.UserType = "SuperUser";
            request.Money = 500;

            var manejador = new New.Handler();

            var userResult = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.True(userResult.IsSuccess);
        }

        [Fact]
        public async void Test2()
        {
            var request = new New.Execute();
            request.Name = "Dave Grada";
            request.Email = "david.grada@gmail.com";
            request.Address = "Centrika Victoria 32";
            request.Phone = "5544112212";
            request.UserType = "Normal";
            request.Money = 200;

            var manejador = new New.Handler();

            var userResult = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.True(!userResult.IsSuccess);
        }
    }
}
