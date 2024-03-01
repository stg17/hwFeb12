using classFeb12.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace classFeb12.Controllers
{
    public class PeopleController : Controller
    {
        public ActionResult ShowPeople()
        {
            PeopleManager manager = new PeopleManager(Properties.Settings.Default.ConStr);
            return View(new ShowPeopleViewModel { people = manager.GetPeople()});
        }

        public ActionResult AddPersonForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPeople(Person person)
        {
            PeopleManager manager = new PeopleManager(Properties.Settings.Default.ConStr);
            manager.AddPerson(person);
            return Redirect("/people/showpeople");
        }

        [HttpPost]
        public ActionResult HiddenDelete(int id)
        {
            PeopleManager manager = new PeopleManager(Properties.Settings.Default.ConStr);
            manager.DeletePerson(id);
            return Redirect("/people/showpeople");
        }

        public ActionResult Edit(int id)
        {
            PeopleManager manager = new PeopleManager(Properties.Settings.Default.ConStr);
            return View(new EditPersonViewModel { person = manager.GetPerson(id) });
        }

        [HttpPost]
        public ActionResult Update(Person person)
        {
            PeopleManager manager = new PeopleManager(Properties.Settings.Default.ConStr);
            manager.UpdatePerson(person);
            return Redirect("/people/showpeople");
        }
    }
}