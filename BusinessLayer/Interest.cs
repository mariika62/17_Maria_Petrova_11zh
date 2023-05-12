using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Interest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public List<User> Users { get; set; }
        public List<Category> Categories { get; set; }
        public Interest()
        {
            Users = new List<User>();
            Categories = new List<Category>();
        }
        public Interest(string name)
        {
            Name = name;
            Users = new List<User>();
            Categories = new List<Category>();
        }
    }
}
