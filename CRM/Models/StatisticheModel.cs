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

    }


}