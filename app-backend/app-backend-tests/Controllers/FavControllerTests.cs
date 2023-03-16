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
    public class FavControllerTests : AssemblyLoader
    {
        private const string TYPE_NAME = "favcontroller";

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
        /// Vérifie que le type contienne une méthode nommée getallfavoris
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodGetAllFavoris()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "getallfavoris"));
        }

        /// <summary>
        /// Vérifie que la méthode getallfavoris soit public
        /// </summary>
        [TestMethod]
        public void MethodGetAllFavorisIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "getallfavoris")?.IsPublic);
        }
        /// <summary>
        /// Vérifie que la méthode getall contienne 0 paramètre
        /// </summary>
        [TestMethod]
        public void MethodGetAllontainsNoParameter()
        {
            Assert.IsTrue(base.GetMethodParameters(TYPE_NAME, "getallfavoris").Count == 0);
        }


        /// <summary>
        /// Vérifie que le type contienne une méthode nommée postfavortie
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodPostFavortie()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "postfavortie"));
        }

        /// <summary>
        /// Vérifie que la méthode postfavortie soit public
        /// </summary>
        [TestMethod]
        public void MethodPostFavortieIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "postfavortie")?.IsPublic);
        }
  


        /// <summary>
        /// Vérifie que le type contienne une méthode nommée deletefavoris
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodDeleteFavoris()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "deletefavoris"));
        }

        /// <summary>
        /// Vérifie que la méthode deletefavoris soit public
        /// </summary>
        [TestMethod]
        public void MethodDeleteFavorisIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "deletefavoris")?.IsPublic);
        }

        /// <summary>
        /// Vérifie que la méthode deletefavoris contienne seulement 1 paramètre
        /// </summary>
        [TestMethod]
        public void MethoDeleteFavorisContainsNoParameter()
        {
            Assert.IsTrue(base.GetRuntimeMethodParameters(TYPE_NAME, "deletefavoris").Count == 1);
        }

        /// <summary>
        /// Vérifie que le paramètre de la méthode deletefavoris soit de type générique
        /// </summary>
        [TestMethod]
        public void MethodDeleteFavorisHasGenericParameter()
        {
            Assert.IsTrue(base.GetRuntimeMethodParameters(TYPE_NAME, "deletefavoris").First().ParameterType == typeof(int));
        }

        /// <summary>
        /// Vérifie que la méthode delete ait les bons types de paramètres
        /// </summary>
        [TestMethod]
        public void MethodDeleteHasRightParametersTypes()
        {
            bool check = true;
            var rightTypes = new List<Type>() { typeof(int) };
            var paramTypes = base.GetMethodParametersTypes(TYPE_NAME, "deletefavoris");

            for (int i = 0; i < rightTypes.Count; i++)
            {
                if (rightTypes[i] != paramTypes[i])
                    check = false;
            }
            Assert.IsTrue(check);
        }



        /// <summary>
        /// Vérifie que le type contienne une méthode nommée getfavorite
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodGetFavorite()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "getfavorite"));
        }

        /// <summary>
        /// Vérifie que la méthode getfavorite soit public
        /// </summary>
        [TestMethod]
        public void MethodGetFavoriteIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "getfavorite")?.IsPublic);
        }



        /// <summary>
        /// Vérifie que le type contienne une méthode nommée favorisexists
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodFavorisExists()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "favorisexists"));
        }

        /// <summary>
        /// Vérifie que la méthode favorisexists soit public
        /// </summary>
        [TestMethod]
        public void MethodFavorisExistsIsPrivate()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "favorisexists")?.IsPrivate);
        }

        /// <summary>
        /// Vérifie que la méthode favorisexists retourne un type bool
        /// </summary>
        [TestMethod]
        public void MethodFavorisExistsTypeIsBool()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "favorisexists")?.ReturnType == typeof(bool));
        }


    }
}