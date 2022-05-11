using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ija9wq_felevesfeladat_2
{
    class NincsKepzetsegException:Exception // Továbbképzés szüksége esetén
    {
        public Munkaero Munkatars { get; }
        public NincsKepzetsegException(Munkaero munkaero):base("Nem elég a munkaerő képzetsége, tovább képzés szükséges!")
        {
            Munkatars = munkaero;
        }
    }

    class NincsIlyenOsztalyException:Exception //ha olyan elem van a kiolvasott fájlokban ami nem odavaló 
    {
        public string Elem { get; }
        public NincsIlyenOsztalyException(string elem):base("Nincs ilyen feladat/munka osztály")
        {
            Elem = elem;
        }
    }
    
}
