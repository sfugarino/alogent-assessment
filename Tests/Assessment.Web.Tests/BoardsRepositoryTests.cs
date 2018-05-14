using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Assessment.Web.Controllers;
using Assessment.Web.Models;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace Assessment.Web.Tests
{
    [TestFixture]
    class BoardsRepositoryTests
    {
        Mock<BoardRepository> _repository = null;

        [SetUp]
        public void Setup()
        {
            var boards = new List<Board>
            {
                new Board{ Id = 1, CreatedAt = DateTime.Now.AddDays(-7), Name = "Board #1" },
                new Board{ Id = 2, CreatedAt = DateTime.Now.AddDays(-2), Name = "Board #2" },
                new Board{ Id = 3, CreatedAt = DateTime.Now.AddDays(-1), Name = "Board #3" },
                new Board{ Id = 4, CreatedAt = DateTime.Now.AddDays(-1), Name = "Board #4" },
                new Board{ Id = 5, CreatedAt = DateTime.Now.AddDays(-1), Name = "Board #5" }
            };

            _repository = new Mock<BoardRepository>();
            _repository.Protected()
                .Setup<List<Board>>("GetBoardsFromFile")
                .Returns(boards);

        }

        [Test]
        public void GatAllTest_ReturnsIQueryable()
        {
            var boards = _repository.Object.GetAll();
            Assert.IsInstanceOf<IQueryable>(boards);
        }

        [Test]
        public void GatAllTest_ReturnsCorrectNumberOfElements()
        {
            var boards = _repository.Object.GetAll();
            Assert.AreEqual(boards.Count(), 5);
        }

        [Test]
        public void Create_CreateFromBoard()
        {
            Board board = new Board { Name = "Test" };
            Board newBoard = _repository.Object.Create(board);
            Assert.AreEqual(newBoard.Id, 6);
            Assert.AreNotEqual(newBoard.CreatedAt, DateTime.MinValue);
        }

        [Test]
        public void Create_CreateFromBoard_NullName()
        {
            Board board = new Board();
            Assert.Throws<ArgumentException>(() =>
            {
                Board newBoard = _repository.Object.Create(board);
            });
        }

        [Test]
        public void Create_CreateFromName()
        {
            string name = "Test";
            Board newBoard = _repository.Object.Create(name);
            Assert.AreEqual(newBoard.Id, 6);
            Assert.AreNotEqual(newBoard.CreatedAt, DateTime.MinValue);
        }

        [Test]
        public void Create_CreateFromNamed_NullName()
        {
            string name = null;
            Assert.Throws<ArgumentException>(() =>
            {
                Board newBoard = _repository.Object.Create(name);
            });
        }

        [Test]
        public void Delete_DeleteExistingItem()
        {
            bool result = _repository.Object.Delete(3);
            Assert.IsTrue(result);
        }

        [Test]
        public void Delete_DeleteNonExistingItem()
        {
            bool result = _repository.Object.Delete(6);
            Assert.IsFalse(result);
        }

        [Test]
        public void Update_UpdateWithVaildName()
        {
            Board board = new Board { Id = 5, CreatedAt = DateTime.Now.AddDays(-1), Name = "Updated Board #5" };

            bool result = _repository.Object.Update(board);
            Assert.IsTrue(result);

            Board updatedBoard = _repository.Object.Find(5);
            Assert.AreEqual(updatedBoard.Name, "Updated Board #5");
        }

        [Test]
        public void Update_UpdateWithNullName()
        {
            Board board = new Board { Id = 5, CreatedAt = DateTime.Now.AddDays(-1) };
            Assert.Throws<ArgumentException>(() =>
            {
                bool result = _repository.Object.Update(board);
            });  
        }
    }
}
