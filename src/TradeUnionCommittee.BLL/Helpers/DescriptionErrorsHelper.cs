using System.Collections.Generic;
using System.Linq;
using TradeUnionCommittee.BLL.Enums;

namespace TradeUnionCommittee.BLL.Helpers
{
    internal sealed class DescriptionErrorsHelper
    {
        internal static string DescriptionError(Errors error)
        {
            switch (error)
            {
                case Errors.ConnectionLost:
                    return "Втрачено з'єднання з базою даних!";

                case Errors.ApplicationError:
                    return "Oops! Трапилась невідома помилка!";

                case Errors.DataBaseError:
                    return "Сталась помилка в базі даних!";

                case Errors.UserNotFound:
                    return "Користувача не знайдено, можливо він був видалений!";

                case Errors.DuplicateEmail:
                    return "Цей Email вже використовується!";

                case Errors.DuplicateData:
                    return "Такий запис вже існує!";

                case Errors.TupleDeletedOrUpdated:
                    return "Запис змінено або видалено іншим користувачем!";

                case Errors.TupleDeleted:
                    return "Запис був видалений!";

                case Errors.EmployeeDeleted:
                    return "Співробітник був видалений!";

                case Errors.DataUsed:
                    return "Дані використовуються!";

                case Errors.NotFound:
                    return "За вашим запитом нічого не знайдено!";

                case Errors.FileNotFound:
                    return "Файл не знайдено!";

                case Errors.IncorrectHashId:
                    return "Недійсний ідентифікатор";

                default:
                    return "Oops! Трапилась невідома помилка!";
            }
        }

        internal static List<string> DescriptionErrors(IEnumerable<Errors> errors)
        {
            return errors.Select(DescriptionError).ToList();
        }
    }
}
