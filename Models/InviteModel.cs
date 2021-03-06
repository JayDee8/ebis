﻿using System.Collections.Generic;

namespace ebis.Models
{
    public class InviteModel
    {
        public osoby m_osoba { get; set; }
        public IEnumerable<ebis.Models.osoby> m_osoby { get; set; }
        public List<List<nastroje>> m_nastroje { get; set; }
        public IEnumerable<int> nastroje_id { get; set; }
        public IEnumerable<int> pozvane_id { get; set; }
        public int event_id { get; set; }
    }
}
