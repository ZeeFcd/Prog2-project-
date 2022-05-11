using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ija9wq_felevesfeladat_2
{
    //  feladat által előírt interfaceszek
    interface IIFeladat
    {
        string Nev { get; set; } //feladat neve
        FeladatszintEnum Szint { get; set; } //feladat szitnje
    }
    interface IMunkaero 
    {
        string Nev { get; set; } 
        KepzetsegEnum Kepzetseg { get; set; }
        LancoltListaGen<Feladat> Elvegezendo { get; set; } //feladatokat tarltalmazó láncolt lista
    }
}
