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
    public class LocalisationControllerTests : AssemblyLoader
    {
        private const string TYPE_NAME = "localisationcontroller";

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
        /// Vérifie que le type contienne une méthode nommée getalllocalisations
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodGetAllLocalisations()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "getalllocalisations"));
        }

        /// <summary>
        /// Vérifie que la méthode getalllocalisations soit public
        /// </summary>
        [TestMethod]
        public void MethodGetAllLocalisationsIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "getalllocalisations")?.IsPublic);
        }
        /// <summary>
        /// Vérifie que la méthode getall contienne 0 paramètre
        /// </summary>
        [TestMethod]
        public void MethodGetAllontainsNoParameter()
        {
            Assert.IsTrue(base.GetMethodParameters(TYPE_NAME, "getalllocalisations").Count == 0);
        }


        /// <summary>
        /// Vérifie que le type contienne une méthode nommée getlocalisation
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodGetLocalisation()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "getlocalisation"));
        }

        /// <summary>
        /// Vérifie que la méthode getlocalisation soit public
        /// </summary>
        [TestMethod]
        public void MethodGetLocalisationIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "getlocalisation")?.IsPublic);
        }



        /// <summary>
        /// Vérifie que le type contienne une méthode nommée postlocalisation
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodPostLocalisation()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "postlocalisation"));
        }

        /// <summary>
        /// Vérifie que la méthode postlocalisation soit public
        /// </summary>
        [TestMethod]
        public void MethodPostLocalisationIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "postlocalisation")?.IsPublic);
        }



        /// <summary>
        /// Vérifie que le type contienne une méthode nommée updatelocalisation
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodUpdateLocalisation()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "updatelocalisation"));
        }

        /// <summary>
        /// Vérifie que la méthode updatelocalisation soit public
        /// </summary>
        [TestMethod]
        public void MethodUpdateLocalisationIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "updatelocalisation")?.IsPublic);
        }



        /// <summary>
        /// Vérifie que le type contienne une méthode nommée deletelocalisation
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodDeleteLocalisation()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "deletelocalisation"));
        }

        /// <summary>
        /// Vérifie que la méthode deletelocalisation soit public
        /// </summary>
        [TestMethod]
        public void MethodDeleteLocalisationIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "deletelocalisation")?.IsPublic);
        }

        /// <summary>
        /// Vérifie que la méthode deletelocalisation contienne seulement 1 paramètre
        /// </summary>
        [TestMethod]
        public void MethoDeleteLocalisationContainsNoParameter()
        {
            Assert.IsTrue(base.GetRuntimeMethodParameters(TYPE_NAME, "deletelocalisation").Count == 1);
        }

        /// <summary>
        /// Vérifie que le paramètre de la méthode deletelocalisation soit de type générique
        /// </summary>
        [TestMethod]
        public void MethodDeleteLocalisationHasGenericParameter()
        {
            Assert.IsTrue(base.GetRuntimeMethodParameters(TYPE_NAME, "deletelocalisation").First().ParameterType == typeof(int));
        }

        /// <summary>
        /// Vérifie que la méthode delete ait les bons types de paramètres
        /// </summary>
        [TestMethod]
        public void MethodDeleteHasRightParametersTypes()
        {
            bool check = true;
            var rightTypes = new List<Type>() { typeof(int) };
            var paramTypes = base.GetMethodParametersTypes(TYPE_NAME, "deletelocalisation");

            for (int i = 0; i < rightTypes.Count; i++)
            {
                if (rightTypes[i] != paramTypes[i])
                    check = false;
            }
            Assert.IsTrue(check);
        }



        /// <summary>
        /// Vérifie que le type contienne une méthode nommée getlocalisationbycity
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodGetLocalisationByCity()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "getlocalisationbycity"));
        }

        /// <summary>
        /// Vérifie que la méthode getlocalisationbycity soit public
        /// </summary>
        [TestMethod]
        public void MethodGetLocalisationByCityIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "getlocalisationbycity")?.IsPublic);
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
        /// Vérifie que le type contienne une méthode nommée localisationexists
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodLocalisationExists()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "localisationexists"));
        }

        /// <summary>
        /// Vérifie que la méthode localisationexists soit public
        /// </summary>
        [TestMethod]
        public void MethodLocalisationExistsPrivate()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "localisationexists")?.IsPrivate);
        }

        /// <summary>
        /// Vérifie que la méthode localisationexists retourne un type bool
        /// </summary>
        [TestMethod]
        public void MethodLocalisationExistsTypeIsBool()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "localisationexists")?.ReturnType == typeof(bool));
        }



        /// <summary>
        /// Vérifie que le type contienne une méthode nommée firstlettertoupper
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodFirstLetterToUpper()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "firstlettertoupper"));
        }

        /// <summary>
        /// Vérifie que la méthode firstlettertoupper soit public
        /// </summary>
        [TestMethod]
        public void MethodFirstLetterToUpperPrivate()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "firstlettertoupper")?.IsPrivate);
        }

        /// <summary>
        /// Vérifie que la méthode firstlettertoupper retourne un type bool
        /// </summary>
        [TestMethod]
        public void MethodFirstLetterToUpperTypeIsBool()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "firstlettertoupper")?.ReturnType == typeof(string));
        }
    }
}