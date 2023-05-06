using KonfidesCase.Authentication.Extensions;
using KonfidesCase.BLL.Extensions;
using KonfidesCase.DAL.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Custom Services
builder.Services.AddKonfidesAuthServices(builder.Configuration);
builder.Services.AddKonfidesDalServices(builder.Configuration);
builder.Services.AddKonfidesBllServices();
#endregion

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

#region Middlewares for Custom Services
app.UseCors("myCors");
app.UseAuthentication();
#endregion

app.UseAuthorization();

app.MapControllers();

app.Run();
