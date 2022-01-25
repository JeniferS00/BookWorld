using BookWorld.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookWorld.ViewModel;

namespace BookWorld.Controllers
{
    public class ItemController : Controller
    {
        private BookCartDBEntities objBookCartDBEntities;
        public ItemController()
        {
            objBookCartDBEntities = new BookCartDBEntities();
        }
        // GET: Item
        public ActionResult Index()
        {
            ItemViewModel objItemViewModel = new ItemViewModel();
            objItemViewModel.CategorySelectListItem = (from objCat in objBookCartDBEntities.Categories
                                                       select new SelectListItem()
                                                       {
                                                           Text = objCat.CategoryName,
                                                           Value = objCat.CategoryID.ToString(),
                                                           Selected = true
                                                       });
            return View(objItemViewModel);
        }

        [HttpPost]
        public JsonResult Index(ItemViewModel objItemViewModel)
        {
            string NewImage = Guid.NewGuid() + Path.GetExtension(objItemViewModel.ImagePath.FileName); //Guid will give Image name path ... is a extension
            objItemViewModel.ImagePath.SaveAs(Server.MapPath("~/Images/" + NewImage));

            Item objItem = new Item();
            objItem.ImagePath = "~/Images/" + NewImage;
            objItem.CategoryID = objItemViewModel.CategoryID;
            objItem.Description = objItemViewModel.Description;
            objItem.ItemCode = objItemViewModel.ItemCode;
            objItem.ItemID = Guid.NewGuid();
            objItem.ItemName = objItemViewModel.ItemName;
            objItem.ItemPrice = objItemViewModel.ItemPrice;
            objBookCartDBEntities.Items.Add(objItem);
            objBookCartDBEntities.SaveChanges();



            return Json(new { Success = true, Message = "Item is added Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}