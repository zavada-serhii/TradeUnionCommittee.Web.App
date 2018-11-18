using System;
using FluentValidation;
using TradeUnionCommittee.Mvc.Web.GUI.Models;

namespace TradeUnionCommittee.Mvc.Web.GUI.FluentValidation
{
    public class CreateEmployeeValidation : AbstractValidator<CreateEmployeeViewModel>
    {
        public CreateEmployeeValidation()
        {
            RuleFor(x => x.FirstName).NotNull().WithMessage("Прізвище не може бути порожнім");
            RuleFor(x => x.SecondName).NotNull().WithMessage("Ім'я не може бути порожнім");
            RuleFor(x => x.Sex).NotNull().WithMessage("Вкажіть стать");

            //------------------------------------------------------------------------------------------------------------------------------------------

            RuleFor(x => x.BasicProfession).NotNull().WithMessage("Професія за дипломом не може бути порожнім");
            RuleFor(x => x.BirthDate).NotNull().WithMessage("Дата народження не може бути порожнім");
            RuleFor(x => x.StartYearWork).NotNull().WithMessage("Рік початку роботи в ОНУ не може бути порожнім");
            RuleFor(x => x.StartYearWork).NotNull().InclusiveBetween(1900, 2200).WithMessage("Неприпустимий Рік початку роботи в ОНУ");
            RuleFor(x => x.StartDateTradeUnion).NotNull().WithMessage("Дата вступу в ППО не може бути порожнім");
            RuleFor(x => x.CityPhone).MinimumLength(6).WithMessage("Довжина номеру повинна бути від 6 символів");
            RuleFor(x => x.CityPhone).MaximumLength(7).WithMessage("Довжина номеру максимум 7 символів");

            //------------------------------------------------------------------------------------------------------------------------------------------

            RuleFor(x => x.LevelEducation).NotNull().WithMessage("Рiвень освiти не може бути порожнім");
            RuleFor(x => x.NameInstitution).NotNull().WithMessage("Місце навчання не може бути порожнім");
            RuleFor(x => x.YearReceiving).InclusiveBetween(1900, 2200).WithMessage("Неприпустимий Рік отримання");

            //------------------------------------------------------------------------------------------------------------------------------------------

            RuleFor(x => x.ScientificDegree)
                .NotNull()
                .When(x => !string.IsNullOrEmpty(x.ScientificTitle) && !string.IsNullOrWhiteSpace(x.ScientificTitle))
                .WithMessage("Учений ступiнь не може бути порожнім");

            RuleFor(x => x.ScientificTitle)
                .NotNull()
                .When(x => !string.IsNullOrEmpty(x.ScientificDegree) && !string.IsNullOrWhiteSpace(x.ScientificDegree))
                .WithMessage("Наукове звання не може бути порожнім");

            //------------------------------------------------------------------------------------------------------------------------------------------

            RuleFor(x => x.HashIdPosition).NotNull().WithMessage("Посада не може бути порожнім");
            RuleFor(x => x.HashIdMainSubdivision).NotNull().WithMessage("Структурний пiдроздiл не може бути порожнім");

            //------------------------------------------------------------------------------------------------------------------------------------------

            RuleFor(x => x.TypeAccommodation).NotNull().WithMessage("Вкажіть адресу проживання!");

            RuleFor(x => x.CityPrivateHouse).NotNull().When(x => x.TypeAccommodation == "privateHouse").WithMessage("Місто не може бути порожнім");
            RuleFor(x => x.StreetPrivateHouse).NotNull().When(x => x.TypeAccommodation == "privateHouse").WithMessage("Вулиця не може бути порожнім");

            RuleFor(x => x.CityHouseUniversity).NotNull().When(x => x.TypeAccommodation == "fromUniversity").WithMessage("Місто не може бути порожнім");
            RuleFor(x => x.StreetHouseUniversity).NotNull().When(x => x.TypeAccommodation == "fromUniversity").WithMessage("Вулиця не може бути порожнім");
            RuleFor(x => x.DateReceivingHouseFromUniversity).NotNull().When(x => x.TypeAccommodation == "fromUniversity").WithMessage("Дата розподiлу не може бути порожнім");

            RuleFor(x => x.HashIdDormitory).NotNull().When(x => x.TypeAccommodation == "dormitory").WithMessage("Номер гуртожитку не може бути порожнім");
            RuleFor(x => x.NumberRoomDormitory).NotNull().When(x => x.TypeAccommodation == "dormitory").WithMessage("Номер кiмнати не може бути порожнім");

            RuleFor(x => x.HashIdDepartmental).NotNull().When(x => x.TypeAccommodation == "departmental").WithMessage("Адреса вiдомчого не може бути порожнім");
            RuleFor(x => x.NumberRoomDepartmental).NotNull().When(x => x.TypeAccommodation == "departmental").WithMessage("Номер кiмнати не може бути порожнім");

            //------------------------------------------------------------------------------------------------------------------------------------------

            RuleFor(x => x.HashIdSocialActivity).NotNull().When(x => x.SocialActivity).WithMessage("Назва посади не може бути порожнім");

            //------------------------------------------------------------------------------------------------------------------------------------------

            RuleFor(x => x.HashIdPrivileges).NotNull().When(x => x.Privileges).WithMessage("Назва пільги не може бути порожнім");

            //------------------------------------------------------------------------------------------------------------------------------------------

            RuleFor(x => x.BirthDate)
                .Null()
                .When(x => x.BirthDate < Convert.ToDateTime("01.01.1900").Date)
                .WithMessage("Дата народження менша 01.01.1900 року");

            RuleFor(x => x.BirthDate)
                .Null()
                .When(x => x.BirthDate != null && x.BirthDate.Value.Year >= x.StartYearWork)
                .WithMessage("Рік народження більший року початку роботи в ОНУ");

            RuleFor(x => x.StartYearWork)
                .Null()
                .When(x => x.StartDateTradeUnion != null && x.StartYearWork > x.StartDateTradeUnion.Value.Year)
                .WithMessage("Рік початку роботи в ОНУ більший року вступу в профспілку");

            RuleFor(x => x.YearReceiving)
                .Null()
                .When(x => x.BirthDate != null && (x.YearReceiving != null && x.BirthDate.Value.Year >= x.YearReceiving))
                .WithMessage("Рік народження більший року отримання освіти");

            RuleFor(x => x.DateReceivingHouseFromUniversity)
                .Null()
                .When(x => x.TypeAccommodation == "fromUniversity" && x.StartDateTradeUnion > x.DateReceivingHouseFromUniversity)
                .WithMessage("Дата вступу в профспілку більша дати розподілу житла від університету!");

            //------------------------------------------------------------------------------------------------------------------------------------------
        }
    }

