using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ija9wq_felevesfeladat_2
{
    class Nyilvantartas
    {
        public delegate void KioszotasVegeHandler(LancoltListaGen<Feladat> maradekfeladat, LancoltListaMunkaero<Munkaero> munkatarsak); //delegált típus, kiadatlan feladat és munkák értésítéséhez
        public event KioszotasVegeHandler kiosztasvegefuggveny; // esemeény az értesítéshez

        int minimalisszint; // ez tárolja, legalacsonyabb szintű feladat szintjét
        LancoltListaGen<Feladat> kiadofeladatok; //csökkenő sorrendbe rendezve
        LancoltListaMunkaero<Munkaero> munkatarsak;//növekvő sorrendbe rendezve
        public LancoltListaGen<Feladat> Kiadofeladatok { get { return kiadofeladatok; }set { kiadofeladatok = value; } }
        public LancoltListaMunkaero<Munkaero> Munkatarsak { get { return munkatarsak; } set { munkatarsak = value; } }

        public Nyilvantartas()
        {
            minimalisszint = 5; // azért 5 mert csak 4 szint van(egyfajta strázsa) és ez azt jelenti, hogy nincs munkafeladat bekérve
            FajlBeolvasFeladatok();
            FajlBeolvasMunkatarsak();
        }

        //Feladat lista létrehozásához tartozó függvények
        #region
        private void FajlBeolvasFeladatok() //beolvassa az ujfeladatokat és ,ha már volt használva a program, a korábban ki nem osztott feladatokat is
        {
            kiadofeladatok = new LancoltListaGen<Feladat>(); //inicializálás
            string[] ujfeladatok = System.IO.File.ReadAllLines("ujfeladatok.txt"); //string tömb amibe a soronként egy feladat neve található
            string[] kimaradtfeladatok = System.IO.File.ReadAllLines("kimaradtfeladatok.txt");// többszöri elindításnál szükség lesz rájuk, ha monjuk egy előző alkalom után maradt kiadatlan
            Feladat tempfeladat; //ideiglenes feladat referencia

            //tömbök bejárása, elvégezendő feladatok beszúrása listába és minimális feladat szint meghatározása(kétszer kell mert az új és a régi)
            for (int i = 0; i < ujfeladatok.Length; i++)
            {
                tempfeladat = FeladatOsztaly(ujfeladatok[i]); // meghívjuk a FeladatOsztaly függvényt ami string alapján besorolja valamelyik feladatclassba
                                                              //(teszt magamnak: nemkezelt kivétel ha olyan szöveg van fájl egyik sorában amit nem kezel a FeladatOsztaly )
                kiadofeladatok.ElemBeszurasCS(tempfeladat); // listába beszúrás
                 
                if (minimalisszint>(int)tempfeladat.Szint) //min kiválasztás
                {
                    minimalisszint = (int)tempfeladat.Szint;
                }
            }
            for (int i = 0; i < kimaradtfeladatok.Length; i++)// ugyanaz mint az előző csak a kimaradt fájllal
            {
                tempfeladat = FeladatOsztaly(kimaradtfeladatok[i]);
                kiadofeladatok.ElemBeszurasCS(tempfeladat);
                if (minimalisszint > (int)tempfeladat.Szint)
                {
                    minimalisszint = (int)tempfeladat.Szint;
                }
            }                   
            
            
        }
        private Feladat FeladatOsztaly(string feladatneve) //megadott stringek alapján, a megfelelő objektumot állítja elő
        {
            switch (feladatneve)
            {
                case "Audit":
                    return new Audit();
                case "ErtekpapirKereskedes":
                    return new ErtekpapirKereskedes();
                case "Ugyfelszolgalat":
                    return new Ugyfelszolgalat();
                case "Ertekesites":
                    return new Ertekesites();
                case "Gyartas":
                    return new Gyartas();
                case "Kiszallitas":
                    return new Kiszallitas();
                case "KutatasVezetes":
                    return new KutatasVezetes();
                case "Infrastrukturafejlesztes":
                    return new Infrastrukturafejlesztes();
                default:
                    throw new NincsIlyenOsztalyException(feladatneve); // feladatneve string nem egyezik senkivel(elírás a fájlban) csak segítségnek hagyom itt
                    
            }
        }
        #endregion

        //Munkaero lista létrehozásához tartozó függvények
        #region
        private void FajlBeolvasMunkatarsak() // nagyon hasnló függvény mint a  FajlBeolvasFeladatok(), kicsi változtatással
        {
            munkatarsak = new LancoltListaMunkaero<Munkaero>();
            string[] ujmunkasok = System.IO.File.ReadAllLines("ujmunkasok.txt"); //új munkások
            string[] kimaradtmunkasok = System.IO.File.ReadAllLines("kimaradtmunkasok.txt"); //akik újak voltak egy előző alkalomnál de nem kaptak munkát
            string[] markiosztot = System.IO.File.ReadAllLines("markiosztott.txt");//akik kaptak munkát de 5 nél kevesebbet szóval még ruházhatunk rájuk

            Munkaero tempmunka; 
            //tömbök bejárása, munkaerők beszúrása listába 
            for (int i = 0; i < ujmunkasok.Length; i++)
            {
                tempmunka = MunkaeroOsztaly(ujmunkasok[i]);// (tesztelésre magamnak)itt nemkezelt kivétel tud keletkezni, ha a fájl egyik sorában olyan szöveg van amit nem kezel le a munkaosztály függvény       

                try // try catch block a képzetlenség elkapására
                {
                   MunkaeroSzintVizsgalat(tempmunka);
                }
                catch (Exception )
                {
                    munkatarsak.ElemBeszurasN(TovabbKepzes()); // beszúrjuk a listába a már biztos megfelelő képzetségű munkaerőt

                } 
                
            }
            for (int i = 0; i < kimaradtmunkasok.Length; i++)// ugyanaz mint az előző csak a kimaradt munkákkal
            {
                tempmunka = MunkaeroOsztaly(kimaradtmunkasok[i]);
                try
                {
                    MunkaeroSzintVizsgalat(tempmunka);
                }
                catch (Exception )
                {
                    munkatarsak.ElemBeszurasN(TovabbKepzes());

                }

            }

            for (int i = 0; i < markiosztot.Length; i++)
            {
                string[] tempstringtomb = markiosztot[i].Split(';', '/'); // tom[0] eleme a munkatárs, a többi a már meglévő feladatai, kivéve az utolsó,mert az egy üres string,mert egy sor /-rel végződik
                tempmunka = MunkaeroOsztaly(tempstringtomb[0]);
                // !!!az utolsó indexig azért nem megyünk mert a split után lesz egy üres tömb elemünk. (tempstringtomb.Length-2 index lesz az utolsó amit feldolgozunk)
                for (int j = 1; j < tempstringtomb.Length-1; j++) // a temp tomb nulladik eleme a munkatárs tehát, az első indextől az utolsó előttiig, hozzá adjuk a munkatárshoz a feladatait  
                {
                    tempmunka.Elvegezendo.ElemBeszurasCS(FeladatOsztaly(tempstringtomb[j]));
                }

                munkatarsak.ElemBeszurasN(tempmunka);
                
            }




        }
        private Munkaero MunkaeroOsztaly(string munkaeroneve) 
        {
            switch (munkaeroneve)
            {
                case "Auditor":
                    return new Auditor();
                case "Broker":
                    return new Broker();
                case "Sales":
                    return new Sales();
                case "Ugyintezo":
                    return new Ugyintezo();
                case "Szabadbolcsesz":
                    return new Szabadbolcsesz();
                case "AlkalmazottMatematikus":
                    return new AlkalmazottMatematikus();
                case "Futar":
                    return new Futar();
                case "BetanitottMunkas":
                    return new BetanitottMunkas();
                case "MuszakiMenedzser":
                    return new MuszakiMenedzser();
                default:
                    throw new NincsIlyenOsztalyException(munkaeroneve); // munkaero string nem egyezike senkivel(elírás a fájlban)

            }
        }
        private void MunkaeroSzintVizsgalat(Munkaero munkatars) // ez fogja vizsgálni, hogy egy munkaerő rendelkezik-e a megfelelő képzetséggel
        {
            if ((int)munkatars.Kepzetseg<minimalisszint)  // haa munkatars nem éri el a minimális szintet, akkor dobunk egy exceptiont (tesztelés magamnak: el is mentjük egy kivételbe a biztonság kedvééért)
            {
                throw new NincsKepzetsegException(munkatars);
            }
            else
            {
                munkatarsak.ElemBeszurasN(munkatars); // ha minden rendben akkor beszúrjuk a munkaerőt a listába
            }

        }
        private Munkaero TovabbKepzes() //vissza ad egy olyan munkaerőt, ami rendelkezik a minimális képzetséggel, ez olyan mintha "továbbképeztük" volna, így a meglévő classokból kozül olyan kerül a nyilvántartásba ami megfelel a szintnek
        {
            int random;
            switch (minimalisszint)// minimalis szint alapján 3 esetet lehet megkülönböztetni
            {                       //1 azért nincs itt caseként mert,ha a minimális szint 1, akkor  ez a függvény nem fog meghívódni, mert nincs olyan,hogy valakinek 0 a képzetsége(Minden munka erőnek legalább 1 a képzetsége,tehát 1 szintű munkát bárki meg kaphat)
                case 2:             //Lényege,hogy annyi elemű tömbe, ahány class van egy szinten, előre eltárolom a példányokat és egy random szám indexxel lekérem a tömbből
                    random = RandomGenerator.rnd.Next(0,2);
                    Munkaero[] munkak2= new Munkaero[2];
                    munkak2[0] = new Sales();
                    munkak2[1] = new Ugyintezo();
                    return munkak2[random];

                case 3:
                    random = RandomGenerator.rnd.Next(0, 3);
                    Munkaero[] munkak3 = new Munkaero[3];
                    munkak3[0] = new Auditor();
                    munkak3[1] = new Broker();
                    munkak3[2] = new Szabadbolcsesz();

                    return munkak3[random];

                case 4:
                    random = RandomGenerator.rnd.Next(0, 2);
                    Munkaero[] munkak4 = new Munkaero[2];
                    munkak4[0] = new AlkalmazottMatematikus();
                    munkak4[1] = new MuszakiMenedzser();
                    return munkak4[random];

                default:
                    throw new Exception("Ha ezt az üzenetet kapod, valószínűleg nincs semmilyen feladat a nyilvántartásban indításkor!"); //Ezt az üzeneteta akkor lehetne megkapni, ha a minimalis szint 1 lenne,(ez az eset kizárva a fenti komment miatt), vagy ha a minimalis szint 5, ekkor üresnek kell lennie mind a 2 feladat txtnek

            }


        }


        #endregion


        //Feladatkiosztás
        public void FeladatKiosztas() 
        {
            
            foreach (Feladat feladat in kiadofeladatok) //bejárjuk akiadó feladatokat minden egyes elemét megnézzük, hogy kitudjuk e osztani
            {

                bool sikerese=munkatarsak.AlkalmasMunkaeroKereses(feladat); //kiosztás sikerességét tartalmazó bool

                if (sikerese) // ha sikeres a feladat kiosztása akkor töröljük a kiadó feladatokból a kiosztott feladatot mert csak egyszer lehet kiosztani és már valamelyik munkaerő tartalmazni fogja
                {
                    kiadofeladatok.ElemTorles(feladat);
                }
                         
                
            }
            kiosztasvegefuggveny?.Invoke(kiadofeladatok,munkatarsak);
            Mentesfajlba();

        }

        //Kiosztottt feladatok elmentése Munkásokaal, kimaradt feladatok elmentése(kimaradtmunkasok és feladatok txtbe) ("markiosztott.txt" fájlba beírás)
        private void Mentesfajlba() 
        {
            System.IO.StreamWriter ir = new System.IO.StreamWriter("kimaradtfeladatok.txt",false);
            foreach (Feladat feladat in kiadofeladatok)
            {
                ir.WriteLine(feladat.Nev);
            }
            ir.Close();

            ir = new System.IO.StreamWriter("kimaradtmunkasok.txt", false);
            foreach (Munkaero munkaero in munkatarsak)
            {
                if (munkaero.Elvegezendo.Darabszam == 0)
                {
                    ir.WriteLine(munkaero.Nev);
                }
            }

            ir.Close();



            ir = new System.IO.StreamWriter("markiosztott.txt", false);
            foreach (Munkaero munkaero in munkatarsak)
            {
                if (munkaero.Elvegezendo.Darabszam!=0)
                {
                    ir.Write(munkaero.Nev + ";");
                    foreach (Feladat feladat in munkaero.Elvegezendo)
                    {
                        ir.Write(feladat.Nev+"/");
                    }
                    ir.WriteLine();

                }

            }

            ir.Close();


        }
    }
}
