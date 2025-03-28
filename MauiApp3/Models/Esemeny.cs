using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3.Models
{
    public partial class Esemeny
    {
        public int Id { get; set; }

        public string Cime { get; set; } = null!;

        public string Helyszin { get; set; } = null!;

        public DateTime Datum { get; set; }

        public string? Leiras { get; set; }

        public string Kepurl { get; set; } = null!;

    }

}
