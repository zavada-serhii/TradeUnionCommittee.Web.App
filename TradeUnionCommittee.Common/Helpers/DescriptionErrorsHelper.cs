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

                case Errors.TupleUpdated:
                    return "Запис вже був оновлений іншим користувачем!";

                case Errors.InvalidId:
                    return "Недійсний ідентифікатор!";

                case Errors.DuplicateData:
                    return "Такий запис вже існує!";

                case Errors.InvalidLoginOrPassword:
                    return "Не правильний логін або пароль!";

                case Errors.DataBaseError:
                    return "Сталась помилка в базі даних!";

                case Errors.NotFound:
                    return "За вашим запитом нічого не знайдено!";
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
