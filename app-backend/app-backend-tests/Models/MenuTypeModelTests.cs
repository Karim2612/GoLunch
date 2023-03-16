using app_backend;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ap_backend_tests;


namespace app_backend_tests.Models
{
    [TestClass]
    public class MenuTypeModelTests : AssemblyLoader
    {
        private const string TYPE_NAME = "menutype";
        /// <summary>
        /// Vérifie si le type existe
        /// </summary>
        [TestMethod]
        public void TypeExist()
        {
            Assert.IsNotNull(GetType(TYPE_NAME));
        }

        /// <summary>
        /// Vérifie si le type est public
        /// </summary>
        [TestMethod]
        public void TypeIsPublic()
        {
            Assert.IsTrue(GetType(TYPE_NAME).IsPublic);
        }

        /// <summary>
        /// Vérifie si la propriété id existe
        /// </summary>
        [TestMethod]
        public void PropertyIdExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "id"));
        }

        /// <summary>
        /// Vérifie si la propriété id est de type int
        /// </summary>
        [TestMethod]
        public void PropertyIdIsTypeInt()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "id") == typeof(int));
        }

        /// <summary>
        /// Vérifie si la propriété nom existe
        /// </summary>
        [TestMethod]
        public void PropertyNomExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "nom"));
        }

        /// <summary>
        /// Vérifie si la propriété nom est de type string
        /// </summary>
        [TestMethod]
        public void PropertyNomIsTypeString()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "nom") == typeof(string));
        }




    }
}