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
    public class MenuControllerTests : AssemblyLoader
    {
        private const string TYPE_NAME = "menucontroller";

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
        /// Vérifie que le type contienne une méthode nommée getallmenus
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodGetAllMenus()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "getallmenus"));
        }

        /// <summary>
        /// Vérifie que la méthode getallmenus soit public
        /// </summary>
        [TestMethod]
        public void MethodGetAllMenusIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "getallmenus")?.IsPublic);
        }
        /// <summary>
        /// Vérifie que la méthode getall contienne 0 paramètre
        /// </summary>
        [TestMethod]
        public void MethodGetAllontainsNoParameter()
        {
            Assert.IsTrue(base.GetMethodParameters(TYPE_NAME, "getallmenus").Count == 0);
        }



        /// <summary>
        /// Vérifie que le type contienne une méthode nommée getmenu
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodGetMenu()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "getmenu"));
        }

        /// <summary>
        /// Vérifie que la méthode getmenu soit public
        /// </summary>
        [TestMethod]
        public void MethodGetMenuIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "getmenu")?.IsPublic);
        }

       

        /// <summary>
        /// Vérifie que le type contienne une méthode nommée postmenu
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodPostMenu()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "postmenu"));
        }

        /// <summary>
        /// Vérifie que la méthode postmenu soit public
        /// </summary>
        [TestMethod]
        public void MethodPostMenuIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "postmenu")?.IsPublic);
        }

        /// <summary>
        /// Vérifie que la méthode postmenu contienne seulement 1 paramètre
        /// </summary>
        [TestMethod]
        public void MethodPostMenuContainsOneParameter()
        {
            var x = (base.GetMethodParameters(TYPE_NAME, "postmenu").Count);
            Assert.AreEqual(1, x);
        }


        /// <summary>
        /// Vérifie que le type contienne une méthode nommée updatemenu
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodUpdateMenu()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "updatemenu"));
        }

        /// <summary>
        /// Vérifie que la méthode updatemenu soit public
        /// </summary>
        [TestMethod]
        public void MethodUpdateMenuIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "updatemenu")?.IsPublic);
        }



        /// <summary>
        /// Vérifie que le type contienne une méthode nommée deletemenu
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodDeleteMenu()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "deletemenu"));
        }

        /// <summary>
        /// Vérifie que la méthode deletemenu soit public
        /// </summary>
        [TestMethod]
        public void MethodDeleteMenuIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "deletemenu")?.IsPublic);
        }

        /// <summary>
        /// Vérifie que le paramètre de la méthode deletemenu soit de type générique
        /// </summary>
        [TestMethod]
        public void MethodDeleteMenuHasGenericParameter()
        {
            Assert.IsTrue(base.GetRuntimeMethodParameters(TYPE_NAME, "deletemenu").First().ParameterType == typeof(int));
        }

        /// <summary>
        /// Vérifie que la méthode delete ait les bons types de paramètres
        /// </summary>
        [TestMethod]
        public void MethodDeleteHasRightParametersTypes()
        {
            bool check = true;
            var rightTypes = new List<Type>() { typeof(int) };
            var paramTypes = base.GetMethodParametersTypes(TYPE_NAME, "deletemenu");

            for (int i = 0; i < rightTypes.Count; i++)
            {
                if (rightTypes[i] != paramTypes[i])
                    check = false;
            }
            Assert.IsTrue(check);
        }

        /// <summary>
        /// Vérifie que la méthode deletemenu contienne seulement 1 paramètre
        /// </summary>
        [TestMethod]
        public void MethoDeleteMenuContainsNoParameter()
        {
            Assert.IsTrue(base.GetRuntimeMethodParameters(TYPE_NAME, "deletemenu").Count == 1);
        }
        


        /// <summary>
        /// Vérifie que le type contienne une méthode nommée menuexists
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodMenuExists()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "menuexists"));
        }

        /// <summary>
        /// Vérifie que la méthode menuexists soit public
        /// </summary>
        [TestMethod]
        public void MethodMenuExistsIsPrivate()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "menuexists")?.IsPrivate);
        }

        /// <summary>
        /// Vérifie que la méthode menuexists retourne un type bool
        /// </summary>
        [TestMethod]
        public void MethodMenuExistsTypeIsBool()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "menuexists")?.ReturnType == typeof(bool));
        }



        /// <summary>
        /// Vérifie que le type contienne une méthode nommée filtres
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodFiltres()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "filtres"));
        }

        /// <summary>
        /// Vérifie que la méthode filtres soit public
        /// </summary>
        [TestMethod]
        public void MethodFiltresIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "filtres")?.IsPublic);
        }

        /// <summary>
        /// Vérifie que la méthode filtres contienne seulement 7 paramètre
        /// </summary>
        [TestMethod]
        public void MethodFiltresContains7Parameterc()
        {
            Assert.IsTrue(base.GetMethodParameters(TYPE_NAME, "filtres").Count == 7);
        }



        /// <summary>
        /// Vérifie que le type contienne une méthode nommée rechercheplat
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodRecherchePlat()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "rechercheplat"));
        }

        /// <summary>
        /// Vérifie que la méthode rechercheplat contienne seulement 1 paramètre
        /// </summary>
        [TestMethod]
        public void MethodRecherchePlatContains1Parameterc()
        {
            Assert.IsTrue(base.GetMethodParameters(TYPE_NAME, "rechercheplat").Count == 1);
        }


        /// <summary>
        /// Vérifie que le type contienne une méthode nommée rechercheprix
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodRecherchePrix()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "rechercheprix"));
        }

        /// <summary>
        /// Vérifie que la méthode rechercheplat contienne seulement 1 paramètre
        /// </summary>
        [TestMethod]
        public void MethodRecherchePrixContains1Parameterc()
        {
            Assert.IsTrue(base.GetMethodParameters(TYPE_NAME, "rechercheprix").Count == 2);
        }


        /// <summary>
        /// Vérifie que le type contienne une méthode nommée patchrestaurant
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodPatchRestaurant()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "patchrestaurant"));
        }

        /// <summary>
        /// Vérifie que la méthode patchrestaurant soit public
        /// </summary>
        [TestMethod]
        public void MethodPatchRestaurantIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "patchrestaurant")?.IsPublic);
        }


        /// <summary>
        /// Vérifie que le type contienne une méthode nommée getlocalisationbylatlong
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodGetLocalisationByLatLong()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "getlocalisationbylatlong"));
        }

        /// <summary>
        /// Vérifie que la méthode getlocalisationbylatlong soit public
        /// </summary>
        [TestMethod]
        public void MethodGetLocalisationByLatLongIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "getlocalisationbylatlong")?.IsPublic);
        }

        /// <summary>
        /// Vérifie que la méthode getlocalisationbylatlong contienne seulement 3 paramètre
        /// </summary>
        [TestMethod]
        public void MethodGetLocalisationByLatLongContains3Parameterc()
        {
            Assert.IsTrue(base.GetMethodParameters(TYPE_NAME, "getlocalisationbylatlong").Count == 3);
        }
    }
}