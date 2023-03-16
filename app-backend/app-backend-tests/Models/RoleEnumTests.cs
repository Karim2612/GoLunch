using Microsoft.VisualStudio.TestTools.UnitTesting;
using ap_backend_tests;


namespace app_backend_tests.Models
{
    [TestClass]
    public class RoleEnumTests : AssemblyLoader
{
    private const string TYPE_NAME = "role";

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
    /// Vérifie si c'est un enum
    /// </summary>
    [TestMethod]
    public void TypeIsEnum()
    {
        Assert.IsTrue(base.GetType(TYPE_NAME)?.IsEnum ?? false);
    }

    /// <summary>
    /// Vérifie si'il contient au moins 3 valeurs
    /// </summary>
    [TestMethod]
    public void ContainsAtLeast3Values()
    {
        Assert.IsTrue(base.GetType(TYPE_NAME)?.GetEnumNames()?.Length >= 3);
    }

    /// <summary>
    /// Vérifie les valeurs 
    /// </summary>
    [TestMethod]
    public void FirstValueShouldBeLow()
    {
        Assert.IsTrue(base.GetType(TYPE_NAME)?.GetEnumNames()[0].ToLower() == "user");
        Assert.IsTrue(base.GetType(TYPE_NAME)?.GetEnumNames()[1].ToLower() == "restaurateur");
        Assert.IsTrue(base.GetType(TYPE_NAME)?.GetEnumNames()[2].ToLower() == "admin");
    }
}
}