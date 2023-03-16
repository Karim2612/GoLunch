using app_backend;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ap_backend_tests;


namespace app_backend_tests.Models
{
    [TestClass]
    public class LocalisationModelTests : AssemblyLoader
    {
        private const string TYPE_NAME = "localisation";
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
        /// Vérifie si la propriété adresse existe
        /// </summary>
        [TestMethod]
        public void PropertyAdresseExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "adresse"));
        }

        /// <summary>
        /// Vérifie si la propriété adresse est de type string
        /// </summary>
        [TestMethod]
        public void PropertyAdresseIsTypeString()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "adresse") == typeof(string));
        }

        /// <summary>
        /// Vérifie si la propriété cp existe
        /// </summary>
        [TestMethod]
        public void PropertyCPExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "cp"));
        }

        /// <summary>
        /// Vérifie si la propriété cp est de type int
        /// </summary>
        [TestMethod]
        public void PropertyCPIsTypeString()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "cp") == typeof(int?));
        }

        /// <summary>
        /// Vérifie si la propriété ville existe
        /// </summary>
        [TestMethod]
        public void PropertyVilleExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "ville"));
        }

        /// <summary>
        /// Vérifie si la propriété ville est de type string
        /// </summary>
        [TestMethod]
        public void PropertyVilleIsTypeString()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "ville") == typeof(string));
        }

        /// <summary>
        /// Vérifie si la propriété pays existe
        /// </summary>
        [TestMethod]
        public void PropertyPaysExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "pays"));
        }

        /// <summary>
        /// Vérifie si la propriété pays est de type string
        /// </summary>
        [TestMethod]
        public void PropertyPaysIsTypeString()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "pays") == typeof(string));
        }

        /// <summary>
        /// Vérifie si la propriété position existe
        /// </summary>
        [TestMethod]
        public void PropertyPositionExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "position"));
        }

        /// <summary>
        /// Vérifie si la propriété position est de type point
        /// </summary>
        [TestMethod]
        public void PropertyPositionTypePoint()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "position?") == GetType("point"));
        }

        /// <summary>
        /// Vérifie si la propriété poslatitude existe
        /// </summary>
        [TestMethod]
        public void PropertyPosLatitudeExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "poslatitude"));
        }

        /// <summary>
        /// Vérifie si la propriété poslatitude est de type double
        /// </summary>
        [TestMethod]
        public void PropertyPosLatitudeTypeDouble()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "poslatitude") == typeof(double?));
        }

        /// <summary>
        /// Vérifie si la propriété poslongitude existe
        /// </summary>
        [TestMethod]
        public void PropertyPosLongitudeExist()
        {
            Assert.IsNotNull(GetProperty(TYPE_NAME, "poslongitude"));
        }

        /// <summary>
        /// Vérifie si la propriété poslongitude est de type double
        /// </summary>
        [TestMethod]
        public void PropertyPosLongitudeTypeDouble()
        {
            Assert.IsTrue(GetPropertyType(TYPE_NAME, "poslongitude") == typeof(double?));
        }




    }
}