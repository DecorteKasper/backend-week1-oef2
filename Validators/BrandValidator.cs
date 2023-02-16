namespace backend_week1_oef2.Validators;

public class BrandValidator : AbstractValidator<Brand>
{
    public BrandValidator(){
         RuleFor(Brand => Brand.Name).NotEmpty().WithMessage("Name is required");
    }
}