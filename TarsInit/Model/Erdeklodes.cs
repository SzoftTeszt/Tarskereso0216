using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarsInit.Model
{
    public class Erdeklodes
    {
        public Erdeklodes()
        {
        }
        public Erdeklodes(string line)
        {
            Nev = line.Split(";")[1];
        }

        public int Id { get; set; }
        public string Nev { get; set; }

        public ICollection<Profilerdeklodes> Profilerdeklodessek { get; set; }

        public override string? ToString()
        {
            return $"{Nev}";
        }
    }
}
