using BookWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookWorld.ViewModel;

namespace BookWorld.Controllers
{
    public class ShoppingController : Controller
    {
        private BookCartDBEntities objBookCartDBEntities;
        List<ShoppingCartViewModel> listofShoppingCartViewModels;
        public ShoppingController()
        {
            objBookCartDBEntities = new BookCartDBEntities();
            listofShoppingCartViewModels = new List<ShoppingCartViewModel>();

        }
        // GET: Shopping
        public ActionResult Index()
        {
            IEnumerable<ShoppingViewModel> listofShoppingViewModel = (from objItem in objBookCartDBEntities.Items
                                                                      join
                                                                      objCate in objBookCartDBEntities.Categories
                                                                      on objItem.CategoryID equals objCate.CategoryID
                                                                      select new ShoppingViewModel()
                                                                      {
                                                                          ImagePath = objItem.ImagePath,
                                                                          ItemName = objItem.ItemName,
                                                                          Description = objItem.Description,
                                                                          ItemPrice = objItem.ItemPrice,
                                                                          ItemID = objItem.ItemID,
                                                                          Category = objCate.CategoryName,
                                                                          ItemCode = objItem.ItemCode
                                                                      }

                                                                     ).ToList();
            return View(listofShoppingViewModel);
        }
        [HttpPost]
        public JsonResult Index(string ItemID)
        {
            ShoppingCartViewModel objShoppingCartViewModel = new ShoppingCartViewModel();
            Item objItem = objBookCartDBEntities.Items.Single(model => model.ItemID.ToString() == ItemID);
            if (listofShoppingCartViewModels.Any(model => model.ItemID.ToString() == ItemID))
            {
                objShoppingCartViewModel = listofShoppingCartViewModels.Single(model => model.ItemID == ItemID);
                objShoppingCartViewModel.Quantity = objShoppingCartViewModel.Quantity + 1;
                objShoppingCartViewModel.Total = objShoppingCartViewModel.Quantity * objShoppingCartViewModel.UnitPrice;
            }
            else
            {
                objShoppingCartViewModel.ItemID = ItemID;
                objShoppingCartViewModel.ImagePath = objItem.ImagePath;
                objShoppingCartViewModel.ItemName = objItem.ItemName;
                objShoppingCartViewModel.Quantity = 1;
                objShoppingCartViewModel.Total = objItem.ItemPrice;
                objShoppingCartViewModel.UnitPrice = objItem.ItemPrice;
                listofShoppingCartViewModels.Add(objShoppingCartViewModel);

            }

            Session["CartCounter"] = listofShoppingCartViewModels.Count;
            Session["cartItem"] = listofShoppingCartViewModels;

            return Json(new { Success = true, Counter = listofShoppingCartViewModels.Count }, JsonRequestBehavior.AllowGet); 
        }
    }
}