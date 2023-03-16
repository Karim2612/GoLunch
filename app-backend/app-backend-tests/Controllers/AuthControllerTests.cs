using Microsoft.VisualStudio.TestTools.UnitTesting;
using ap_backend_tests;



namespace app_backend_tests.Controllers
{
    [TestClass]
    public class AuthControllerTests : AssemblyLoader
    {
        private const string TYPE_NAME = "authcontroller";

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
        /// Vérifie que le type contienne une méthode nommée checkuser
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodCheckUser()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "checkuser"));
        }

        /// <summary>
        /// Vérifie que la méthode checkuser soit public
        /// </summary>
        [TestMethod]
        public void MethodCheckUserIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "checkuser")?.IsPublic);
        }



        /// <summary>
        /// Vérifie que le type contienne une méthode nommée register
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodRegister()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "register"));
        }

        /// <summary>
        /// Vérifie que la méthode register soit public
        /// </summary>
        [TestMethod]
        public void MethodRegisterIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "register")?.IsPublic);
        }



        /// <summary>
        /// Vérifie que le type contienne une méthode nommée login
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodLogin()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "login"));
        }

        /// <summary>
        /// Vérifie que la méthode login soit public
        /// </summary>
        [TestMethod]
        public void MethodLoginIsPublic()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "login")?.IsPublic);
        }



        /// <summary>
        /// Vérifie que le type contienne une méthode nommée createtoken
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodCreateToken()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "createtoken"));
        }

        /// <summary>
        /// Vérifie que la méthode createtoken soit Private
        /// </summary>
        [TestMethod]
        public void MethodCreateTokenIsPrivate()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "createtoken")?.IsPrivate);
        }



        /// <summary>
        /// Vérifie que le type contienne une méthode nommée createpasswordhash
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodCreatePasswordHash()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "createpasswordhash"));
        }

        /// <summary>
        /// Vérifie que la méthode createpasswordhash soit Private
        /// </summary>
        [TestMethod]
        public void MethodCreatePasswordHashIsPrivate()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "createpasswordhash")?.IsPrivate);
        }



        /// <summary>
        /// Vérifie que le type contienne une méthode nommée verifypasswordhash
        /// </summary>
        [TestMethod]
        public void TypeContainsMethodVerifyPasswordHash()
        {
            Assert.IsNotNull(base.GetMethod(TYPE_NAME, "verifypasswordhash"));
        }

        /// <summary>
        /// Vérifie que la méthode verifypasswordhash soit public
        /// </summary>
        [TestMethod]
        public void MethodVerifyPasswordHashIsPrivate()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "verifypasswordhash")?.IsPrivate);
        }

        /// <summary>
        /// Vérifie que la méthode verifypasswordhash retourne un type bool
        /// </summary>
        [TestMethod]
        public void MethodVerifyPasswordHashTypeIsBool()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "verifypasswordhash")?.ReturnType == typeof(bool));
        }

        /// <summary>
        /// Vérifie que la méthode verifypasswordhash contienne seulement 7 paramètre
        /// </summary>
        [TestMethod]
        public void MethodVerifyPasswordHashContains3Parameterc()
        {
            Assert.IsTrue(base.GetMethodParameters(TYPE_NAME, "verifypasswordhash").Count == 3);
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
        /// Vérifie que la méthode userexists retourne un type bool
        /// </summary>
        [TestMethod]
        public void MethodUserExistsTypeIsBool()
        {
            Assert.IsTrue(base.GetMethod(TYPE_NAME, "userexists")?.ReturnType == typeof(bool));
        }
    }
}