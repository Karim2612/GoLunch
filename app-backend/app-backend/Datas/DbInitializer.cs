using NetTopologySuite;
using NetTopologySuite.Geometries;
using app_backend.Models;
using System.Linq;

namespace app_backend.Datas
{
    public static class DbInitializer
    {
        public static void Initialize(GolunchDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Localisations.Any())
            {
                //Flo WHEN using TopologySuite 
                var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
                var localisations = new Localisation[]
                {
                    //Flo WITHOUT TopologySuite
                    //new Localisation { Adresse = "EPSIC", CP = 1002, Ville = "Lausanne", Canton = "Vaud", Pays = "Suisse", PosLatitude = 46.523675186478556, PosLongitude = 6.620335584657289 },
                    //new Localisation { Adresse = "McDonald du Flon", CP = 1002, Ville = "Lausanne", Canton = "Vaud", Pays = "Suisse", PosLatitude = 46.52233887561809, PosLongitude = 6.625953320517623 },
                    //new Localisation { Adresse = "Chez Flow", CP = 1880, Ville = "Bex", Canton = "Vaud", Pays = "Suisse", PosLatitude = 46.250946768880034, PosLongitude = 7.00612773252572 },
                    //new Localisation { Adresse = "Indian Massala resto préféré de Flo près de chez lui", CP = 1880, Ville = "Bex", Canton = "Vaud", Pays = "Suisse", PosLatitude = 46.24797097454932, PosLongitude = 7.005424723677462 },
                    //new Localisation { Adresse = "La Nonna restaurant italien où Flo mange des Pizzas", CP = 1860, Ville = "Aigle", Canton = "Vaud", Pays = "Suisse", PosLatitude = 46.3184351660941, PosLongitude = 6.96482170537787 }
                    // Flo WITH TopologySuite
                    new Localisation { Adresse = "Av. Louis-Ruchonnet 15", CP = 1003, Ville = "Lausanne", Canton = "Vaud", Pays = "Suisse", Position = geometryFactory.CreatePoint(new Coordinate(6.626051256865105, 46.518435402840794)), PosLatitude = 46.518435402840794, PosLongitude = 6.626051256865105 },
                    new Localisation { Adresse = "Rue Margencel 29", CP = 1860, Ville = "Aigle", Canton = "Vaud", Pays = "Suisse", Position = geometryFactory.CreatePoint(new Coordinate(6.96482170537787, 46.3184351660941)), PosLatitude = 46.3184351660941, PosLongitude = 6.96482170537787 },
                    new Localisation { Adresse = "Rue du Bourg 4", CP = 1860, Ville = "Aigle", Canton = "Vaud", Pays = "Suisse", Position = geometryFactory.CreatePoint(new Coordinate(6.968855321935623, 46.31834290070345)), PosLatitude = 46.31834290070345, PosLongitude = 6.968855321935623 },
                    new Localisation { Adresse = "Rue du Bourg 5", CP = 1860, Ville = "Aigle", Canton = "Vaud", Pays = "Suisse", Position = geometryFactory.CreatePoint(new Coordinate(6.968674832789605, 46.31835045822089)), PosLatitude = 46.31835045822089, PosLongitude = 6.968674832789605 },
                    new Localisation { Adresse = "Rte des Pépinières 14", CP = 1880, Ville = "Bex", Canton = "Vaud", Pays = "Suisse", Position = geometryFactory.CreatePoint(new Coordinate(7.005424723677462, 46.24797097454932)), PosLatitude = 46.24797097454932, PosLongitude = 7.005424723677462 },
                    new Localisation { Adresse = "Rue du Bourg 21", CP = 1860, Ville = "Aigle", Canton = "Vaud", Pays = "Suisse", Position = geometryFactory.CreatePoint(new Coordinate(6.968394461944991, 46.31893262454376)), PosLatitude = 46.31893262454376, PosLongitude = 6.968394461944991 },
                    new Localisation { Adresse = "Place du Marché 6", CP = 1860, Ville = "Aigle", Canton = "Vaud", Pays = "Suisse", Position = geometryFactory.CreatePoint(new Coordinate(6.969202010068478, 46.31773395677791)), PosLatitude = 46.31773395677791, PosLongitude = 6.969202010068478 },
                    new Localisation { Adresse = "Rue du Midi 1", CP = 1860, Ville = "Aigle", Canton = "Vaud", Pays = "Suisse", Position = geometryFactory.CreatePoint(new Coordinate(6.968951023344022, 46.31757797019017)), PosLatitude = 46.31757797019017, PosLongitude = 6.968951023344022 },
                    new Localisation { Adresse = "Place du Marché 4", CP = 1860, Ville = "Aigle", Canton = "Vaud", Pays = "Suisse", Position = geometryFactory.CreatePoint(new Coordinate(6.969449420618717, 46.31779269430828)), PosLatitude = 46.31779269430828, PosLongitude = 6.969449420618717 },
                    new Localisation { Adresse = "Av. Louis-Ruchonnet 11", CP = 1003, Ville = "Lausanne", Canton = "Vaud", Pays = "Suisse", Position = geometryFactory.CreatePoint(new Coordinate(6.626329532568995, 46.51827694433242)), PosLatitude = 46.51827694433242, PosLongitude = 6.626329532568995 },
                    new Localisation { Adresse = "EPSIC", CP = 1002, Ville = "Lausanne", Canton = "Vaud", Pays = "Suisse", Position = geometryFactory.CreatePoint(new Coordinate(6.620335584657289, 46.523675186478556)), PosLatitude = 46.523675186478556, PosLongitude = 6.620335584657289 },
                    new Localisation { Adresse = "McDonald du Flon", CP = 1002, Ville = "Lausanne", Canton = "Vaud", Pays = "Suisse", Position = geometryFactory.CreatePoint(new Coordinate(6.625953320517623, 46.52233887561809)), PosLatitude = 46.52233887561809, PosLongitude = 6.625953320517623 },
                    new Localisation { Adresse = "Chez Flow", CP = 1880, Ville = "Bex", Canton = "Vaud", Pays = "Suisse", Position = geometryFactory.CreatePoint(new Coordinate(7.00612773252572, 46.250946768880034)), PosLatitude = 46.250946768880034, PosLongitude = 7.00612773252572 },
                    new Localisation { Adresse = "Av. de la Gare 55", CP = 1880, Ville = "Bex", Canton = "Vaud", Pays = "Suisse", Position = geometryFactory.CreatePoint(new Coordinate(7.0013448539718635, 46.252007174020406)), PosLatitude = 46.252007174020406, PosLongitude = 7.0013448539718635 },

                };

                context.Localisations.AddRange(localisations);
                context.SaveChanges();
            }

