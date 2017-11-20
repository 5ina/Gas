using Abp.AutoMapper;
using GasSolution.Common;
using GasSolution.Domain.Common;
using GasSolution.Web.Areas.Admin.Models.KeyFonts;
using GasSolution.Web.Framework.DataGrids;
using GasSolution.Web.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GasSolution.Web.Areas.Admin.Controllers
{
    public class KeyFontController : GasControllerAdminBase
    {
        #region ctor && Fields
        private readonly IKeyFontService _keyService;

        public KeyFontController(IKeyFontService keyService)
        {
            this._keyService = keyService;
        }
        #endregion


        #region Utilities

        [NonAction]
        private void PrepareKeyFontModel(KeyFontModel model)
        {
            model.AvailableType = KeyFontType.Text.EnumToDictionary(e => e.GetDescription()).ToList();
        }
        #endregion

        #region Method

        public ActionResult Index()
        {
            return List();
        }

        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command, string keywords)
        {
            var keys = _keyService.GetAllKeyFonts(keywords: keywords,
                                                    pageIndex: command.Page - 1,
                                                    pageSize: command.PageSize);
            var jsonData = new DataSourceResult
            {
                Data = keys.Items.Select(f => new
                {
                    Id = f.Id,
                    Keyword = f.Keyword,
                    Type = ((KeyFontType)f.KeyFontType).GetDescription(),

                }),
                Total = keys.TotalCount
            };
            return AbpJson(jsonData);
        }


        public ActionResult Create()
        {
            var model = new KeyFontModel();
            PrepareKeyFontModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(KeyFontModel model)
        {
            if (ModelState.IsValid)
            {
                var font = model.MapTo<KeyFont>();
                _keyService.InsertKeyFont(font);
                return RedirectToAction("List");
            }
            PrepareKeyFontModel(model);
            return View(model);
        }


        public ActionResult Edit(int id)
        {
            var key = _keyService.GetKeyFontById(id);
            var model = key.MapTo<KeyFontModel>();
            PrepareKeyFontModel(model);
            return View(model);
        }

        #endregion

    }
}