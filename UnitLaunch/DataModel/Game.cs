using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace UnitLaunch.DataModel
{
    public class Game
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [Key]
        public string FilePath { get; set; }
        public string? ImagePath { get; set; }

    }
}
