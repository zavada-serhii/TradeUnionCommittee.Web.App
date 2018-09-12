using System;
using System.Collections.Generic;
using System.Linq;

namespace TradeUnionCommittee.Common.ActualResults
{
    public enum Errors
    {
        TupleDeleted = 1,
        TupleUpdated = 2,
        InvalidId = 3,
        DuplicateData = 4,
        InvalidLoginOrPassword = 5
    }

    public class ActualResult
    {
        public List<string> ErrorsList { get; set; }
        public bool IsValid { get; set; }

        public ActualResult()
        {
            IsValid = true;
            ErrorsList = new List<string>();
        }

        public ActualResult(string error)
        {
            IsValid = false;
            ErrorsList = new List<string> { error };
        }

        public ActualResult(List<string> errors)
        {
            IsValid = false;
            ErrorsList = errors;
        }

        public ActualResult(Errors error)
        {
            IsValid = false;
            ErrorsList = new List<string> { DescriptionError(error) };
        }

        public ActualResult(IEnumerable<Errors> errors)
        {
            IsValid = false;
            ErrorsList = DescriptionErrors(errors);
        }

        //----------------------------------------------------------------------------

        private string DescriptionError(Errors error)
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

                default:
                    throw new ArgumentOutOfRangeException(nameof(error), error, "Ви зламали систему. Вітаю:)");
            }
        }

        private List<string> DescriptionErrors(IEnumerable<Errors> errors)
        {
            return errors.Select(DescriptionError).ToList();
        }

        //----------------------------------------------------------------------------
    }

    public class ActualResult<T> : ActualResult
    {
        public T Result { get; set; }

        public ActualResult()
        {
        }

        public ActualResult(string error) : base(error)
        {

        }

        public ActualResult(List<string> errors) : base(errors)
        {

        }

        public ActualResult(Errors error) : base(error)
        {

        }

        public ActualResult(IEnumerable<Errors> errors) : base(errors)
        {

        }
    }
}