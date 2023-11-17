using CRM.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Models
{
    public class StatisticheModel
    {
        public List<SelectListItem> Utenti { get; set; }

        public List<SelectListItem> Clienti { get; set; }

        public List<SelectListItem> Servizi { get; set; }

        public List<SelectListItem> Tipologie {  get; set; }

    }

    public class StatisticheOutputModel
    {
        public List<int> Valori { get; set; }
        public List<string> Etichette { get; set; }
    }

    public class StatisticheFilterModel
    {
        public DateTime DataDal { get; set; }
        public DateTime DataAl { get; set; }
        public int visualizzazione { get; set; }
        public int utenti { get; set; }
        public int clienti { get; set; }
        public int servizi { get; set; }
        public int tipologie { get; set; }
    }

}