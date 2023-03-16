using app_backend;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ap_backend_tests;


namespace app_backend_tests.Models
{
    [TestClass]
    public class UserLoginTests : AssemblyLoader
    {
        private const string TYPE_NAME = "userlogin";
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
        /// Vérifie si la propriété username existe
        /// </summary>
        [TestMethod]
        public void PropertyUsernameExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "username"));
        }

        /// <summary>
        /// Vérifie si la propriété username est de type string
        /// </summary>
        [TestMethod]
        public void PropertyUsernameIsTypeString()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "username") == typeof(string));
        }

        /// <summary>
        /// Vérifie si la propriété password existe
        /// </summary>
        [TestMethod]
        public void PropertyPasswordExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "password"));
        }

        /// <summary>
        /// Vérifie si la propriété password est de type string
        /// </summary>
        [TestMethod]
        public void PropertyPasswordIsTypeString()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "password") == typeof(string));
        }


    }
}