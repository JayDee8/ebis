using System.Collections.Generic;
namespace ebis.Models
{
    public class EventModel
    {
        public akce em_akce { get; set; }
        public IEnumerable<ebis.Models.fermany> em_fermany { get; set; }
        public IEnumerable<ebis.Models.osoby_akce> em_osoby_akce0 { get; set; }
        public IEnumerable<ebis.Models.osoby_akce> em_osoby_akce1 { get; set; }
        public IEnumerable<ebis.Models.osoby_akce> em_osoby_akce2 { get; set; }
        public IEnumerable<ebis.Models.akce_produkcni_listy> em_akce_produkcni_listy0 { get; set; }
        public IEnumerable<ebis.Models.akce_produkcni_listy> em_akce_produkcni_listy1 { get; set; }
        public IEnumerable<ebis.Models.akce_produkcni_listy> em_akce_produkcni_listy2 { get; set; }
        public IEnumerable<ebis.Models.akce_naklady> em_akce_naklady0 { get; set; }
        public IEnumerable<ebis.Models.akce_naklady> em_akce_naklady1 { get; set; }
        public IEnumerable<ebis.Models.akce_naklady> em_akce_naklady2 { get; set; }
        public IEnumerable<ebis.Models.akce_naklady> em_akce_naklady3 { get; set; }
        public IEnumerable<ebis.Models.akce_naklady> em_akce_naklady4 { get; set; }
        public IEnumerable<ebis.Models.akce_naklady> em_akce_naklady5 { get; set; }
        public int id { get; set; }
    }
}