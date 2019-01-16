namespace Biblioteka.Models {
    public class Autorstwo {
        public int IdKsiazka { get; set; }
        public int IdAutor { get; set; }

        public Autorstwo() {

        }

        public Autorstwo(int idKsiazka, int idAutor) {
            IdKsiazka = idKsiazka;
            IdAutor = idAutor;
        }
    }
}