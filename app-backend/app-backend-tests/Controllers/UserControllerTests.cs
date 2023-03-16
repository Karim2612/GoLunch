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
    public class UserControllerTests : AssemblyLoader
    {
        private const string TYPE_NAME = "usercontroller";

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
        /// Vérifie que le type contienne une méthode nommée getallusers
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodGetAllUsers()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "getallusers"));
        }

        /// <summary>
        /// Vérifie que la méthode getallusers soit public
        /// </summary>
        [TestMethod]
        public void MethodGetAllUsersIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "getallusers")?.IsPublic);
        }
        /// <summary>
        /// Vérifie que la méthode getall contienne 0 paramètre
        /// </summary>
        [TestMethod]
        public void MethodGetAllontainsNoParameter()
        {
            Assert.IsTrue(base.GetMethodParameters(TYPE_NAME, "getallusers").Count == 0);
        }


        /// <summary>
        /// Vérifie que le type contienne une méthode nommée getuser
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodGetUser()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "getuser"));
        }

        /// <summary>
        /// Vérifie que la méthode getuser soit public
        /// </summary>
        [TestMethod]
        public void MethodGetUserIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "getuser")?.IsPublic);
        }



        /// <summary>
        /// Vérifie que le type contienne une méthode nommée patch
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodPatch()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "patch"));
        }

        /// <summary>
        /// Vérifie que la méthode patch soit public
        /// </summary>
        [TestMethod]
        public void MethodPatchIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "patch")?.IsPublic);
        }



        /// <summary>
        /// Vérifie que le type contienne une méthode nommée updateuser
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodUpdateUser()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "updateuser"));
        }

        /// <summary>
        /// Vérifie que la méthode updateuser soit public
        /// </summary>
        [TestMethod]
        public void MethodUpdateUserIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "updateuser")?.IsPublic);
        }



        /// <summary>
        /// Vérifie que le type contienne une méthode nommée deleteuser
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodDeleteUser()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "deleteuser"));
        }

        /// <summary>
        /// Vérifie que la méthode deleteuser soit public
        /// </summary>
        [TestMethod]
        public void MethodDeleteUserIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "deleteuser")?.IsPublic);
        }

        /// <summary>
        /// Vérifie que la méthode deleteuser contienne seulement 1 paramètre
        /// </summary>
        [TestMethod]
        public void MethodDeleteUserContainsNoParameter()
        {
            Assert.IsTrue(base.GetRuntimeMethodParameters(TYPE_NAME, "deleteuser").Count == 1);
        }

        /// <summary>
        /// Vérifie que le paramètre de la méthode deleteuser soit de type générique
        /// </summary>
        [TestMethod]
        public void MethodDeleteUserHasGenericParameter()
        {
            Assert.IsTrue(base.GetRuntimeMethodParameters(TYPE_NAME, "deleteuser").First().ParameterType == typeof(int));
        }

        /// <summary>
        /// Vérifie que la méthode delete ait les bons types de paramètres
        /// </summary>
        [TestMethod]
        public void MethodDeleteHasRightParametersTypes()
        {
            bool check = true;
            var rightTypes = new List<Type>() { typeof(int) };
            var paramTypes = base.GetMethodParametersTypes(TYPE_NAME, "deleteuser");

            for (int i = 0; i < rightTypes.Count; i++)
            {
                if (rightTypes[i] != paramTypes[i])
                    check = false;
            }
            Assert.IsTrue(check);
        }



        /// <summary>
        /// Vérifie que le type contienne une méthode nommée userexists
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodUserExists()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "userexists"));
        }

        /// <summary>
        /// Vérifie que la méthode userexists soit public
        /// </summary>
        [TestMethod]
        public void MethodUserExistsIsPrivate()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "userexists")?.IsPrivate);
        }

        /// <summary>
        /// Vérifie que la méthode userexists retourne un type bool
        /// </summary>
        [TestMethod]
        public void MethodUserExistsTypeIsBool()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "userexists")?.ReturnType == typeof(bool));
        }



        /// <summary>
        /// Vérifie que le type contienne une méthode nommée getfavoriterestaurants
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodGetFavoriteRestaurants()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "getfavoriterestaurants"));
        }

        /// <summary>
        /// Vérifie que la méthode getfavoriterestaurants soit public
        /// </summary>
        [TestMethod]
        public void MethodGetFavoriteRestaurantsIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "getfavoriterestaurants")?.IsPublic);
        }

        /// <summary>
        /// Vérifie que la méthode getfavoriterestaurants contienne seulement 7 paramètre
        /// </summary>
        [TestMethod]
        public void MethodGetFavoriteRestaurantsContains1Parameterc()
        {
            Assert.IsTrue(base.GetMethodParameters(TYPE_NAME, "getfavoriterestaurants").Count == 1);
        }
    }
}