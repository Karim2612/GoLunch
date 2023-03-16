using app_backend;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ap_backend_tests;


namespace app_backend_tests.Models
{
    [TestClass]
    public class RestaurantModelTests : AssemblyLoader
    {
        private const string TYPE_NAME = "restaurant";
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

        /// <summary>
        /// Vérifie si la propriété description existe
        /// </summary>
        [TestMethod]
        public void PropertyDescriptionExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "description"));
        }

        /// <summary>
        /// Vérifie si la propriété description est de type string
        /// </summary>
        [TestMethod]
        public void PropertyDescriptionIsTypeString()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "description") == typeof(string));
        }

        /// <summary>
        /// Vérifie si la propriété contactemail existe
        /// </summary>
        [TestMethod]
        public void PropertyContactEmailExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "contactemail"));
        }

        /// <summary>
        /// Vérifie si la propriété contactemail est de type string
        /// </summary>
        [TestMethod]
        public void PropertyContactEmailIsTypeString()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "contactemail") == typeof(string));
        }

        /// <summary>
        /// Vérifie si la propriété contacttel existe
        /// </summary>
        [TestMethod]
        public void PropertyContactTelExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "contacttel"));
        }

        /// <summary>
        /// Vérifie si la propriété contacttel est de type string
        /// </summary>
        [TestMethod]
        public void PropertyContactTelIsTypeString()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "contacttel") == typeof(string));
        }

        /// <summary>
        /// Vérifie si la propriété urlsite existe
        /// </summary>
        [TestMethod]
        public void PropertyUrlSiteExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "urlsite"));
        }

        /// <summary>
        /// Vérifie si la propriété urlsite est de type string
        /// </summary>
        [TestMethod]
        public void PropertyUrlSiteIsTypeString()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "urlsite") == typeof(string));
        }

        /// <summary>
        /// Vérifie si le type contient une méthode equals
        /// </summary>
        [TestMethod]
        public void TypeContainsMethosEquals()
        {
            Assert.IsNotNull(GetMethod(TYPE_NAME, "equals"));
        }

        /// <summary>
        /// Vérifie que la méthode equals retourne un type bool
        /// </summary>
        [TestMethod]
        public void MethodEqualsReturnTypeIsBool()
        {
            Assert.IsTrue(GetMethod(TYPE_NAME, "equals")?.ReturnType == typeof(bool));
        }

        /// <summary>
        /// Vérifie que la méthode equals soit public
        /// </summary>
        [TestMethod]
        public void MethodEqualsIsPublic()
        {
            Assert.IsTrue(GetMethod(TYPE_NAME, "equals")?.IsPublic);
        }
        /// <summary>
        /// Vérifie que la méthode equals soit virtual
        /// </summary>
        [TestMethod]
        public void MethodEqualsIsVirtual()
        {
            Assert.IsTrue(GetMethod(TYPE_NAME, "equals")?.IsVirtual);
        }

        /// <summary>
        /// Vérifie que la méthode equals contienne 1 paramètre
        /// </summary>
        [TestMethod]
        public void MethodEqualsContainsOneParameter()
        {
            Assert.IsTrue(GetMethodParameters(TYPE_NAME, "equals").Count == 1);
        }

    }
}