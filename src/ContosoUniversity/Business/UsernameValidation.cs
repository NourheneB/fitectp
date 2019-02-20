using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.Business
{
    public class UsernameValidation
    {
        public bool UsernameIsAvailable (string username)
        {
            // Définition du contexte
            SchoolContext db = new SchoolContext();

            //Dénombrer le nombre d'occurences ayant un nom d'utilisateur similaire à celui renseigné par l'utilisateur. 
            int countLogin = db.People.Where(u => u.Login == username).Count();

            return (countLogin == 0);   
        }     
    }
}