    public class UpdateEmployeeValidation : AbstractValidator<UpdateEmployeeViewModel>
    {
        public UpdateEmployeeValidation()
        {
            RuleFor(x => x.FirstName).NotNull().WithMessage("Прізвище не може бути порожнім");
            RuleFor(x => x.SecondName).NotNull().WithMessage("Ім'я не може бути порожнім");
            RuleFor(x => x.Sex).NotNull().WithMessage("Вкажіть стать");

            //------------------------------------------------------------------------------------------------------------------------------------------

            RuleFor(x => x.BasicProfession).NotNull().WithMessage("Професія за дипломом не може бути порожнім");
            RuleFor(x => x.BirthDate).NotNull().WithMessage("Дата народження не може бути порожнім");
            RuleFor(x => x.StartYearWork).NotNull().WithMessage("Рік початку роботи в ОНУ не може бути порожнім");
            RuleFor(x => x.StartYearWork).NotNull().InclusiveBetween(1900, 2200).WithMessage("Неприпустимий Рік початку роботи в ОНУ");
            RuleFor(x => x.StartDateTradeUnion).NotNull().WithMessage("Дата вступу в ППО не може бути порожнім");
            RuleFor(x => x.CityPhone).MinimumLength(6).WithMessage("Довжина номеру повинна бути від 6 символів");
            RuleFor(x => x.CityPhone).MaximumLength(7).WithMessage("Довжина номеру максимум 7 символів");

            //------------------------------------------------------------------------------------------------------------------------------------------

            RuleFor(x => x.LevelEducation).NotNull().WithMessage("Рiвень освiти не може бути порожнім");
            RuleFor(x => x.NameInstitution).NotNull().WithMessage("Місце навчання не може бути порожнім");
            RuleFor(x => x.YearReceiving).InclusiveBetween(1900, 2200).WithMessage("Неприпустимий Рік отримання");

            //------------------------------------------------------------------------------------------------------------------------------------------

            RuleFor(x => x.ScientificDegree)
                .NotNull()
                .When(x => !string.IsNullOrEmpty(x.ScientificTitle) && !string.IsNullOrWhiteSpace(x.ScientificTitle))
                .WithMessage("Учений ступiнь не може бути порожнім");

            RuleFor(x => x.ScientificTitle)
                .NotNull()
                .When(x => !string.IsNullOrEmpty(x.ScientificDegree) && !string.IsNullOrWhiteSpace(x.ScientificDegree))
                .WithMessage("Наукове звання не може бути порожнім");

            //------------------------------------------------------------------------------------------------------------------------------------------

            RuleFor(x => x.BirthDate)
                .Null()
                .When(x => x.BirthDate < Convert.ToDateTime("01.01.1900").Date)
                .WithMessage("Дата народження менша 01.01.1900 року");

            RuleFor(x => x.BirthDate)
                .Null()
                .When(x => x.BirthDate.Year >= x.StartYearWork)
                .WithMessage("Рік народження більший року початку роботи в ОНУ");

            RuleFor(x => x.StartYearWork)
                .Null()
                .When(x => x.StartYearWork > x.StartDateTradeUnion.Year)
                .WithMessage("Рік початку роботи в ОНУ більший року вступу в профспілку");

            RuleFor(x => x.YearReceiving)
                .Null()
                .When(x => (x.YearReceiving != null && x.BirthDate.Year >= x.YearReceiving))
                .WithMessage("Рік народження більший року отримання освіти");

            //-------------------------

            RuleFor(x => x.StartYearWork)
                .NotNull()
                .When(x => x.StartYearWork > x.EndYearWork)
                .WithMessage("Рік початку роботи в ОНУ не може бути більшим року завершення роботи в ОНУ");

            RuleFor(x => x.EndYearWork)
                .Null()
                .When(x => x.EndYearWork < x.StartYearWork)
                .WithMessage("Рік завершення роботи в ОНУ не може бути меншим року початку роботи в ОНУ");

            //-------------------------

            RuleFor(x => x.StartDateTradeUnion)
                .NotNull()
                .When(x => x.StartDateTradeUnion > x.EndDateTradeUnion)
                .WithMessage("Дата вступу в ППО не може бути більшою дати виходу з ППО");

            RuleFor(x => x.EndDateTradeUnion)
                .Null()
                .When(x => x.EndDateTradeUnion < x.StartDateTradeUnion)
                .WithMessage("Дата виходу з ППО не може бути меншою дати вступу в ППО");

            //------------------------------------------------------------------------------------------------------------------------------------------
        }
    }
}
