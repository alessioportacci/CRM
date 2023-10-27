using CRM.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using System.Web.Security;

namespace CRM.Models
{
    public class RoleManager : RoleProvider
    {

        ModelDbContext db = new ModelDbContext();

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            List<string> roles = new List<string>();
            try
            {
                //Mi prendo l'id dell'utente e la lista dei suoi ruoli
                int utente = db.Utenti.Where(u => u.Username == username).FirstOrDefault().Id;
                List<UtentiRuoli> Ruoli = db.UtentiRuoli.Where(ut => ut.FkUtente == utente).ToList();
                //La scorro e aggiungo il ruolo alla lista dei ruoli da restituire
                foreach (UtentiRuoli ruolo in Ruoli)
                    roles.Add(db.Ruoli.Find(ruolo.FkRuolo).Ruolo);
            }
            catch (Exception ex) { }

            return roles.ToArray();
        }


        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}