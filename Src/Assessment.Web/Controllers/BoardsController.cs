using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Assessment.Web.Models;
using Newtonsoft.Json;

namespace Assessment.Web.Controllers
{
    [Route("api/[controller]")]
    public class BoardsController : Controller
    {
        public IBoardRepository boards;

        public BoardsController(IBoardRepository boards)
        {
            this.boards = boards;
        }

        [HttpGet]
        public IEnumerable<Board> GetAll()
        {
            return boards.GetAll();
        }

        [HttpGet]
        [Route("{id:int}")]
        public Board Find(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id), "Board ID must be greater than zero.");

            return boards.Find(id);
        }

        [HttpPost]
        public Board Create([FromBody]Board board)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentNullException("Board name can not be null");
            }
            return boards.Create(board);
        }

        [HttpPut]
        public bool Update(Board board)
        {
            var found = Find(board.Id);
            if (found == null) throw new ArgumentException("Board does not exist");

            return boards.Update(board);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public bool Delete(int id)
        {
            var board = Find(id);
            if (board == null) throw new ArgumentException("Board does not exist");

            return boards.Delete(id);
        }

        [HttpPost]
        [Route("postit/{boardId:int}/{text}")]
        public PostIt CreatePostIt(int boardId, string text)
        {
            var board = Find(boardId);
            if (board == null) throw new ArgumentException("Board does not exist");
            return boards.AddPostIt(boardId, text);
        }


        [HttpDelete]
        [Route("postit/{boardId:int}/{postItId:int}")]
        public bool DeletePostIt(int boardId, int postItId)
        {
            var board = Find(boardId);
            if (board == null) throw new ArgumentException("Board does not exist");

            return boards.DeletePostIt(boardId, postItId);
        }
    }
}
