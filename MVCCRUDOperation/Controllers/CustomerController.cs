using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCCRUDOperation.Data_Access;
using MVCCRUDOperation.Models;

namespace MVCCRUDOperation.Controllers
{
    public class CustomerController : Controller
    {
        CustomerRepository customer_Repository = new CustomerRepository();

        // GET: Customer
        public ActionResult Index()
        {
            var CustomerList = customer_Repository.GetAllCustomers();
            if (CustomerList.Count == 0)
            {
                TempData["InfoMessage"] = "Currently no  customers details in the database ";
            }
            return View(CustomerList);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {

            try
            {
                var customer = customer_Repository.GetCustomersByID(id).FirstOrDefault();
                if (customer == null)
                {
                    TempData["InfoMessage"] = "Customer not available with ID " + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(customer);
            }
            catch (Exception exception)
            {

                TempData["ErrorMessage"] = exception.Message;
                return View();
            }
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(CustomerModel customer)
        {
            bool IsInserted = false;
            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = customer_Repository.InsertCustomerData(customer);
                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Customer details saved successfully!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to save the details!";
                    }

                }
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {

                TempData["ErrorMessage"] = exception.Message;
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            var customer = customer_Repository.GetCustomersByID(id).FirstOrDefault();
            if (customer == null)
            {
                TempData["InfoMessage"] = "Customer not available with ID" + id.ToString();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // POST: Customer/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult Updatecustomer(CustomerModel customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = customer_Repository.UpdateCustomerData(customer);
                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Customer details updated successfully!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to update the details!";
                    }
                }
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {

                TempData["ErrorMessage"] = exception.Message;
                return View();
            }
        }


        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var customer = customer_Repository.GetCustomersByID(id).FirstOrDefault();
                if (customer == null)
                {
                    TempData["InfoMessage"] = "Customer not available with ID " + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(customer);
            }
            catch (Exception exception)
            {

                TempData["ErrorMessage"] = exception.Message;
                return View();
            }
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                // TODO: Add delete logic here
                string result = customer_Repository.DeleteCustomer(id);
                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;


                }
               
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {

                TempData["ErrorMessage"] = exception.Message;
                return View();
            }
        }
    }
}