            //Flo ajout de Restaurants par défaults
            if (!context.Restaurants.Any())
            {

                var restaurants = new Restaurant[]
                {
                    new Restaurant { Nom = "L'Impression", Description = "Café - Restaurant proposant des lunch's en semaine et brunch's les week-ends.", ContactEmail = "info@limpressioncafe.com", ContactTel = "+41213207171", UrlSite = "https://www.limpressioncafe.com", RestoPhoto = "https://static.wixstatic.com/media/3c9672_397d58a3a1af408c8b9e8f1c10d09316~mv2.png/v1/fill/w_640,h_412,al_c,q_85,usm_0.66_1.00_0.01,enc_auto/3c9672_397d58a3a1af408c8b9e8f1c10d09316~mv2.png", Localisation = context.Localisations.FirstOrDefault(e => e.Id == 1) },
                    new Restaurant { Nom = "La Nonna Aigle", Description = "Ristorante Pizzeria La Nonna", ContactEmail = null, ContactTel = "+41244662431", UrlSite = "http://www.la-nonna.ch", RestoPhoto = "https://img.restaurantguru.com/r493-Nonna-exterior-2021-08.jpg", Localisation = context.Localisations.FirstOrDefault(e => e.Id == 2) },
                    new Restaurant { Nom = "Xiang Long", Description = "Restaurant Chinois proposant chaque jour 3 menus du jour", ContactEmail = null, ContactTel = "+41244664200", UrlSite = "https://xianglong.ch", RestoPhoto = "https://static.mycity.travel/manage/uploads/6/28/43658/210ba2ab040dc7643cf6c958e07717764857b3e9_2000.jpg", Localisation = context.Localisations.FirstOrDefault(e => e.Id == 3) },
                    new Restaurant { Nom = "Big One Burger", Description = "Chaque jour un Burger du Jour avec salade et boisson", ContactEmail = null, ContactTel = "+41219220772", UrlSite = null, RestoPhoto = "https://media-cdn.tripadvisor.com/media/photo-s/15/5c/ae/b7/vue-de-l-exterieur-de.jpg", Localisation = context.Localisations.FirstOrDefault(e => e.Id == 4) },
                    new Restaurant { Nom = "Indian Masala", Description = "Restaurant spécialités Indienne et Sri-Lankaise ", ContactEmail = null, ContactTel = "+41779857000", UrlSite = "https://www.indian-masala-bex.ch", RestoPhoto = "https://media-cdn.tripadvisor.com/media/photo-s/1b/a5/66/71/facade-de-notre-restaurant.jpg", Localisation = context.Localisations.FirstOrDefault(e => e.Id == 5) },
                    new Restaurant { Nom = "Thai Basilic", Description = "Menus thailandais du jour ou à la carte", ContactEmail = "thaibasilicaigle@gmail.com", ContactTel = "+41244665066", UrlSite = "https://www.thaibasilic.ch/", RestoPhoto = "https://images.happycow.net/venues/500/19/36/hcmp193666_821753.jpeg", Localisation = context.Localisations.FirstOrDefault(e => e.Id == 6) },
                    new Restaurant { Nom = "Café restaurant du Marché", Description = "Menu du jour les midis en semaine de 11h30 à 14h", ContactEmail = "infos@restaurant-marche-aigle.ch", ContactTel = "+41244664649", UrlSite = "https://www.restaurant-marche-aigle.com/", RestoPhoto = "https://media-cdn.tripadvisor.com/media/photo-s/07/0b/09/fa/cafe-restaurant-du-marche.jpg", Localisation = context.Localisations.FirstOrDefault(e => e.Id == 7) },
                    new Restaurant { Nom = "Café de la Place Chez Marie", Description = "Restaurant chez Marie avec menu du jour à 18.50 CHF", ContactEmail = null, ContactTel = "+41244661084", UrlSite = null, RestoPhoto = "https://static.mycity.travel/manage/uploads/6/28/43011/8696e6b521cbad290893b712e862c7ef8f3d0323_2000.jpg", Localisation = context.Localisations.FirstOrDefault(e => e.Id == 8) },
                    new Restaurant { Nom = "La Pinte Communale", Description = "Restaurant gastronomique avec menu du jour Business", ContactEmail = null, ContactTel = "+41244666270", UrlSite = "https://pinte-communale.ch", RestoPhoto = "https://ik.imagekit.io/guidle/tr:w-250,h-250,c-at_least,dpr-2.625/6/e1/39/6e1394b59ac9f8aa6a5a5600cd65940da2ee6f4e.jpg", Localisation = context.Localisations.FirstOrDefault(e => e.Id == 9) },
                    new Restaurant { Nom = "Nandanam", Description = "Restaurant Indien avec menu du jour + menu du jour végétarien, fermé les lundi.", ContactEmail = "contact@nandanam.ch", ContactTel = "+41213122300", UrlSite = "https://www.nandanam.ch", RestoPhoto = "", Localisation = context.Localisations.FirstOrDefault(e => e.Id == 10) },
                    new Restaurant { Nom = "Giga Tacos Bex", Description = "Des tacos de taille S à XXL chez Giga Tacos à Bex", ContactEmail = null, ContactTel = null, UrlSite = "https://www.gigatacos.ch/", RestoPhoto = "https://www.gigatacos.ch/img/%20Interieur%20Renens%202022%20C.jpg", Localisation = context.Localisations.FirstOrDefault(e => e.Id == 14) },
                };

                context.Restaurants.AddRange(restaurants);
                context.SaveChanges();
            }

