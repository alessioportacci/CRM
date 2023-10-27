using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Models
{
    public class AppuntamentiCalendarioModel
    {

            public int id { get; set; }

            public string NomeCliente { get; set; }

            public string Tipologia { get; set; }

            public string Data { get; set; }

            public string Inizio { get; set; }

            public string Fine { get; set; }

            public string Descrizione { get; set; }

            public string Note { get; set; }

            public bool Concluso { get; set; }

            public string Colore { get; set;}

    }
}