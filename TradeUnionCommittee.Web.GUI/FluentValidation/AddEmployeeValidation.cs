using FluentValidation;
using System;
using TradeUnionCommittee.Web.GUI.Models;

namespace TradeUnionCommittee.Web.GUI.FluentValidation
{
    public class AddEmployeeValidation : AbstractValidator<AddEmployeeViewModel>
    {
        public AddEmployeeValidation()
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

            //------------------------------------------------------------------------------------------------------------------------------------------

            RuleFor(x => x.LevelEducation).NotNull().WithMessage("Рiвень освiти не може бути порожнім");
            RuleFor(x => x.NameInstitution).NotNull().WithMessage("Місце навчання не може бути порожнім");
            RuleFor(x => x.YearReceiving).InclusiveBetween(1900, 2200).WithMessage("Неприпустимий Рік отримання");

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

            RuleFor(x => x.ScientifickDegree).NotNull().When(x => x.Scientifick).WithMessage("Учений ступiнь не може бути порожнім");
            RuleFor(x => x.ScientifickTitle).NotNull().When(x => x.Scientifick).WithMessage("Наукове звання не може бути порожнім");

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
}