﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using EletronicPartsCatalog.Features.Projects;
using EletronicPartsCatalog.Infrastructure;
using EletronicPartsCatalog.Infrastructure.Errors;
using EletronicPartsCatalog.UnitTests.Infrastructure;
using Moq;
using Xunit;

namespace EletronicPartsCatalog.UnitTests.Features.Projects
{
    public class EditTest
    {
        [Fact]
        public async Task Edit_Inexistent_Project_WithValidInput_ShouldReturn_RestException()
        {
            var mockedContext = new EletronicPartsCatalogContextMock().GetMockedContextWithData();

            var sut = new Edit.Handler(mockedContext);

            var message = new Edit.Command
            {
                Project = new Edit.ProjectData
                {
                    Title = "Title",
                    Description = "Description",
                    Body = "Body"

                },
                Slug = "title"
            };

            await Assert.ThrowsAsync<RestException>(() => sut.Handle(message, CancellationToken.None));
        }
    }
}