            if (!context.MenuTypes.Any())
            {
                var menutypes = new MenuType[]
                {
                    new MenuType { Nom = "Menu du jour"},
                    new MenuType { Nom = "Business Menu"},
                    new MenuType { Nom = "HIT de la semaine"},
                    new MenuType { Nom = "Suggestion du Chef"}
                };
                context.MenuTypes.AddRange(menutypes);
                context.SaveChanges();
            }

            if (!context.Menus.Any())
            {
                var menus = new Menu[]
                {
                    new Menu { Entree = "Soupe au Kimchi", Plat = "Bibimbap riz légumes et boeuf mariné", Dessert = "", Prix = 23, InclusBoisson = false, InclusCafe = false, PlatPhoto="https://assets.tmecosys.com/image/upload/t_web767x639/img/recipe/ras/Assets/EF9E3126-C3F4-4001-9FC0-3E97EB1BC191/Derivates/930242b2-9975-46d2-bf38-0ac37e073e47.jpg", DateMenu = DateTime.Today, DateModif = DateTime.Now, Type = context.MenuTypes.FirstOrDefault(e => e.Id == 1) },
                    new Menu { Entree = "", Plat = "Kamikaze Burger Rösti, Choux marinés, cheddar, Batavia", Dessert = "", Prix = 14.90, InclusBoisson = true, InclusCafe = false, PlatPhoto="https://flaevor.com/wp-content/uploads/2020/09/Potato-RostiBurgerwith-KoreanChilli-MayoRecipe.jpg", DateMenu = DateTime.Today, DateModif = DateTime.Now, Type = context.MenuTypes.FirstOrDefault(e => e.Id == 1) },
                    new Menu { Entree = "Salade", Plat = "Steak de tofu avec Blé ebly carottes", Dessert = "Tarte de framboises", Prix = 19.80, InclusBoisson = false, InclusCafe = true, PlatPhoto="https://img.over-blog-kiwi.com/1/06/55/56/20160726/ob_e67e8b_stea1-1.jpg", DateMenu = DateTime.Today, DateModif = DateTime.Now, Type = context.MenuTypes.FirstOrDefault(e => e.Id == 1), Restaurant = context.Restaurants.FirstOrDefault(e => e.Id == 1), Localisation = context.Localisations.FirstOrDefault(e => e.Id == 1) },
                    new Menu { Entree = "", Plat = "Filet de Cabillaud Menuière", Dessert = "Crème Brulée", Prix = 30, InclusBoisson = false, InclusCafe = true, PlatPhoto="https://www.picard.fr/dw/image/v2/AAHV_PRD/on/demandware.static/-/Sites-catalog-picard/default/dw97f011d6/produits/poissons-crustaces/edition/000000000000074771_E1.jpg", DateMenu = DateTime.Today, DateModif = DateTime.Now, Type = context.MenuTypes.FirstOrDefault(e => e.Id == 1), Restaurant = context.Restaurants.FirstOrDefault(e => e.Id == 1), Localisation = context.Localisations.FirstOrDefault(e => e.Id == 1) },
                    new Menu { Entree = "Rouleaux de Printemps végétariens", Plat = "Poulet frit sauce Satay et riz Basmati", Dessert = "Salade de fruits", Prix = 42.00, InclusBoisson = false, InclusCafe = true, PlatPhoto="https://www.hervecuisine.com/wp-content/uploads/2016/03/riz-coco-poulet-satay-1024x685.jpg", DateMenu = DateTime.Today, DateModif = DateTime.Now, Type = context.MenuTypes.FirstOrDefault(e => e.Id == 1), Restaurant = context.Restaurants.FirstOrDefault(e => e.Id == 3), Localisation = context.Localisations.FirstOrDefault(e => e.Id == 3) },
                    new Menu { Entree = "Falafel", Plat = "Kebab agneau et frites ketchup", Dessert = "", Prix = 9.90, InclusBoisson = true, InclusCafe = false, PlatPhoto="https://miro.medium.com/max/560/1*9j5ejmDjBkRiIscgbX_lnw.jpeg", DateMenu = DateTime.Today, DateModif = DateTime.Now, Type = context.MenuTypes.FirstOrDefault(e => e.Id == 3), Restaurant = context.Restaurants.FirstOrDefault(e => e.Id == 4), Localisation = context.Localisations.FirstOrDefault(e => e.Id == 4) },
                    new Menu { Entree = "", Plat = "Rôti de Veau pommes de terres sautées", Dessert = "", Prix = 17.50, InclusBoisson = false, InclusCafe = false, PlatPhoto="https://assets.afcdn.com/recipe/20180503/79003_w1024h1024c1cx3333cy2496.jpg", DateMenu = DateTime.Today, DateModif = DateTime.Now, Type = context.MenuTypes.FirstOrDefault(e => e.Id == 4), Restaurant = context.Restaurants.FirstOrDefault(e => e.Id == 7), Localisation = context.Localisations.FirstOrDefault(e => e.Id == 7) },
                    new Menu { Entree = "Salade aux haricots", Plat = "Pizza Valaisanne - Patates Raclette Lard", Dessert = "", Prix = 21.50, InclusBoisson = true, InclusCafe = false, PlatPhoto="https://img.cuisineaz.com/610x610/2018/09/19/i142597-pizza-pommes-de-terre-lardons.jpeg", DateMenu = DateTime.Today, DateModif = DateTime.Now, Type = context.MenuTypes.FirstOrDefault(e => e.Id == 4), Restaurant = context.Restaurants.FirstOrDefault(e => e.Id == 2), Localisation = context.Localisations.FirstOrDefault(e => e.Id == 2) },
                    new Menu { Entree = "", Plat = "Risotto aux asperges et parmesan", Dessert = "Dessert du jour", Prix = 16, InclusBoisson = false, InclusCafe = false, PlatPhoto="https://img.cuisineaz.com/660x660/2013/12/20/i75724-recette-de-risotto-aux-asperges.jpeg", DateMenu = DateTime.Today, DateModif = DateTime.Now, Type = context.MenuTypes.FirstOrDefault(e => e.Id == 1), Restaurant = context.Restaurants.FirstOrDefault(e => e.Id == 1), Localisation = context.Localisations.FirstOrDefault(e => e.Id == 1) },
                    new Menu { Entree = "Gaspacho de courgette", Plat = "Lasagnes aux légumes d'hiver", Dessert = "", Prix = 18.60, InclusBoisson = false, InclusCafe = true, PlatPhoto="https://www.maiabaudelaire.com/assets/img/recettes/big/5042.jpg", DateMenu = DateTime.Today, DateModif = DateTime.Now, Type = context.MenuTypes.FirstOrDefault(e => e.Id == 1), Restaurant = context.Restaurants.FirstOrDefault(e => e.Id == 1), Localisation = context.Localisations.FirstOrDefault(e => e.Id == 1) },

                };
                context.Menus.AddRange(menus);
                context.SaveChanges();
            }

