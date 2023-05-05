using KonfidesCase.Authentication.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddKonfidesAuthServices(builder.Configuration);
//builder.Services.AddKonfidesDalServices(builder.Configuration);
//builder.Services.AddKonfidesBllServices();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("myCors");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
