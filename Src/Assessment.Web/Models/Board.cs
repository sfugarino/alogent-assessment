using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Assessment.Web.Models
{
    public class Board
    {
        private List<PostIt> _postIts = new List<PostIt>();

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        public PostIt[] PostIts
        {
            get
            {
                return _postIts.ToArray();
            }
            set
            {
                if (value == null)
                    _postIts = new List<PostIt>();
                else
                    _postIts = value.ToList<PostIt>();
            }
        }

        public void AddPostIt(PostIt postIt)
        {
            int id = 1;
            if (_postIts.Count() != 0)
                id = _postIts.Select(p => p.Id).Max() + 1;
            postIt.Id = id;
            _postIts.Add(postIt);

        }

        public bool DeletePostIt(int postItId)
        {
            PostIt postIt = _postIts.SingleOrDefault(p => p.Id == postItId);

            if (postIt == null)
                throw new ArgumentException("PostIt not found");

            return _postIts.Remove(postIt);

        }
    }
}
