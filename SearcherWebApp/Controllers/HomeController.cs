using ChemicalStructureSearchingDemo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChemicalStructureSearchingDemo.SearcherWebApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string molfileContents, SearchType searchType)
        {
            try
            {
                var searcher = new ChemicalStructureSearcher(this.HttpContext.Server.MapPath("~/App_Data/Index"));
                var queryStructure = new ChemicalStructure() { MolfileContents = molfileContents };
                var results = searcher.Search(queryStructure, searchType);
                return PartialView("_SearchResults", results);
            }
            catch(Exception ex)
            {
                return PartialView("_SearchError", ex.Message);
            }
        }

        [HttpPost]
        public ContentResult GetStructureImageBase64(string molfileContents, int width, int height)
        {
            var chemicalStructure = new ChemicalStructure() { MolfileContents = molfileContents };
            return Content(Convert.ToBase64String(chemicalStructure.ToBitmapBytes(width, height)));
        }

        [HttpGet]
        public ContentResult GetSampleMolfile(string id)
        {
            switch (id)
            {
                case "acid_chloride":
                    return Content(ChemicalStructureSearchingDemo.SearcherWebApp.Properties.Resources.acid_chloride);
                case "ampicillin":
                    return Content(ChemicalStructureSearchingDemo.SearcherWebApp.Properties.Resources.ampicillin);
                case "prozac":
                    return Content(ChemicalStructureSearchingDemo.SearcherWebApp.Properties.Resources.Prozac);
                case "substructure":
                    return Content(ChemicalStructureSearchingDemo.SearcherWebApp.Properties.Resources.Substructure);
            }

            return Content(String.Empty);
        }
    }
}