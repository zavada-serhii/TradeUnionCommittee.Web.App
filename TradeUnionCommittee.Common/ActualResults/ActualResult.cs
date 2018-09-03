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
            ErrorsList = DescriptionErrors(errors).ToList();
        }

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

                default:
                    throw new ArgumentOutOfRangeException(nameof(error), error, null);
            }
        }

        private IEnumerable<string> DescriptionErrors(IEnumerable<Errors> errors)
        {
            var result = new List<string>();

            foreach (var error in errors)
            {
                switch (error)
                {
                    case Errors.TupleDeleted:
                        result.Add("Запис вже видалено іншим користувачем!");
                        break;

                    case Errors.TupleUpdated:
                        result.Add("Запис вже був оновлений іншим користувачем!");
                        break;

                    case Errors.InvalidId:
                        result.Add("Недійсний ідентифікатор!");
                        break;

                    case Errors.DuplicateData:
                        result.Add("Такий запис вже існує!");
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(error), error, null);
                }
            }
            return result;
        }
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
    }
}