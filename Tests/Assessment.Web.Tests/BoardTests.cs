using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Assessment.Web.Controllers;
using Assessment.Web.Models;
using Moq;
using NUnit.Framework;

namespace Assessment.Web.Tests
{
    [TestFixture]
    class BoardTests
    {
        [Test]
        public void Board_Valid()
        {

            Board board = new Board();
            board.Name = "Test";

            var context = new ValidationContext(board, null, null);
            var result = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(board, context, result, true);

            Assert.IsTrue(isValid);
        }

        [Test]
        public void Board_NullName()
        {

            Board board = new Board();

            var context = new ValidationContext(board, null, null);
            var result = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(board, context, result, true);

            Assert.IsFalse(isValid);
        }

        [Test]
        public void Board_ValidLengthName()
        {
            Random random = new Random();
            const string AllowedChars =
                "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            string name = new string(Enumerable.Repeat(AllowedChars, 150)
                    .Select(s => s[random.Next(s.Length)]).ToArray());

            Board board = new Board();
            board.Name = name;

            var context = new ValidationContext(board, null, null);
            var result = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(board, context, result, true);

            Assert.IsTrue(isValid);
        }

        [Test]
        public void Board_InvalidLengthName()
        {
            Random random = new Random();
            const string AllowedChars = 
                "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            string name = new string(Enumerable.Repeat(AllowedChars, 151)
                    .Select(s => s[random.Next(s.Length)]).ToArray());

            Board board = new Board();
            board.Name = name;

            var context = new ValidationContext(board, null, null);
            var result = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(board, context, result, true);

            Assert.IsFalse(isValid);
        }
    }
}
