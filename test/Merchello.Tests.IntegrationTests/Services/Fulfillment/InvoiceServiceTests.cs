﻿using System;
using Merchello.Core.Models;
using Merchello.Core.Services;
using NUnit.Framework;

namespace Merchello.Tests.IntegrationTests.Services.Fulfillment
{
    [TestFixture]
    [Category("Service Integration")]
    public class InvoiceServiceTests : FulfillmentTestsBase
    {
        private IInvoiceService _invoiceService;



        [SetUp]
        public void Init()
        {
            _invoiceService = PreTestDataWorker.InvoiceService;
        }

        /// <summary>
        /// Test confirms that a new invoice can be created without being persisted
        /// </summary>
        [Test]
        public void Can_Create_An_Invoice()
        {
            //// Arrange
            
  
            //// Act
            var invoice = _invoiceService.CreateInvoice(Core.Constants.DefaultKeys.InvoiceStatus.Unpaid);

            //// Assert
            Assert.NotNull(invoice);
            Assert.AreEqual(0, invoice.InvoiceNumber);
            Assert.IsFalse(((Invoice)invoice).HasIdentity);
        }
        
        /// <summary>
        /// Test confirms that an invoice can be Saved and an Invoice Number is generated
        /// </summary>
        [Test]
        public void Can_Save_An_Invoice_With_InvoiceNumber()
        {
            //// Arrange
            var invoice = _invoiceService.CreateInvoice(Core.Constants.DefaultKeys.InvoiceStatus.Unpaid);
            Console.Write(invoice.InvoiceStatusKey);

            //// Act
            _invoiceService.Save(invoice);

            //// Assert
            Assert.NotNull(invoice);
            Assert.AreNotEqual(0, invoice.InvoiceNumber);
            Assert.IsTrue(((Invoice)invoice).HasIdentity);

        }

        
    }
}