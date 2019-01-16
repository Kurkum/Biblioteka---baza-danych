namespace Biblioteka.Models {
    public class AutorstwoWidok {
        public int IdKsiazka { get; }
        public int IdAutor { get; }
        public string Imie { get; }
        public string Nazwisko { get; }

        public AutorstwoWidok(int idKsiazka, int idAutor, string imie, string nazwisko) {
            IdKsiazka = idKsiazka;
            IdAutor = idAutor;
            Imie = imie;
            Nazwisko = nazwisko;
        }
    }
}