            //if (!context.Roles.Any())
            //{
            //    var roles = new Role[]
            //    {
            //        new Role { Type = "utilisateur"},
            //        new Role { Type = "restaurateur"},
            //        new Role { Type = "administrateur"}

            //    };
            //    context.Roles.AddRange(roles);
            //    context.SaveChanges();
            //}

            //Flo fonctionne pas le StringToByte->mauvais mdp
            if (!context.Users.Any())
            {
                var users = new User[]
                {
                    new User { Username = "flow", Email = "flo@nepal.io", PassHash = StringToByteArray("pj120OL3ghMITYdL89ceaps5sP3k4WjEjN8U1RbOBs5yihbKuqH7jCGpoBNhY3XvqIM1rBmcxRFIHAurYmFd5Q=="), PassSalt= StringToByteArray("uL7G6QK/tfPIFxshs0tjRE+XXxjvbjxgoV/5okLvIDBb+wyf3KF3atqOdLjd2XoQpgVmzDDcmvw/fhJiCq0GR7Xk2kLeCqAysFfpUhOEmo9uTK7f0tpwX3oASwKSkZZAHm0f4gP7frw64V8Oq1oXXhG19MtMnGvklbc/lH2BeOg="), MobileNum = null, DateInscription = DateTime.Now, Role = Role.Admin },
                    new User { Username = "spiderman", Email = "peter@parker.com", PassHash = StringToByteArray("zXfwDjYjYBPvGoyPggrNzhDEWYTTmNay7e1okQ3j4QJ09xsLRic3ONjgvbVoCTtZxO1S8mNyhI8qiF5DY0/zjA=="), PassSalt= StringToByteArray("a+LyfFf/CSDqBngiVtk3sTiAs2JhUHSfMRwdrZX2tXZ9v6MmQQ8+wwQFUPii1mjl8lyhoIEI5KbDTgtG1T15DRAeV72QLY5rrP3Wsyk7AHDYzwI2T/TzDiQp0M+t/S+wkcfGtNiOJ92nnwglerLboUC7PEtmPDPt0Ki50vRmdt8="), MobileNum = null, DateInscription = DateTime.Now, Role = Role.Restaurateur },
                    new User { Username = "admin", Email = "admin@golun.ch", PassHash = StringToByteArray("+jKAhuJNIv0grWTflp4UZoa8aXncp2dSivF/TF8CDsE+QeUEQ5QlgAOmWvHqE3l0HQK0bNbK/v+OtQMdo6rE4w=="), PassSalt= StringToByteArray("CgLh12ymsDcIKDckax+wDOOoMKLU2cZn1ckWOJfXLTIOj/K1W41k+mLiadUdONn3cyUVBUipHbr39d9C52+pPjBOrjOVrpEJCMpsGUMTxz1IDjmfd9jM5arl+9kri3d/+VZ175xepl7B7i0pTjoHqmpUd4gNm1ePaDep2Xt87Z8="), MobileNum = "+41763358609", DateInscription = DateTime.Now, Role = Role.User },
                    new User { Username = "user", Email = "user@golun.ch", PassHash = StringToByteArray("twK/d90LdFy3TPOryOE4TRQvSElPLbuA7eQ0u3uC9EVZWF6K0tVVDmeW5BCV4dzpqah6C7hrmZ9yks+hieThMQ=="), PassSalt= StringToByteArray("hLb7HrxiqoJxYadlcu2CzJFHTrxAa2n0Corp0Q36DfxL6cSufOAfrjLo918ZrfQRwcPQMf8BRqe+rI5mtJX/HjvEyaaYGMDuKZPph+/PT/HinmxhS9gDMcnlzjA6IdBsTdaddrjyV+WqX1U5tk4YQke62zE+FSClVsNLkXcYjLI="), MobileNum = null, DateInscription = DateTime.Now, Role = Role.User },
                };
                context.Users.AddRange(users);
                context.SaveChanges();
            }

            //Flo ajout de favoris
            if (!context.Favoris.Any())
            {
                var favoris = new Fav[]
                {
                    new Fav { Restaurant = null, RestaurantId = 1, User = null, UserId = 1 },
                    new Fav { Restaurant = null, RestaurantId = 2, User = null, UserId = 1 },
                    new Fav { Restaurant = null, RestaurantId = 6, User = null, UserId = 1 },
                    new Fav { Restaurant = null, RestaurantId = 10, User = null, UserId = 1 },
                    new Fav { Restaurant = null, RestaurantId = 3, User = null, UserId = 2 },
                    new Fav { Restaurant = null, RestaurantId = 4, User = null, UserId = 2 },
                    new Fav { Restaurant = null, RestaurantId = 2, User = null, UserId = 2 },
                    new Fav { Restaurant = null, RestaurantId = 2, User = null, UserId = 4 },
                };
                context.Favoris.AddRange(favoris);
                context.SaveChanges();
            }
        }

        internal static void Initialize(GolunchDbContext golunchDbContext, object context)
        {
            throw new NotImplementedException();
        }
        //Conversion string en Bye Array
        private static byte[] StringToByteArray(string str)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetBytes(str);
        }
    }
}
