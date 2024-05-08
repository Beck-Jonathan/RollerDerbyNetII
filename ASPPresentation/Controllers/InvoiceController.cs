using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using LogicLayer;
using Microsoft.AspNet.Identity;

namespace ASPPresentation.Controllers
{
    public class InvoiceController : Controller
    {
        IInvoiceManager _invoiceManager;
        // GET: Invoice
        [Authorize]
        public ActionResult Index()
        {
            int mode = 0;
            if (User.IsInRole("Skater"))
            {
                mode = 1;
            }
            _invoiceManager = new InvoiceManager();
            List<Invoice> invoices = new List<Invoice>();
            try
            {
                fillDropDowns();
                if (mode != 1)
                {
                    invoices = _invoiceManager.getAllInvoice();
                    
                }
                else
                {
                    
                    SkaterManager skaterManager = new SkaterManager();
                    String email = User.Identity.GetUserName();
                    String derbyname = skaterManager.getSkaterByEmail(email).SkaterID;
                    invoices = _invoiceManager.getInvoiceBySkater(derbyname);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");

            }


            return View(invoices);
        }

        [HttpPost]
        public ActionResult Index(String Skater)
        {
            _invoiceManager = new InvoiceManager();
            List<Invoice> invoices;
            fillDropDowns();

            SkaterManager skaterManager = new SkaterManager();

            invoices = _invoiceManager.getInvoiceBySkater(Skater);
            return View(invoices);


        }


        // GET: Invoice/Details/5
        public ActionResult Details(int id)
        {
            Invoice detail = new Invoice();
            _invoiceManager = new InvoiceManager();
            try
            {
                detail=_invoiceManager.getInvoiceByPrimaryKey(id);
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
            if (User.IsInRole("Skater")) {
                Session["Invoice"] = id;
                return View("Pay",detail);
            }
            return View(detail) ;
        }

        // GET: Invoice/Create
        public ActionResult Create()
        {
            _invoiceManager = new InvoiceManager();
            fillDropDowns();
            return View();
        }

        // POST: Invoice/Create
        [HttpPost]
        public ActionResult Create(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _invoiceManager = new InvoiceManager();
                    _invoiceManager.addInvoice(invoice);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(invoice);
                }
            }
            else
            {
                return View(invoice);
            }
        }

        // GET: Invoice/Pay/5
        public ActionResult Pay(int id)
        {
            Invoice invoice = new Invoice();
            _invoiceManager = new InvoiceManager();
            try
            {
                invoice = _invoiceManager.getInvoiceByPrimaryKey(id);
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }

            return View(invoice);
        }

        // GET: Invoice/Edit/5
        public ActionResult Edit(int id)
        {
            Invoice invoice = new Invoice();
            _invoiceManager = new InvoiceManager();
            try
            {
                invoice = _invoiceManager.getInvoiceByPrimaryKey(id);
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
            
            return View(invoice);
        }

        // Post: Invoice/Pay/5
        [HttpPost]
        public ActionResult Pay(int id, Invoice invoice)
        {
            int result = 0;
            invoice.InvoiceID = id;
            _invoiceManager = new InvoiceManager();
            try
            {
                result= _invoiceManager.payInvoice(invoice);
                if (result == 1) {
                    RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }

            return View(invoice);
        }

        // POST: Invoice/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Invoice/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Invoice/Delete/5
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


        private void fillDropDowns()
        {
            List<String> allSkaters = new List<String>();
            allSkaters = _invoiceManager.getDistinctSkaterForDropDown();
            ViewBag.SkaterList = allSkaters;



        }
    }
}
