using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Comment
    {

        public Comment()
        {

        }



        public Comment(int id, int issuedId, string description, DateTime created)
        {
            Id = id;
            IssueId = issuedId;
            Description = description;
            Created = created;
        }

        public int Id { get; set; }
        public int IssueId { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
    }

}
