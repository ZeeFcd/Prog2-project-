using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ija9wq_felevesfeladat_2
{
    class Munkaero : IMunkaero,IComparable
    {
        public string Nev { get; set; }
        public KepzetsegEnum Kepzetseg { get; set; }
        public LancoltListaGen<Feladat> Elvegezendo { get ; set; }
        public Munkaero(string nev,int kepzetseg)
        {
            Nev = nev;
            Kepzetseg = (KepzetsegEnum)kepzetseg;
            Elvegezendo = new LancoltListaGen<Feladat>();

        }

        // Munkaero képzetséh enumnál beállítottt int értékeke alapján valósítja meg a Compareto függvényt
        public int CompareTo(object obj)
        {
            Munkaero a = this;
            Munkaero b = obj as Munkaero;

            return a.Kepzetseg.CompareTo(b.Kepzetseg);
        }

        public override string ToString()
        {

            return "Munkaerő neve: " + Nev + "| Kepzettsége: " + Kepzetseg.ToString();
        }



    }

    //Előre definiált munkaerő lehetőségek
    class Auditor :Munkaero
    {
        public Auditor() : base("Auditor", 3) { }
        
                
        
    }
    class Broker : Munkaero
    {
        public Broker() : base("Broker",3) { }




    }
    class Sales: Munkaero
    {
        public Sales() : base("Sales",2) { }




    }
    class Ugyintezo : Munkaero
    {
        public Ugyintezo() : base("Ugyintezo", 2) { }




    }
    class Szabadbolcsesz : Munkaero
    {
        public Szabadbolcsesz() : base("Szabadbolcsesz",3) { }




    }
    class AlkalmazottMatematikus : Munkaero
    {
        public AlkalmazottMatematikus() : base("AlkalmazottMatematikus",4) { }




    }
    class Futar : Munkaero
    {
        public Futar() : base("Futar",1) { }




    }
    class BetanitottMunkas : Munkaero
    {
        public BetanitottMunkas() : base("BetanitottMunkas", 1) { }




    }
    class MuszakiMenedzser : Munkaero
    {
        public MuszakiMenedzser() : base("MuszakiMenedzser", 4) { }




    }
                                                                                                                                                                                                                                                                                                                                                                                                  //When the impostor is sus  
}
