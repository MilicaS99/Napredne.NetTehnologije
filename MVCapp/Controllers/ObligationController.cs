using DataAccessLayer.UnitOfWork;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCapp.Controllers
{
    public class ObligationController : Controller
    {
        private readonly IUnitOfWork uow;

        public ObligationController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        private  static List<Obligation> obligations = new List<Obligation>();

        [Authorize(Roles ="Admin")]
        public IActionResult CreateObligation(int id)
        {
            UserObligationViewModel model = new UserObligationViewModel();
            Person p = (User)uow.PersonRepository.SearchById(new Person { Id = id });

            model.User = new UserViewModel
            {
                UserId = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateObligation(int id,UserViewModel User)
        {
            UserObligationViewModel model = new UserObligationViewModel();
            

            model.Obligations = obligations;
            foreach(Obligation o in obligations)
            {
                o.PersonId = id;
              
            }
            uow.PersonRepository.AddObl(obligations);
            uow.Save();
            return RedirectToAction("Index","Home");
        }

        public IActionResult AddObligation(string name,string desc, DateTime deadline,int number)
        {
            Obligation o = new Obligation();
            o.Name = name;
            o.Description = desc;
            o.Deadline = deadline;
            obligations.Add(o);
            ObligationViewModel model = new ObligationViewModel();
            model.Name = name;
            model.Description = desc;
            model.Deadline = deadline;
            model.Sn = number;
            return PartialView(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult EditObligation(int id)
        {
            UserObligationViewModel model = new UserObligationViewModel();
            Person p = (User)uow.PersonRepository.SearchById(new Person { Id = id });

            model.User = new UserViewModel
            {
                UserId = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult EditObligation(int id,UserViewModel model)
        {
            
            Person p = (User)uow.PersonRepository.SearchById(new Person { Id = id });

            if (p.Obligations != null)
            {
                foreach (Obligation o in p.Obligations)
                {
                    uow.PersonRepository.DeleteObl(o);

                }
                uow.Save();
            }
            foreach (Obligation o in obligations)
            {
                o.PersonId = id;

            }
            uow.PersonRepository.AddObl(obligations);
            uow.Save();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult DetailsObligation(int id)
        {
            Person p = (User)uow.PersonRepository.SearchById(new Person { Id = id });

            UserObligationViewModel model = new UserObligationViewModel();
            model.Obligations = p.Obligations;

            return View(model);
        }
    }
}
