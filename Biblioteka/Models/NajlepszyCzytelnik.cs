using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteka.Models {
    public class NajlepszyCzytelnik {
        public string Imie { get; }
        public string Nazwisko { get; }
        public int Liczba { get; set; }

        public NajlepszyCzytelnik(string imie, string nazwisko, int liczba) {
            Imie = imie;
            Nazwisko = nazwisko;
            Liczba = liczba;
        }
    }
}