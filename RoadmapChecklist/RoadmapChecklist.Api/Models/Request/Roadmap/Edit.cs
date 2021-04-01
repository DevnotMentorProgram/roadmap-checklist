using FluentValidation;
using System;

namespace RoadmapChecklist.Api.Models.Request.Roadmap
{
    public class Edit
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Visibility { get; set; } = 1;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class EditValidator : AbstractValidator<Edit>
    {
        public EditValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id field is required !");
            RuleFor(x => x.Name).NotNull().WithMessage("Name field is required !");
            RuleFor(x => x.Visibility).GreaterThanOrEqualTo(1);
            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate.Value)
                .WithMessage("End date must not exceed start date!")
                .When(x => x.StartDate.HasValue);
        }
    }
}
