using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Assessment.Web.Models
{
    public interface IBoardRepository
    {
        IQueryable<Board> GetAll();
        Board Find(int id);
        Board Create(Board board);
        bool Add(Board board);
        bool Update(Board board);
        bool Delete(int id);
        PostIt AddPostIt(int boardId, string text);
        bool DeletePostIt(int boardId, int postItId);
    }

    public class BoardRepository : IBoardRepository
    {
        private List<Board> boards;

        public BoardRepository()
        {
            boards = GetBoardsFromFile();
        }

        protected virtual List<Board> GetBoardsFromFile()
        {
            var filePath = Application.Configuration["DataFile"];
            if (!Path.IsPathRooted(filePath)) filePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);

            var json = System.IO.File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<List<Board>>(json);
        }

        public IQueryable<Board> GetAll()
        {
            return boards.AsQueryable();
        }

        public Board Find(int id)
        {
            return boards.FirstOrDefault(x => x.Id == id);
        }

        public Board Create(string name)
        {
            if (String.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name can not be null, empty or whitespace");

            int id = boards.Count == 0 ? 1 : boards.Select(b => b.Id).Max() + 1;
            Board board = new Board();
            board.Name = name;
            return Create(board);
        }


        public Board Create(Board board)
        {
            if (String.IsNullOrWhiteSpace(board.Name)) throw new ArgumentException("Name can not be null, empty or whitespace");
            int id = boards.Select(b => b.Id).Max() + 1;
            board.Id = id;
            board.Name = board.Name;
            board.CreatedAt = DateTime.Now;
            boards.Add(board);

            return board;
        }

        public bool Add(Board board)
        {
            if (String.IsNullOrWhiteSpace(board.Name))
                throw new ArgumentException("Name can not be null, empty or whitespace");

            if (Find(board.Id) != null) return false;

            boards.Add(board);

            return true;
        }

        public PostIt AddPostIt(int boardId, string text)
        {
            if (String.IsNullOrWhiteSpace(text))
                throw new ArgumentException("PostIt text can not be null, empty or whitespace");

            Board board = Find(boardId);

            if (board == null)
                throw new ArgumentException("Board not found");

            PostIt postIt = new PostIt
            {
                Text = text,
                CreatedAt = DateTime.Now
            };

            board.AddPostIt(postIt);

            return postIt;
        }

        public bool Update(Board board)
        {
            if(String.IsNullOrWhiteSpace(board.Name))
                throw new ArgumentException("Name can not be null, empty or whitespace");

            var found = Find(board.Id);

            if (found == null) return false;

            found.Id = board.Id;
            found.CreatedAt = board.CreatedAt;
            found.Name = board.Name;
            found.PostIts = board.PostIts;

            return true;
        }

        public bool Delete(int id)
        {
            var board = Find(id);
            if (board == null) return false;

            return boards.Remove(board);
        }

        public bool DeletePostIt(int boardId, int postItId)
        {
            Board board = Find(boardId);

            if (board == null)
                throw new ArgumentException("Board not found");


            return board.DeletePostIt(postItId);
        }
    }
}
