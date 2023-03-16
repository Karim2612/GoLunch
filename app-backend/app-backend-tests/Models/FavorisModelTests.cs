using app_backend;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ap_backend_tests;


namespace app_backend_tests.Models
{
    [TestClass]
    public class FavorisModelTests : AssemblyLoader
    {
        private const string TYPE_NAME = "fav";
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
        /// Vérifie si la propriété restaurantid existe
        /// </summary>
        [TestMethod]
        public void PropertyRestaurantIdExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "restaurantid"));
        }

        /// <summary>
        /// Vérifie si la propriété restaurantid est de type int
        /// </summary>
        [TestMethod]
        public void PropertyRestaurantIdIsTypeString()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "restaurantid") == typeof(int));
        }

        /// <summary>
        /// Vérifie si la propriété restaurant existe
        /// </summary>
        [TestMethod]
        public void PropertyRestaurantExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "restaurant"));
        }

        /// <summary>
        /// Vérifie si la propriété restaurant est de type point
        /// </summary>
        [TestMethod]
        public void PropertyRestaurantTypePoint()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "restaurant?") == GetType("point"));
        }

        /// <summary>
        /// Vérifie si la propriété userid existe
        /// </summary>
        [TestMethod]
        public void PropertyUserIdExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "userid"));
        }

        /// <summary>
        /// Vérifie si la propriété userid est de type int
        /// </summary>
        [TestMethod]
        public void PropertyUserIdIsTypeString()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "userid") == typeof(int));
        }

        /// <summary>
        /// Vérifie si la propriété user existe
        /// </summary>
        [TestMethod]
        public void PropertyUserExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "user"));
        }

        /// <summary>
        /// Vérifie si la propriété user est de type point
        /// </summary>
        [TestMethod]
        public void PropertyUserTypePoint()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "user?") == GetType("point"));
        }






    }
}