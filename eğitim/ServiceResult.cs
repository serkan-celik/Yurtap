using System;
using Tabim.Core.Common.Enum;

namespace Tabim.Core.Structural
{
    public class ServiceResult<T> 
    {
        public T Result { get; set; }
        public string Message { get; set; }
        public ServiceResultType ResultType { get; set; }
        public ServiceError Error { get; set; }

        private ServiceResult() { }

        //Başarılı sonuç
        public ServiceResult(T result)
        {
            Result = result;
            ResultType = ServiceResultType.Success;
        }

        //Kontrollü Hata Durumu
        public ServiceResult(string message)
        {
            Message = message;
            ResultType = ServiceResultType.Error;
        }

        //Kontrollü Hata Durumu
        public ServiceResult(ServiceError error)
        {
            Error = error;
            ResultType = ServiceResultType.Error;
        }

        //Kontrollü Hata Durumu
        public ServiceResult(ServiceError error, string message)
        {
            Error = error;
            Message = message;
            ResultType = ServiceResultType.Error;
        }

        //Kontrollü Hata Durumu
        public ServiceResult(T result, string info, ServiceResultType resultType)
        {
            Result = result;
            Message = info;
            ResultType = resultType;
        }



        public bool Success
        {
            get
            {
                return ResultType == ServiceResultType.Success?true:false;
            }
        }

        public string Info
        {
            get
            {
                if (ResultType == ServiceResultType.Success)
                    return Message;
                else return "";
            }
        }


    }

    public enum ServiceResultType
    {
        //işlem sırasında beklenmeyen hata alındı - yakalanan hatada mesaj döner, mesaj dönmüyorsa ilgili bölüm kontrol edilmelidir. 
        Error = 0,
        //İşlem başarılı tamamlandı - info dönebilir
        Success = 1,
        //İşlem kontrollü bir nedenle yapılmadı - message ya da info döner
        Failure = 2
    }
}
