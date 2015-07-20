using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Db.DataAccess;
using Db.Entity;
using Db.Entity.Directory;
using Microsoft.Security.Application;
using T034.ViewModel;
using T034.ViewModel.Common;

namespace T034.Controllers
{
    public class NodeController : Controller
    {
        private readonly IBaseDb _db;

        public NodeController()
        {
            _db = MvcApplication.DbFactory.CreateBaseDb();

            Mapper.CreateMap<Node, NodeViewModel>()
                .ForMember(dest => dest.NodeTypeId, opt => opt.MapFrom(src => (int)src.NodeType));
            Mapper.CreateMap<NodeViewModel, Node>()
                .ForMember(dest => dest.NodeType, opt => opt.MapFrom(src => (NodeType)src.NodeTypeId));
               // .ForMember(dest => dest.Path, opt => opt.MapFrom(src => Sanitizer.GetSafeHtmlFragment(src.Path)));
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddOrEdit(int? id)
        {
            var model = new NodeViewModel();
            if (id.HasValue)
            {
                var item = _db.Get<Node>(id.Value);
                model = Mapper.Map(item, model);
            }

            model.Types = new List<SelectListItem>
                {
                    new SelectListItem{Text = "Фото", Value = "1"},
                    new SelectListItem{Text = "Документ", Value = "2"},
                    new SelectListItem{Text = "Видео", Value = "3"}
                };

            return View(model);
        }

        [Authorize]
        public ActionResult AddOrEdit(NodeViewModel model)
        {
            var item = new Node();


            item = Mapper.Map(model, item);

            var result = _db.SaveOrUpdate(item);

            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            try
            {
                var items = _db.Select<Node>();

                var model = new List<NodeViewModel>();
                model = Mapper.Map(items, model);

                return View(model);
            }
            catch (Exception ex)
            {
                return View("ServerError", (object)"Получение списка публикаций");
            }
        }

    }
}
