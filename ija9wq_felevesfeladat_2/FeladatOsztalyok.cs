using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ija9wq_felevesfeladat_2
{
    class Feladat : IIFeladat, IComparable
    {
        public string Nev { get; set; }
        public FeladatszintEnum Szint { get; set; }
        public Feladat(string nev,int szint) 
        {
            Nev = nev;
            Szint = (FeladatszintEnum)szint; 
        }

        // feladatszint enumnál beállítottt int értékeke alapján valósítja meg a Compare  to függvényt
        public int CompareTo(object obj) 
        {
            Feladat a = this;
            Feladat b = obj as Feladat;

            return a.Szint.CompareTo(b.Szint);
        }

        public override string ToString()
        {

            return "Feladat: " + Nev + "| Szint: " + Szint.ToString();
        }
    }

    //Előre definiált feladat lehetőségek
    class Audit : Feladat
    {
        public Audit() : base("Audit", 3) { }
               
    }
    class ErtekpapirKereskedes : Feladat
    {
        public ErtekpapirKereskedes() : base("ErtekpapirKereskedes", 3) { }

    }
    class Ugyfelszolgalat : Feladat
    {
       public Ugyfelszolgalat() : base("Ugyfelszolgalat", 2) { }

    }
    class Ertekesites : Feladat
    {
        public Ertekesites() : base("Ertekesites", 2) { }

    }
    class Gyartas : Feladat
    {
        public Gyartas() : base("Gyartas", 1) { }

    }
    class Kiszallitas : Feladat
    {
        public Kiszallitas() : base("Kiszallitas", 1) { }


    }
    class KutatasVezetes : Feladat
    {
        public KutatasVezetes() : base("KutatasVezetes", 4) { }


    }
    class Infrastrukturafejlesztes: Feladat
    {
        public Infrastrukturafejlesztes() : base("Infrastrukturafejlesztes", 4) { }


    }


}
