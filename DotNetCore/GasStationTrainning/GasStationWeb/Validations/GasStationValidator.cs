using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStationWeb.Validations
{
    public class GasStationValidator: AbstractValidator<GasStationWeb.ModelsViews.GasStaionMV>
    {
        public GasStationValidator()
        {
            RuleFor(x => x.GasStationName).NotEmpty().WithMessage(String.Format(Resources.Resource.E0001, "ガソリンスタンド名")).MaximumLength(100).WithMessage(String.Format(Resources.Resource.E0004, "ガソリンスタンド名", "100"));
            RuleFor(x => x.Longitude).NotEmpty().WithMessage(String.Format(Resources.Resource.E0001, "Longitude"));
            RuleFor(x => x.Latitude).NotEmpty().WithMessage(String.Format(Resources.Resource.E0001, "Latitude"));
            RuleFor(x => x.Address).MaximumLength(200).WithMessage(String.Format(Resources.Resource.E0004, "住所", "200"));
            RuleFor(x => x.OpeningTime).MaximumLength(50).WithMessage(String.Format(Resources.Resource.E0004, "開館時間", "50"));
        }
    }
}
