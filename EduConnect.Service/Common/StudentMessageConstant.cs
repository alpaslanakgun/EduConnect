using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.Services.Common
{
    public class StudentMessageConstant
    {
        public const string NotFound = "Öğrenci bulunamadı.";
        public const string DeletionFailed = "Öğrenci silme işlemi başarısız oldu.";
        public const string UpdateFailed = "Öğrenci güncelleme işlemi başarısız oldu.";
        public const string SearchFailed = "Öğrenci arama işlemi başarısız oldu.";
        public const string AlreadyDeleted = "Öğrenci zaten silinmiş veya bulunamadı.";
        public const string NoStudentsFound = "Arama kriterlerine uygun öğrenci bulunamadı.";
        public const string DeletionSuccessful = "Öğrenci silme işlemi başarıyla gerçekleştirildi.";
        public const string UpdateSuccessful = "Öğrenci güncelleme işlemi başarıyla gerçekleştirildi.";
        public const string AddSuccessful = "Öğrenci ekleme işlemi başarıyla gerçekleştirildi.";
        public const string HardDeleteSuccessful = "Öğrenci kalıcı olarak silindi.";
    }
}
