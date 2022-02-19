using ProductsQuestionSol.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductsQuestionSol.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {

            SqlConnection c = new SqlConnection();
            List<ProductsModel> list1 = new List<ProductsModel>();
            try
            {
                c.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JKJan22;Integrated Security=True";
                c.Open();

                SqlCommand cmdSelect = new SqlCommand();
                cmdSelect.Connection = c;
               
                cmdSelect.CommandType = System.Data.CommandType.Text;
                cmdSelect.CommandText = "select * from Products";

                SqlDataReader read = cmdSelect.ExecuteReader();
                while (read.Read())
                {
                    list1.Add(new ProductsModel { ProductId = (int)read["ProductId"], ProductName = read["ProductName"].ToString(), Rate = (decimal)read["Rate"], Description = read["Description"].ToString(), CategoryName = read["CategoryName"].ToString() });
                }
                read.Close();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex;
            }
            finally
            {
                c.Close();
            }
            return View(list1);
        }



        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            SqlConnection c = new SqlConnection();
            ProductsModel obj = new ProductsModel();
            try
            {
                c.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JKJan22;Integrated Security=True";
                c.Open();

                SqlCommand cmdUpdate = new SqlCommand();
                cmdUpdate.Connection = c;
                cmdUpdate.CommandType = System.Data.CommandType.Text;
                cmdUpdate.CommandText = "select * from Products where ProductId = @ProductId";

                cmdUpdate.Parameters.AddWithValue("@ProductId", id);

                SqlDataReader read = cmdUpdate.ExecuteReader();

                while (read.Read())
                {
                    obj = new ProductsModel { ProductId = (int)read["ProductId"], ProductName = read["ProductName"].ToString(), Rate = (decimal)read["Rate"], Description = read["Description"].ToString(), CategoryName = read["CategoryName"].ToString() };
                }
                read.Close();
            }
            catch
            {
                return View();
            }
            finally
            {
                c.Close();
            }
            return View(obj);

        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProductsModel obj)
        {
            try
            {
                SqlConnection c = new SqlConnection();
                try
                {
                    c.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JKJan22;Integrated Security=True";
                    c.Open();

                    SqlCommand cmdUpdate = new SqlCommand();
                    cmdUpdate.Connection = c;
                   
                    cmdUpdate.CommandType = System.Data.CommandType.Text;
                    cmdUpdate.CommandText = "UPDATE Products SET ProductName = @ProductName, Rate = @Rate, Description = @Description, CategoryName = @CategoryName where ProductId = @ProductId ";

                    cmdUpdate.Parameters.AddWithValue("@ProductId", obj.ProductId);
                    cmdUpdate.Parameters.AddWithValue("@ProductName", obj.ProductName);
                    cmdUpdate.Parameters.AddWithValue("@Rate", obj.Rate);
                    cmdUpdate.Parameters.AddWithValue("@Description", obj.Description);
                    cmdUpdate.Parameters.AddWithValue("@CategoryName", obj.CategoryName);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
                finally
                {
                    c.Close();
                }

            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
