﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Assessment.Web.Controllers;
using Assessment.Web.Models;
using Moq;
using NUnit.Framework;

namespace Assessment.Web.Tests
{
    [TestFixture]
    class BoardsControllerTests
    {
        [Test]
        public void Constructor_CreatesController()
        {
            var boardRepo = Mock.Of<IBoardRepository>();
            var controller = new BoardsController(boardRepo);
            Assert.NotNull(controller);
        }

        [Test]
        public void GetAll_DoesLookupThroughRepository()
        {
            var boardRepo = new Mock<IBoardRepository>();
            var controller = new BoardsController(boardRepo.Object);

            controller.GetAll();

            boardRepo.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void Find_NegativeId_ThrowsOutOfRangeException()
        {
            var boardRepo = Mock.Of<IBoardRepository>();
            var controller = new BoardsController(boardRepo);
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                controller.Find(-1);
            });
        }

        [Test]
        public void Find_ZeroId_ThrowsOutOfRangeException()
        {
            var boardRepo = Mock.Of<IBoardRepository>();
            var controller = new BoardsController(boardRepo);
            Assert.Throws<ArgumentOutOfRangeException>(() => 
            {
                controller.Find(0);
            });
        }

        [Test]
        public void Find_ValidId_DoesLookupThroughRepository()
        {
            var boardRepo = new Mock<IBoardRepository>();
            boardRepo.Setup(x => x.Find(It.IsAny<int>())).Returns(new Board());

            var controller = new BoardsController(boardRepo.Object);

            controller.Find(1);

            boardRepo.Verify(x => x.Find(1), Times.Once);
        }

        [Test]
        public void Delete_InvalidId_ThrowArgumentException()
        {
            var boardRepo = Mock.Of<IBoardRepository>();
            var controller = new BoardsController(boardRepo);
            Assert.Throws<ArgumentException>(() =>
            {
                controller.Delete(6);
            });
        }

        [Test]
        public void Delete_ValidId_VerifyExpectedMethodsInvokecOnlyOnce()
        {
            var boardRepo = new Mock<IBoardRepository>();
            boardRepo.Setup(x => x.Find(It.IsAny<int>())).Returns(new Board());
            boardRepo.Setup(x => x.Delete(It.IsAny<int>())).Returns(true);

            var controller = new BoardsController(boardRepo.Object);

            controller.Delete(3);

            boardRepo.Verify(x => x.Find(3), Times.Once);
            boardRepo.Verify(x => x.Delete(3), Times.Once);
        }

        [Test]
        public void Create_Valid_DoesLookupThroughRepository()
        {
            var boardRepo = new Mock<IBoardRepository>();
            Board board = new Board
            {
                Id = 3
            };

            
        }
    }
}
