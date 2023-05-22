var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Custom Services
builder.Services.AddKonfidesAuthServices(builder.Configuration);
builder.Services.AddKonfidesDalServices(builder.Configuration);
builder.Services.AddKonfidesBllServices();
#endregion

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddCors(options => options.AddPolicy("myCors", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddScoped<SignInManager<AuthUser>>();
builder.Services.AddControllers().AddControllersAsServices().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

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

app.UseSession();

app.Run();
