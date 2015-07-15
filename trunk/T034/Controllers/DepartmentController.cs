using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Db.DataAccess;
using Db.Entity;
using Db.Entity.Vgiik;
using T034.ViewModel;

namespace T034.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IBaseDb _db;

        public DepartmentController()
        {
            _db = MvcApplication.DbFactory.CreateBaseDb();


            //нужен мап для DepartmentViewModel.Nodes - NodeId через заяпятую (пока так)
            Mapper.CreateMap<Department, DepartmentViewModel>()
                .ForMember(dest => dest.Nodes, opt => opt.MapFrom(src => src.Nodes == null || !src.Nodes.Any() ? "" : src.Nodes.Select(n => n.Id.ToString()).Aggregate((i, j) => i.ToString() + "," + j.ToString())))
                .ForMember(dest => dest.Albums, opt => opt.Ignore());
            Mapper.CreateMap<DepartmentViewModel, Department>()
                .ForMember(dest => dest.Nodes, opt => opt.MapFrom(src => new List<Node>(src.Nodes.Split(new [] { ','}).Select(n => new Node{Id = Convert.ToInt32(n)}))));
        }

        public ActionResult List()
        {
            try
            {
                var items = _db.Select<Department>();

                var model = new List<DepartmentViewModel>();
                model = Mapper.Map(items, model);

                return View(model);
            }
            catch (Exception ex)
            {
                return View("ServerError", (object)"Получение списка");
            }
        }

        [HttpGet]
        //[Authorize]
        public ActionResult AddOrEdit(int? id)
        {
            var model = new DepartmentViewModel();
            if (id.HasValue)
            {
                var item = _db.Get<Department>(id.Value);
                model = Mapper.Map(item, model);
            }

            return View(model);
        }

        //[AuthorizeUser]
        public ActionResult AddOrEdit(DepartmentViewModel model)
        {
            var item = new Department();
            if (model.Id > 0)
            {
                item = _db.Get<Department>(model.Id);
            }

            var newItem = Mapper.Map(model, item);

            item.Nodes = newItem.Nodes;

            var result = _db.SaveOrUpdate(item);

            return RedirectToAction("List");
        }

    }
}
