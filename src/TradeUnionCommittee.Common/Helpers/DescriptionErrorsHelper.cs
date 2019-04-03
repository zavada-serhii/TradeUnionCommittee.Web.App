using System;
using System.Collections.Generic;
using System.Linq;
using TradeUnionCommittee.Common.Enums;

namespace TradeUnionCommittee.Common.Helpers
{
    internal sealed class DescriptionErrorsHelper
    {
        internal static string DescriptionError(Errors error)
        {
            switch (error)
            {
                case Errors.TupleDeleted:
                    return "Запис вже видалено іншим користувачем!";

                case Errors.TupleDeletedOrUpdated:
                    return "Запис змінено або видалено іншим користувачем!";

                case Errors.DuplicateData:
                    return "Такий запис вже існує!";

                case Errors.InvalidLoginOrPassword:
                    return "Не правильний логін або пароль!";

                case Errors.DataBaseError:
                    return "Сталась помилка в базі даних!";

                case Errors.NotFound:
                    return "За вашим запитом нічого не знайдено!";

                case Errors.FileNotFound:
                    return "Файл не знайдено!";



                case Errors.UserNotFound:
                    return "Користувача не знайдено, можливо він був видалений!";

                case Errors.DuplicateEmail:
                    return "Цей Email уже використовується!";

                default:
                    throw new ArgumentOutOfRangeException(nameof(error), error, "Ви зламали систему. Вітаю:)");
            }
        }

        internal static List<string> DescriptionErrors(IEnumerable<Errors> errors)
        {
            return errors.Select(DescriptionError).ToList();
        }
    }
}
