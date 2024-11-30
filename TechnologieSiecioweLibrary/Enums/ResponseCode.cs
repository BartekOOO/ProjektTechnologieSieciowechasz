using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnologieSiecioweLibrary.Enums
{
    public enum ResponseCode
    {
        [Description("Sukces - żądanie zakończyło się powodzeniem.")]
        OK = 200,

        [Description("Utworzono - żądanie zakończone sukcesem, zasób został utworzony.")]
        CREATED = 201,

        [Description("Zaakceptowano - żądanie zostało przyjęte, ale nie jest jeszcze przetworzone.")]
        ACCEPTED = 202,

        [Description("Błędne żądanie - serwer nie może zrozumieć żądania z powodu błędów składni.")]
        BAD_REQUEST = 400,

        [Description("Brak autoryzacji - brak wymaganych uprawnień.")]
        UNAUTHORIZED = 402,

        [Description("Błąd rejestracji - użytkownik z tą nazwą już istnieje")]
        USER_ALREADY_EXISTS = 600,

        [Description("Zabronione - brak dostępu do zasobu.")]
        FORBIDDEN = 403,

        [Description("Nie znaleziono - serwer nie może znaleźć żądanego zasobu.")]
        NOT_FOUND = 404,

        [Description("Wewnętrzny błąd serwera - serwer napotkał sytuację, której nie potrafi obsłużyć.")]
        INTERNAL_SERVER_ERROR = 500,

        [Description("Nie zaimplementowano - metoda żądania nie jest obsługiwana przez serwer.")]
        NOT_IMPLEMENTED = 501,

        [Description("Błąd bramy - serwer pośredniczący otrzymał nieprawidłową odpowiedź od serwera docelowego.")]
        BAD_GATEWAY = 502,

        [Description("Usługa niedostępna - serwer jest tymczasowo przeciążony lub wyłączony.")]
        SERVICE_UNAVAILABLE = 503
    }
}
