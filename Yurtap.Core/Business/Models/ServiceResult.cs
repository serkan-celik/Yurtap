
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yurtap.Core.Business.Models
{
    public class ServiceResult<T>
    {
        public T Result { get; set; }
        public string Message { get; set; } = "Sonuçlar listelendi";
        public string ResultType { get; set; }
        public ServiceResultType ResultCode  { get; set; }
        //public ServiceError Error { get; set; }

        private ServiceResult() {

        }

        //Başarılı sonuç
        public ServiceResult(T result)
        {
            Result = result;
            ResultType = Enum.GetName(typeof(ServiceResultType), ServiceResultType.Success);
            ResultCode = ServiceResultType.Success;
        }

        //Kontrollü Hata Durumu
        public ServiceResult(string message)
        {
            Message = message;
            ResultType = Enum.GetName(typeof(ServiceResultType), ServiceResultType.Error);
            ResultCode = ServiceResultType.Error;
        }

        //Kontrollü Hata Durumu
        //public ServiceResult(ServiceError error)
        //{
        //    Error = error;
        //    ResultType = ServiceResultType.Error;
        //}

        //Kontrollü Hata Durumu
        //public ServiceResult(ServiceError error, string message)
        //{
        //    Error = error;
        //    Message = message;
        //    ResultType = ServiceResultType.Error;
        //}

        //Kontrollü Hata Durumu
        public ServiceResult(T result, string info, ServiceResultType resultType)
        {
            Result = result;
            Message = info;
            ResultType = Enum.GetName(typeof(ServiceResultType), resultType);
            ResultCode = resultType;
        }

        public bool Success
        {
            get
            {
                switch (ResultCode)
                {
                    case ServiceResultType.Success:
                        return true;
                    case ServiceResultType.Created:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public string Info
        {
            get
            {
                if (ResultCode == ServiceResultType.Success)
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
        Success = 200,
        //İşlem kontrollü bir nedenle yapılmadı - message ya da info döner
        Failure = 2,
        NotFound = 404,
        Unauthorized = 401,
        Created=201,
        BadRequest=400
    }
}
