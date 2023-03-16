using app_backend;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ap_backend_tests;

namespace app_backend_tests.Models

{
    [TestClass]
    public class MenuModelTests : AssemblyLoader
    {
        private const string TYPE_NAME = "menu";
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
        /// Vérifie si la propriété entree existe
        /// </summary>
        [TestMethod]
        public void PropertyEntreeExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "entree"));
        }

        /// <summary>
        /// Vérifie si la propriété entree est de type string
        /// </summary>
        [TestMethod]
        public void PropertyEntreeIsTypeString()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "entree") == typeof(string));
        }

        /// <summary>
        /// Vérifie si la propriété plat existe
        /// </summary>
        [TestMethod]
        public void PropertyPlatExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "plat"));
        }

        /// <summary>
        /// Vérifie si la propriété plat est de type string
        /// </summary>
        [TestMethod]
        public void PropertyPlatIsTypeString()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "plat") == typeof(string));
        }

        /// <summary>
        /// Vérifie si la propriété dessert existe
        /// </summary>
        [TestMethod]
        public void PropertyDessertExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "dessert"));
        }

        /// <summary>
        /// Vérifie si la propriété dessert est de type string
        /// </summary>
        [TestMethod]
        public void PropertyDessertIsTypeString()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "dessert") == typeof(string));
        }

        /// <summary>
        /// Vérifie si la propriété prix existe
        /// </summary>
        [TestMethod]
        public void PropertyPrixExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "prix"));
        }

        /// <summary>
        /// Vérifie si la propriété prix est de type double
        /// </summary>
        [TestMethod]
        public void PropertyPrixTypeDouble()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "prix") == typeof(double?));
        }

        /// <summary>
        /// Vérifie si la propriété menutype existe
        /// </summary>
        [TestMethod]
        public void PropertyMenuTypeExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "type"));
        }

        /// <summary>
        /// Vérifie si la propriété menutype est de type category
        /// </summary>
        [TestMethod]
        public void PropertyMenuTypeIsTypeType()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "menutype?") == GetType("menutype?"));
        }

        /// <summary>
        /// Vérifie si la propriété localisation existe
        /// </summary>
        [TestMethod]
        public void PropertyLocalisationExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "localisation"));
        }

        /// <summary>
        /// Vérifie si la propriété localisation est de type localisation
        /// </summary>
        [TestMethod]
        public void PropertyLocalisationIsTypeLocalisation()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "localisation?") == GetType("localisation?"));
        }

        /// <summary>
        /// Vérifie si la propriété inclusboisson existe
        /// </summary>
        [TestMethod]
        public void PropertyInclusBoissonExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "inclusboisson"));
        }

        /// <summary>
        /// Vérifie si la propriété inclusboisson est de type bool
        /// </summary>
        [TestMethod]
        public void PropertyInclusBoissonTypeBool()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "inclusboisson") == typeof(bool?));
        }

        /// <summary>
        /// Vérifie si la propriété incluscafe existe
        /// </summary>
        [TestMethod]
        public void PropertyInclusCafeExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "incluscafe"));
        }

        /// <summary>
        /// Vérifie si la propriété incluscafe est de type bool
        /// </summary>
        [TestMethod]
        public void PropertyInclusCafeTypeBool()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "incluscafe") == typeof(bool?));
        }

        /// <summary>
        /// Vérifie si la propriété datemenu existe
        /// </summary>
        [TestMethod]
        public void PropertyDateMenuExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "datemenu"));
        }

        /// <summary>
        /// Vérifie si la propriété datemenu est de type datetime
        /// </summary>
        [TestMethod]
        public void PropertyDateMenuIsTypeDateTime()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "datemenu?") == GetType("datetime?"));
        }

        /// <summary>
        /// Vérifie si la propriété datemodif existe
        /// </summary>
        [TestMethod]
        public void PropertyDateModifExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "datemenu"));
        }

        /// <summary>
        /// Vérifie si la propriété datemodif est de type datetime
        /// </summary>
        [TestMethod]
        public void PropertyDateModifIsTypeDateTime()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "datemodif?") == GetType("datetime?"));
        }



    }
}