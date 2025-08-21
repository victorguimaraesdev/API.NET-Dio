using API.NET.Domains.Entitys;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Domains.Entity 
{
    [TestClass]
    public class AdministratorsTest 
    {
        [TestMethod]
        public void TestGetSetProp()
        {
            var adm = new Administrator();

                adm.Id = 1;
                adm.Email = "teste@teste.com";
                adm.Password = "teste";
                adm.Profile = "adm";

            Assert.AreEqual(1, adm.Id);
            Assert.AreEqual("teste@teste.com", adm.Email);
            Assert.AreEqual("teste", adm.Password);
            Assert.AreEqual("adm", adm.Profile);
        }
    }
}
