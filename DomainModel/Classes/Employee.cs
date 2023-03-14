using System;
using System.Text.Json.Serialization;

namespace DomainModel.Classes
{
    public class Employee
    {
        public System.Guid id { get; set; }
        public int seq_id { get; set; }
        public string name { get; set; }
        public Int16 salary { get; set; }
        public string company { get; set; }

        public string nome_azienda { get; set; } // Questa proprieta non fa parte del modello dati
        
        //public int AziendaId { get; set; }
        [JsonIgnore]
        public Azienda Azienda { get; set; }
    }
}

