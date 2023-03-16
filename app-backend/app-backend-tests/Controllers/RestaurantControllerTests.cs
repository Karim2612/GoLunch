using Microsoft.VisualStudio.TestTools.UnitTesting;
using ap_backend_tests;
using System.Threading.Tasks;
using System;
using Moq;
using app_backend.Controllers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace app_backend_tests.Controllers
{
    [TestClass]
    public class RestaurantControllerTests : AssemblyLoader
    {
        private const string TYPE_NAME = "restaurantcontroller";

        /// <summary>
        /// Vérifie si le type existe
        /// </summary>
        [TestMethod]
        public void TypeExist()
        {
            Assert.IsNotNull(base.GetType(TYPE_NAME));
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
        /// Vérifie que le type contienne une méthode nommée getallrestaurants
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodGetAllRestaurants()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "getallrestaurants"));
        }

        /// <summary>
        /// Vérifie que la méthode getallrestaurants soit public
        /// </summary>
        [TestMethod]
        public void MethodGetAllRestaurantsIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "getallrestaurants")?.IsPublic);
        }
        /// <summary>
        /// Vérifie que la méthode getall contienne 0 paramètre
        /// </summary>
        [TestMethod]
        public void MethodGetAllontainsNoParameter()
        {
            Assert.IsTrue(base.GetMethodParameters(TYPE_NAME, "getallrestaurants").Count == 0);
        }


        /// <summary>
        /// Vérifie que le type contienne une méthode nommée getrestaurant
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodGetRestaurant()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "getrestaurant"));
        }

        /// <summary>
        /// Vérifie que la méthode getrestaurant soit public
        /// </summary>
        [TestMethod]
        public void MethodGetRestaurantIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "getrestaurant")?.IsPublic);
        }



        /// <summary>
        /// Vérifie que le type contienne une méthode nommée postrestaurant
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodPostRestaurant()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "postrestaurant"));
        }

        /// <summary>
        /// Vérifie que la méthode postrestaurant soit public
        /// </summary>
        [TestMethod]
        public void MethodPostRestaurantIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "postrestaurant")?.IsPublic);
        }



        /// <summary>
        /// Vérifie que le type contienne une méthode nommée updaterestaurant
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodUpdateRestaurant()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "updaterestaurant"));
        }

        /// <summary>
        /// Vérifie que la méthode updaterestaurant soit public
        /// </summary>
        [TestMethod]
        public void MethodUpdateRestaurantIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "updaterestaurant")?.IsPublic);
        }



        /// <summary>
        /// Vérifie que le type contienne une méthode nommée deleterestaurant
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodDeleteRestaurant()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "deleterestaurant"));
        }

        /// <summary>
        /// Vérifie que la méthode deleterestaurant soit public
        /// </summary>
        [TestMethod]
        public void MethodDeleteRestaurantIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "deleterestaurant")?.IsPublic);
        }

        /// <summary>
        /// Vérifie que la méthode deleterestaurant contienne seulement 1 paramètre
        /// </summary>
        [TestMethod]
        public void MethoDeleteRestaurantContainsNoParameter()
        {
            Assert.IsTrue(base.GetRuntimeMethodParameters(TYPE_NAME, "deleterestaurant").Count == 1);
        }

        /// <summary>
        /// Vérifie que le paramètre de la méthode deleterestaurant soit de type générique
        /// </summary>
        [TestMethod]
        public void MethodDeleteRestaurantHasGenericParameter()
        {
            Assert.IsTrue(base.GetRuntimeMethodParameters(TYPE_NAME, "deleterestaurant").First().ParameterType == typeof(int));
        }

        /// <summary>
        /// Vérifie que la méthode delete ait les bons types de paramètres
        /// </summary>
        [TestMethod]
        public void MethodDeleteHasRightParametersTypes()
        {
            bool check = true;
            var rightTypes = new List<Type>() { typeof(int) };
            var paramTypes = base.GetMethodParametersTypes(TYPE_NAME, "deleterestaurant");

            for (int i = 0; i < rightTypes.Count; i++)
            {
                if (rightTypes[i] != paramTypes[i])
                    check = false;
            }
            Assert.IsTrue(check);
        }



        /// <summary>
        /// Vérifie que le type contienne une méthode nommée restaurantexists
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodRestaurantExists()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "restaurantexists"));
        }

        /// <summary>
        /// Vérifie que la méthode restaurantexists soit public
        /// </summary>
        [TestMethod]
        public void MethodRestaurantExistsIsPrivate()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "restaurantexists")?.IsPrivate);
        }

        /// <summary>
        /// Vérifie que la méthode restaurantexists retourne un type bool
        /// </summary>
        [TestMethod]
        public void MethodRestaurantExistsTypeIsBool()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "restaurantexists")?.ReturnType == typeof(bool));
        }
    }
}