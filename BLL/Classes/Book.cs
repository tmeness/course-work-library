using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    [Serializable]
    public class Book
    {
        public string Name { get; set; }
        public Library Library { get; set; }
        public Student Student { get; set; }
        public bool Availability { get; set; } = false;

    }
}
