using DataAccessFakes;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace UnitTestProject1
{
    [TestClass]
    public class InvoiceManagerTests
    {
        IInvoiceManager _invoiceManager = null;

        [TestInitialize]
        public void testSetup()
        {
            _invoiceManager = new InvoiceManager(new InvoiceAccessorFakes());
        }

        [TestMethod]
        public void a() {
            //arrage

            //act

            //assert
            Assert.AreNotEqual(0, 0);
        
        }
        [TestMethod]
        public void TestAddInvoiceAddsAnInvoice()
        {
            //arrage
            bool actual = false;
            bool expected = true;
            Invoice invoice = new Invoice();
            invoice.InvoiceID = 4;
            //act
            actual=_invoiceManager.addInvoice(invoice);
            //assert
            Assert.AreEqual(actual, expected);

        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAddInvoiceCanFailWithBadData()
        {
            //arrage
            bool actual = false;
            bool expected = true;
            Invoice invoice = new Invoice();
            invoice.InvoiceID = 3;
            //act
            actual = _invoiceManager.addInvoice(invoice);
            //assert
            

        }
        [TestMethod]
        public void TestGetInvoiceByPKGetsCorrectInvoice() {
            //arrange
            decimal expected = 20;
            decimal actual = 0;
            int pk = 1;
            //act
            actual = _invoiceManager.getInvoiceByPrimaryKey(pk).InvoiceAmount;

            //assert
            Assert.AreEqual(expected, actual);     
        
        
        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestGetInvoiceByPKFailsWithBadData() {
            //arrange
            int badpk = 5;
            //act
            Invoice result = _invoiceManager.getInvoiceByPrimaryKey(badpk);
            //assert
            //nothing to do
    
        }

        [TestMethod]
        public void TestPayInvoiceWillMarkAnInvoiceAsPaid() {
           
            //arrange
            bool expected = false;
            bool actual = true;
            Invoice invoice = new Invoice() { InvoiceID = 1 };
            
            //act
            _invoiceManager.payInvoice(invoice);

            actual = _invoiceManager.getInvoiceByPrimaryKey(1).is_active;

            //assert
            Assert.AreEqual(expected, actual);
        
        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestPayInvoiceCanFail() { 
            //arrange
            int badpk = 5;
            Invoice invoice = new Invoice() { InvoiceID = badpk };
            //act
            _invoiceManager.payInvoice(invoice);
            //assert - nothing to do
        }

        [TestMethod]
        public void TestRefundInvoiceWillMarkAnInvoiceAsActive()
        {

            //arrange
            bool expected = true;
            bool actual = false;
            Invoice invoice = new Invoice() { InvoiceID = 3 };

            //act
            _invoiceManager.refundInvoice(invoice);

            actual = _invoiceManager.getInvoiceByPrimaryKey(3).is_active;

            //assert
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestRefundInvoiceCanFail()
        {
            //arrange
            int badpk = 5;
            Invoice invoice = new Invoice() { InvoiceID = badpk };
            //act
            _invoiceManager.refundInvoice(invoice);
            //assert - nothing to do
        }

        [TestMethod]
        public void TestGetAllInvoicesReturnsAllInvoices() {
            //arrange
            int actual = 0;
            int expected = 3;
            //act
            actual = _invoiceManager.getAllInvoice().Count;
            //assert
            Assert.AreEqual(expected, actual);
        
        }

        [TestMethod]
        public void TestAddMultipleCanAddMultiple() {
            int actual = 0;
            int expected = 2;
            Invoice add1 = new Invoice() { InvoiceID = 4 };
            Invoice add2 = new Invoice() { InvoiceID = 5 };
            List<Invoice> adds = new List<Invoice> { add1, add2 };
            actual = _invoiceManager.addMultipleInvoices(adds);
            Assert.AreEqual(expected, actual);
        
        
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAddMultipleCanFailWtihBadData()
        {
            int actual = 0;
            int expected = 2;
            Invoice add1 = new Invoice() { InvoiceID = 2 };
            Invoice add2 = new Invoice() { InvoiceID = 5 };
            List<Invoice> adds = new List<Invoice> { add1, add2 };
            actual = _invoiceManager.addMultipleInvoices(adds);
            Assert.AreEqual(expected, actual);


        }

        [TestMethod]
        public void TestEditInvoiceEditsCorrectInvoice() {
            decimal actual = 0;
            decimal expected = 26;
            Invoice old = new Invoice() { InvoiceID = 1 };
            Invoice _new = new Invoice() {InvoiceID=1,InvoiceAmount=26 };
            int result=_invoiceManager.editInvoice(old, _new);
            actual = _invoiceManager.getInvoiceByPrimaryKey(1).InvoiceAmount;
            Assert.AreEqual(expected, actual);
        
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestEditInvoiceFailsWithBadData() {
            decimal actual = 0;
            decimal expected = 26;
            Invoice old = new Invoice() { InvoiceID = 5 };
            Invoice _new = new Invoice() { InvoiceID = 5, InvoiceAmount = 26 };
            int result = _invoiceManager.editInvoice(old, _new);
            //assert - nothing to do


        }

        [TestMethod]
        public void TestSelectInvoiceBySkaterReturnsCorrectNumberOfInvoices()
        {
            int actual = 0;
            int expected = 2;
            
             actual = _invoiceManager.getInvoiceBySkater("Gannon").Count;
            
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestSelectInvoiceBySkaterFailsWithEmptySet()
        {
            int actual = 0;
            int expected = 2;

            actual = _invoiceManager.getInvoiceBySkater("dummy").Count;

            //assert - nothing to do


        }

    }
}
