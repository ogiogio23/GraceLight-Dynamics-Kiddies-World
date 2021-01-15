using GldKiddiesWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using System.IO;

namespace GldKiddiesWorld.Controllers
{
    public class AdminController : Controller
    {
        private GldKiddiesWorldDbContext db = new GldKiddiesWorldDbContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin user)
        {
            var usr = db.Admins.SingleOrDefault(a => a.Email == user.Email && a.Password == user.Password);
            if (usr != null)
            {
                Session["User"] = usr;
                return RedirectToAction("Order");
            }
            else
            {
                ViewBag.Message = "Invalid Email or Password";
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Admin admin)
        {
            if (ModelState.IsValid)
            {
                var res = db.Admins.Where(a => a.AdminId == admin.AdminId).FirstOrDefault();
                if (res != null)
                {
                    ViewBag.Message = "Record already exist!";
                    return RedirectToAction("Register");
                }
                db.SaveChanges();

                db.Admins.Add(admin);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Admin");
            }
            return View();
        }

        public ActionResult Category()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(Category category)
        {

            if (ModelState.IsValid)
            {

                var res = db.Categories.Where(c => c.CatId == category.CatId).FirstOrDefault();
                if (res != null)
                {
                    ViewBag.Message = "Category already exist";
                    return RedirectToAction("AddCategory");
                }
                db.SaveChanges();

                db.Categories.Add(category);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Category");
            }
            return View();
        }

        public ActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory([Bind(Include = "CatId,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Category");
            }
            return View(category);
        }

        public ActionResult DeleteCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Category");
        }

        public ActionResult Product()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        public ActionResult AddProduct()
        {
            ViewBag.CatId = new SelectList(db.Categories, "CatId", "CategoryName");
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AddProduct(Product product, HttpPostedFileBase ProductImageUrl1, HttpPostedFileBase ProductImageUrl2, HttpPostedFileBase ProductImageUrl3)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                if (ProductImageUrl1 == null || ProductImageUrl2 == null || ProductImageUrl3 == null)
                {
                    return View();
                }

                string path = Path.Combine(Server.MapPath("~/Content/Product_Images"), Path.GetFileName(ProductImageUrl1.FileName), Path.GetFileName(ProductImageUrl2.FileName), Path.GetFileName(ProductImageUrl3.FileName));
                string[] paths = path.Split('\\');
                string defaultPath = "";
                for(int i = 0; i< paths.Length - 3; i++)
                {
                   defaultPath+=paths[i]+"\\";
                }
                Array.Reverse(paths);
                string imgPath = defaultPath + paths[2];
                ProductImageUrl1.SaveAs(defaultPath+paths[2]);
                ProductImageUrl2.SaveAs(defaultPath+paths[1]);
                ProductImageUrl3.SaveAs(defaultPath+paths[0]);
                ViewBag.FileStatus = "File uploaded successfully.";

            }
            catch (Exception e)
            {
                e.GetBaseException();
                //ViewBag.FileStatus = "Error while file uploading.";
                return View();
            }

            product.ProductImageUrl1 = ProductImageUrl1.FileName;
            product.ProductImageUrl2 = ProductImageUrl2.FileName;
            product.ProductImageUrl3 = ProductImageUrl3.FileName;

            var res = db.Categories.Single(a => a.CatId == product.CatId);
            db.SaveChanges();

            if (res == null)
            {
                return View();
            }

            product.CatId = res.CatId;
            var prod = db.Products.Add(product);
            if (prod == null)
            {
                return RedirectToAction("AddProduct");
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                e.GetBaseException();
            }

            return RedirectToAction("Product");
            ViewBag.CatId = new SelectList(db.Categories, "CatId", "CategoryName", product.CatId);

        }

        public ActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatId = new SelectList(db.Categories, "CatId", "CategoryName", product.CatId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct([Bind(Include = "ProductId,ProductName,ProductImageUrl1,ProductImageUrl2,ProductImageUrl3,Price,Description,Quantity,DateCreated,CatId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Product");
            }
            ViewBag.CatId = new SelectList(db.Categories, "CatId", "CategoryName", product.CatId);
            return View(product);
        }

        public ActionResult ProductDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatId = new SelectList(db.Categories, "CatId", "CategoryName", product.CatId);
            return View(product);
        }

        public ActionResult DeleteProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProductConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Product");
        }


        public ActionResult Order()
        {
            return View();
        }

        public ActionResult ManageAccount()
        {
            return View();
        }

        public ActionResult Admin()
        {
            return View(db.Admins.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}