var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidatorsFromAssemblyContaining<BrandValidator>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

var brands = new List<Brand>();
var carModels = new List<CarModel>();

brands.Add(new Brand() { BrandId = 1, Name = "Audi", Country = "Germany", Logo = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6f/Audi_Logo.svg/1200px-Audi_Logo.svg.png" });
brands.Add(new Brand() { BrandId = 2, Name = "BMW", Country = "Germany", Logo = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/02/BMW_logo_%282017%29.svg/1200px-BMW_logo_%282017%29.svg.png" });
brands.Add(new Brand() { BrandId = 3, Name = "Mercedes", Country = "Italy", Logo = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4a/Mercedes-Benz_logo_%282018%29.svg/1200px-Mercedes-Benz_logo_%282018%29.svg.png" });

carModels.Add(new CarModel() { CarModelId = 1, Name = "X1", Brand = brands[1] });
carModels.Add(new CarModel() { CarModelId = 2, Name = "X2", Brand = brands[1] });
carModels.Add(new CarModel() { CarModelId = 3, Name = "X3", Brand = brands[1] });
carModels.Add(new CarModel() { CarModelId = 4, Name = "A6", Brand = brands[0] });

//retrieve all brands
app.MapGet("/brands", () =>
{
    return Results.Ok(brands);
});

//retrieve all brands from a country
app.MapGet("/brands/{country}", (string Country) =>
{
    var filteredBrands = brands.Where(brand => brand.Country == Country);
    return Results.Ok(filteredBrands);
});

//retrieve a specific brand
app.MapGet("/brand/{BrandId}", (int BrandId) =>
{
    var brand = brands.FirstOrDefault(brand => brand.BrandId == BrandId);
    if (brand == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(brand);
});

//add a brand
app.MapPost("/brand", (Brand brand) =>
{
    brand.BrandId = brands.Max(b => b.BrandId) + 1;
    brands.Add(brand);
    return Results.Created($"/brand/{brand.BrandId}", brand);
});


//retrieve all car models with their brand
app.MapGet("/carModels", () =>
{
    return Results.Ok(carModels);
});

//retrieve all car models from a brand
app.MapGet("/carModels/{brand}", (string brand) =>
{
    var filteredCarModels = carModels.Where(carModel => carModel.Brand.Name == brand);
    return Results.Ok(filteredCarModels);
});

//retrieve a specific car model
app.MapGet("/carModels/{CarModelId}", (int CarModelId) =>
{
    var carModel = carModels.FirstOrDefault(carModel => carModel.CarModelId == CarModelId);
    if (carModel == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(carModel);
});

//retrieve all car models from a country
app.MapGet("/carModels/{country}", (string Country) =>
{
    var filteredCarModels = carModels.Where(carModel => carModel.Brand.Country == Country);
    return Results.Ok(filteredCarModels);
});



app.Run("http://localhost:5000");