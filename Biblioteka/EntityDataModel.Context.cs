﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Biblioteka
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class BibliotekaEntities : DbContext
    {
        public BibliotekaEntities()
            : base("name=BibliotekaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Autor> Autors { get; set; }
        public virtual DbSet<Czytelnik> Czytelniks { get; set; }
        public virtual DbSet<Egzemplarz> Egzemplarzs { get; set; }
        public virtual DbSet<Gatunek> Gatuneks { get; set; }
        public virtual DbSet<Ksiazka> Ksiazkas { get; set; }
        public virtual DbSet<Wydawnictwo> Wydawnictwoes { get; set; }
        public virtual DbSet<Wypozyczenie> Wypozyczenies { get; set; }
        public virtual DbSet<Ksiegozbior> Ksiegozbiors { get; set; }
        public virtual DbSet<PrzetrzymaneKsiazki> PrzetrzymaneKsiazkis { get; set; }
    
        public virtual int DodajAutora(Nullable<int> idAutor, string imie, string nazwisko)
        {
            var idAutorParameter = idAutor.HasValue ?
                new ObjectParameter("IdAutor", idAutor) :
                new ObjectParameter("IdAutor", typeof(int));
    
            var imieParameter = imie != null ?
                new ObjectParameter("Imie", imie) :
                new ObjectParameter("Imie", typeof(string));
    
            var nazwiskoParameter = nazwisko != null ?
                new ObjectParameter("Nazwisko", nazwisko) :
                new ObjectParameter("Nazwisko", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DodajAutora", idAutorParameter, imieParameter, nazwiskoParameter);
        }
    
        public virtual int DodajCzytelnika(Nullable<int> idCzytelnik, string imie, string nazwisko, Nullable<int> telefon, string adres, Nullable<int> wersja)
        {
            var idCzytelnikParameter = idCzytelnik.HasValue ?
                new ObjectParameter("IdCzytelnik", idCzytelnik) :
                new ObjectParameter("IdCzytelnik", typeof(int));
    
            var imieParameter = imie != null ?
                new ObjectParameter("Imie", imie) :
                new ObjectParameter("Imie", typeof(string));
    
            var nazwiskoParameter = nazwisko != null ?
                new ObjectParameter("Nazwisko", nazwisko) :
                new ObjectParameter("Nazwisko", typeof(string));
    
            var telefonParameter = telefon.HasValue ?
                new ObjectParameter("Telefon", telefon) :
                new ObjectParameter("Telefon", typeof(int));
    
            var adresParameter = adres != null ?
                new ObjectParameter("Adres", adres) :
                new ObjectParameter("Adres", typeof(string));
    
            var wersjaParameter = wersja.HasValue ?
                new ObjectParameter("Wersja", wersja) :
                new ObjectParameter("Wersja", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DodajCzytelnika", idCzytelnikParameter, imieParameter, nazwiskoParameter, telefonParameter, adresParameter, wersjaParameter);
        }
    
        public virtual int DodajGatunek(Nullable<int> idGatunek, string nazwa)
        {
            var idGatunekParameter = idGatunek.HasValue ?
                new ObjectParameter("IdGatunek", idGatunek) :
                new ObjectParameter("IdGatunek", typeof(int));
    
            var nazwaParameter = nazwa != null ?
                new ObjectParameter("Nazwa", nazwa) :
                new ObjectParameter("Nazwa", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DodajGatunek", idGatunekParameter, nazwaParameter);
        }
    
        public virtual int DodajWydawnictwo(Nullable<int> idWydawnictwo, string nazwa)
        {
            var idWydawnictwoParameter = idWydawnictwo.HasValue ?
                new ObjectParameter("IdWydawnictwo", idWydawnictwo) :
                new ObjectParameter("IdWydawnictwo", typeof(int));
    
            var nazwaParameter = nazwa != null ?
                new ObjectParameter("Nazwa", nazwa) :
                new ObjectParameter("Nazwa", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DodajWydawnictwo", idWydawnictwoParameter, nazwaParameter);
        }
    
        public virtual int Przedluzenie(Nullable<int> idWypozyczenie)
        {
            var idWypozyczenieParameter = idWypozyczenie.HasValue ?
                new ObjectParameter("IdWypozyczenie", idWypozyczenie) :
                new ObjectParameter("IdWypozyczenie", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Przedluzenie", idWypozyczenieParameter);
        }
    
        public virtual int PrzedluzenieTerminuOddania(Nullable<int> idWypozyczenie)
        {
            var idWypozyczenieParameter = idWypozyczenie.HasValue ?
                new ObjectParameter("IdWypozyczenie", idWypozyczenie) :
                new ObjectParameter("IdWypozyczenie", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PrzedluzenieTerminuOddania", idWypozyczenieParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual int UsunAutora(Nullable<int> idAutor)
        {
            var idAutorParameter = idAutor.HasValue ?
                new ObjectParameter("IdAutor", idAutor) :
                new ObjectParameter("IdAutor", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UsunAutora", idAutorParameter);
        }
    
        public virtual int UsunCzytelnika(Nullable<int> idCzytelnik)
        {
            var idCzytelnikParameter = idCzytelnik.HasValue ?
                new ObjectParameter("IdCzytelnik", idCzytelnik) :
                new ObjectParameter("IdCzytelnik", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UsunCzytelnika", idCzytelnikParameter);
        }
    
        public virtual int UsunEgzemplarz(Nullable<int> idKsiazka)
        {
            var idKsiazkaParameter = idKsiazka.HasValue ?
                new ObjectParameter("IdKsiazka", idKsiazka) :
                new ObjectParameter("IdKsiazka", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UsunEgzemplarz", idKsiazkaParameter);
        }
    
        public virtual int UsunGatunek(Nullable<int> idGatunek)
        {
            var idGatunekParameter = idGatunek.HasValue ?
                new ObjectParameter("IdGatunek", idGatunek) :
                new ObjectParameter("IdGatunek", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UsunGatunek", idGatunekParameter);
        }
    
        public virtual int UsunWydawnictwo(Nullable<int> idWydawnictwo)
        {
            var idWydawnictwoParameter = idWydawnictwo.HasValue ?
                new ObjectParameter("IdWydawnictwo", idWydawnictwo) :
                new ObjectParameter("IdWydawnictwo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UsunWydawnictwo", idWydawnictwoParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> LiczbaWypozyczenOkres(Nullable<System.DateTime> dataOd, Nullable<System.DateTime> dataDo)
        {
            var dataOdParameter = dataOd.HasValue ?
                new ObjectParameter("DataOd", dataOd) :
                new ObjectParameter("DataOd", typeof(System.DateTime));
    
            var dataDoParameter = dataDo.HasValue ?
                new ObjectParameter("DataDo", dataDo) :
                new ObjectParameter("DataDo", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("LiczbaWypozyczenOkres", dataOdParameter, dataDoParameter);
        }
    
        public virtual ObjectResult<NajpopularniejszeGatunki_Result> NajpopularniejszeGatunki()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<NajpopularniejszeGatunki_Result>("NajpopularniejszeGatunki");
        }
    
        public virtual int DodajEgzemplarz(Nullable<int> idEgzemplarz, Nullable<int> idKsiazka)
        {
            var idEgzemplarzParameter = idEgzemplarz.HasValue ?
                new ObjectParameter("IdEgzemplarz", idEgzemplarz) :
                new ObjectParameter("IdEgzemplarz", typeof(int));
    
            var idKsiazkaParameter = idKsiazka.HasValue ?
                new ObjectParameter("IdKsiazka", idKsiazka) :
                new ObjectParameter("IdKsiazka", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DodajEgzemplarz", idEgzemplarzParameter, idKsiazkaParameter);
        }
    
        public virtual int DodajKsiazka(Nullable<int> idKsiazka, Nullable<int> idWydawnictwo, Nullable<int> idGatunek, string tytul, Nullable<int> rokWydania)
        {
            var idKsiazkaParameter = idKsiazka.HasValue ?
                new ObjectParameter("IdKsiazka", idKsiazka) :
                new ObjectParameter("IdKsiazka", typeof(int));
    
            var idWydawnictwoParameter = idWydawnictwo.HasValue ?
                new ObjectParameter("IdWydawnictwo", idWydawnictwo) :
                new ObjectParameter("IdWydawnictwo", typeof(int));
    
            var idGatunekParameter = idGatunek.HasValue ?
                new ObjectParameter("IdGatunek", idGatunek) :
                new ObjectParameter("IdGatunek", typeof(int));
    
            var tytulParameter = tytul != null ?
                new ObjectParameter("Tytul", tytul) :
                new ObjectParameter("Tytul", typeof(string));
    
            var rokWydaniaParameter = rokWydania.HasValue ?
                new ObjectParameter("RokWydania", rokWydania) :
                new ObjectParameter("RokWydania", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DodajKsiazka", idKsiazkaParameter, idWydawnictwoParameter, idGatunekParameter, tytulParameter, rokWydaniaParameter);
        }
    
        public virtual ObjectResult<KsiegozbiorProcedure_Result> KsiegozbiorProcedure()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<KsiegozbiorProcedure_Result>("KsiegozbiorProcedure");
        }
    
        public virtual ObjectResult<Nullable<decimal>> WartoscKary2(Nullable<int> idCzytelnik)
        {
            var idCzytelnikParameter = idCzytelnik.HasValue ?
                new ObjectParameter("IdCzytelnik", idCzytelnik) :
                new ObjectParameter("IdCzytelnik", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("WartoscKary2", idCzytelnikParameter);
        }
    }
}
