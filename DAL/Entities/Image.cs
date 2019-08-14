using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Picture
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
    }
}
