using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net;
using static Cinema.Authorization;

namespace UnitTest
{
    [TestClass]
    public class AuthorizationTest
    {
        [TestMethod]
        public void LoadDataBase()
        {
            Cinema.Authorization authorization = new Cinema.Authorization();

            var role = authorization.LoadDataBase("1", "1", "DESKTOP-09DGVTM\\SQLEXPRESS").Result;

            Assert.AreEqual(role, "Букер");
        }
    }

    [TestClass]
    public class Print
    {
        [TestMethod]
        public void PrintTicket()
        {
            Cinema.PlacesCashierControls placesCashierControls = new Cinema.PlacesCashierControls();
            string pathFile = placesCashierControls.PrintTicketPDF();

            bool isExist = false;
            if (File.Exists(pathFile))
            { 
                isExist = true;
            }

            Assert.IsTrue(isExist);
        }

        [TestMethod]
        public void PrintPdf()
        {
            Cinema.TicketAnalysis ticketAnalysis = new Cinema.TicketAnalysis();
            string pathFile = ticketAnalysis.PrintPDF();

            bool isExist = false;
            if (File.Exists(pathFile))
            {
                isExist = true;
            }

            Assert.IsTrue(isExist);
        }
    }
}
