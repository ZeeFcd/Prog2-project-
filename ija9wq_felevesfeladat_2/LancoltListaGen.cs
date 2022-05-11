using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ija9wq_felevesfeladat_2
{
    class ListaElem<T>
    {
        public T Tartalom { get; set; }
        public ListaElem<T> Kovetkezo { get; set; }
    }
    class LancoltListaGen<T> : IEnumerable where T : IComparable //Generikus Láncolt lista csökennő vagynövekvő sorrendbe való beszúrással, ienumeratorral való bejárással
    {
        public LancoltListaGen()
        {
            Darabszam = 0;
        }
        protected ListaElem<T> fej;
        public int Darabszam { get; private set; } //hány darab elem van benne minden alkalommal amikor beszúrunk vagy torlünk változik
        

        

        public void ElemBeszurasCS(T tartalom) //csökkenő sorrendbe beszúrás
        {
            ListaElem<T> uj = new ListaElem<T>();
            uj.Tartalom = tartalom;
            ListaElem<T> p = fej;
            ListaElem<T> e = null;
                     
            while (p != null && p.Tartalom.CompareTo(tartalom)>0)
            {
                e = p;
                p = p.Kovetkezo;
            }
            if (e == null)
            {
                uj.Kovetkezo = fej;
                fej = uj;
                Darabszam++;

            }
            else
            {
               uj.Kovetkezo = p;
               e.Kovetkezo = uj;
                Darabszam++;
            }          

        }
        public void ElemBeszurasN(T tartalom) //növekvő sorrendbe beszúrás, munkaerő tömbnél kell majd
        {
            ListaElem<T> uj = new ListaElem<T>();
            uj.Tartalom = tartalom;
            ListaElem<T> p = fej;
            ListaElem<T> e = null;

            while (p != null && p.Tartalom.CompareTo(tartalom) < 0)
            {
                e = p;
                p = p.Kovetkezo;
            }
            if (e == null)
            {
                uj.Kovetkezo = fej;
                fej = uj;
                Darabszam++;
            }
            else
            {
                uj.Kovetkezo = p;
                e.Kovetkezo = uj;
                Darabszam++;
            }




        }
        public void ElemTorles(T torlendo)  //elem törlése , akkor lesz szükség rá amikor kiosztunk egy feladatot és találunk neki munkaerőt
        {
            ListaElem<T> p = fej;
            ListaElem<T> e = null;
            while (p != null && !p.Tartalom.Equals(torlendo))
            {
                e = p;
                p = p.Kovetkezo;
            }
            if (p != null)
            {
                if (e == null)
                {
                    fej = p.Kovetkezo;
                    Darabszam--;
                }
                else
                {
                    e.Kovetkezo = p.Kovetkezo;
                    Darabszam--;
                }
            }
            

        }
        



        public IEnumerator GetEnumerator() //bejáráshoz megvalósítja az ienumerablet
        {
            return new ListaBejaro<T>(fej);
        }
    }

    //gyakorlat videó alapján elkészített enumerator
    class ListaBejaro<T> : IEnumerator
    {
        ListaElem<T> fej;
        ListaElem<T> aktualis;

        public ListaBejaro(ListaElem<T> fej)
        {
            this.fej = fej;
            this.aktualis = new ListaElem<T>();
            this.aktualis.Kovetkezo = fej;
        }

        public object Current
        {
            get
            {
                return aktualis.Tartalom;
            }
        }
               

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            if (aktualis==null)
            {
                return false;
            }
            aktualis = aktualis.Kovetkezo;
            return aktualis != null;
        }

        public void Reset()
        {
            aktualis = new ListaElem<T>();
            aktualis.Kovetkezo = fej;
        }
    }

}
