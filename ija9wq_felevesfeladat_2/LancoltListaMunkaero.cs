using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ija9wq_felevesfeladat_2
{
    //ez specifikusan munka kiosztáshoz készült, munkaerore hajazó lista mert egy lineáris keresést vagy eldönést valósítok meg egyéni feltétellel
    class LancoltListaMunkaero<T>:LancoltListaGen<T> where T : Munkaero,IComparable
    {
        
        public LancoltListaMunkaero():base()
        {

        }

        public bool AlkalmasMunkaeroKereses(Feladat feladat)  //minimálisan alkalmas munkaerő keresése,
        {
            ListaElem<T> p = fej;

            while (p != null && ((int)feladat.Szint > (int)p.Tartalom.Kepzetseg || (int)p.Tartalom.Elvegezendo.Darabszam == 5)) //ciklus feltétel: -ha az éppen aktuálisan vizsgált munkaerő, nem éri el a minimális alkalmasságot, (Szint>Képzettség)
            {                                                                                                                // -vagy a munkaerő képzetsége eléri a a vizsgált elem képzettségét minimálisa, de nem bír már többet válallni (elvégzendőfeladatok lista darbszáma 5)

                p = p.Kovetkezo;
            }
            if (p != null) // talált olyan munka erőt akihez el lehet helyezni a feladatot
            {

                p.Tartalom.Elvegezendo.ElemBeszurasCS(feladat); 
                return true; //visszatérés ahoz kell ,hogy majd kívülrő tudja a kiadofeladat lista ,hogy kitörölheti-e a beoszott munkát
            }
            else
            {
                return false;
            }

        }

    }
}
