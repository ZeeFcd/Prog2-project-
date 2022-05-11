using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ija9wq_felevesfeladat_2
{
    
    class Program
    {
        static void Ertesites(LancoltListaGen<Feladat> maradekfeladat, LancoltListaMunkaero<Munkaero> munkatarsak) //eventre feliratkozó függvény, ami kiírja a consolera a ki nem adott feladatokat és a szabad munka erőt
        {
            Console.Clear();
            Console.WriteLine("Feladat kiosztás megtörtént!");
            Console.WriteLine("Kioszott feladatok a munkásokkal a 'markiosztott.txt' ban találhatóak meg!");
            Console.WriteLine("Kimaradt feladatok és munkások, a kimaradt txt-kben találhatóak meg!");

            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("Feladatok, amik nem lettek kiosztva:");
            Listakiiras<Feladat>(maradekfeladat);
            if (maradekfeladat.Darabszam==0)
            {
                Console.WriteLine("Nem maradt kiosztandó munka!");
            }
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("Maradék szabad munkaerő:");
            bool vanszabadmunkaero = false; // bool  arra az esetre ha nem  maradt szabad munkaero

            foreach (Munkaero munkaero in munkatarsak)
            {
                if (munkaero.Elvegezendo.Darabszam==0)
                {
                    Console.WriteLine(munkaero.ToString());
                    vanszabadmunkaero = true;
                }
               
            }
            if (!vanszabadmunkaero) // ha nincs szabad akkor írja is ki
            {
                Console.WriteLine("Nem maradt szabad munkaerő!");
            }



        }
        static void Listakiiras<T>(LancoltListaGen<T> lista) where T:IComparable
        {
            foreach (T item in lista)
            {
                Console.WriteLine(item.ToString()); ;
            }
        }

        static void Main(string[] args)
        {
            Nyilvantartas nyilvantart = new Nyilvantartas();
            nyilvantart.kiosztasvegefuggveny += Ertesites;
            
            Console.WriteLine("Üdvözlöm a nyilvántartásban!");
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("Beolvasott feladatok:");
            Listakiiras<Feladat>(nyilvantart.Kiadofeladatok);      
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("Beolvasott munkatársak:");
            Listakiiras<Munkaero>(nyilvantart.Munkatarsak);

            Console.WriteLine();
            Console.WriteLine("Nyomjon ENTER-t a feladatok kiosztásához!");
            Console.ReadLine();

            nyilvantart.FeladatKiosztas();

            Console.WriteLine();
            Console.WriteLine("Nyomjon ENTER-t a kilépéshez!");
            Console.ReadLine();

            ;
        }
    }
}
