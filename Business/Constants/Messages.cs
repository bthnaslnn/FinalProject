using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime ="Sistem bakımda";
        public static string ProductsListed = "Ürünlerl listelendi";
        public static string ProductCountOfCategoryError="Bir kategoride en fazla 10 ürün olabiliir";
        public static string ProductNameAlreadyExists="Bu isimde başka bir ürün zaten var.";
        internal static string CategoryLimitExceded="Kategori limiti aşıldı.";
    }
}
