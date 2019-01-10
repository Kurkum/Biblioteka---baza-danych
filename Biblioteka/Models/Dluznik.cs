namespace Biblioteka.Models {
    public class Dluznik {
        public int IdCzytelnik { get; }
        public decimal WartoscKary { get; }

        public Dluznik(int idCzytelnik, decimal wartoscKary) {
            IdCzytelnik = idCzytelnik;
            WartoscKary = wartoscKary;
        }
    }
}