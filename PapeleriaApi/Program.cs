using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PapeleriaApi.Modelos;
using PapeleriaApi.Modelos.Datos;
using PapeleriaApi.Modelos.Repositorios;
using PapeleriaApi.Servicios;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DBContexto>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    })
    .AddIdentityCore<Usuario>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DBContexto>();

builder.Services
	.AddHttpContextAccessor() // nos permite acceder al httpContex de la solicitud
	.AddAuthorization() // autoriza solicitudes como roles
	.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) //agrega esquema de autentificacion de bearer
	.AddJwtBearer(options => //configura la autentificacion por tokens, que debe vaalidar 
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
		};
	});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IRolRepositorio, RolRepositorio>();
builder.Services.AddTransient<ILoginRepositorio, LoginRepositorio>();
builder.Services.AddTransient<IProductoRepositorio, ProductoRepositorio>();
builder.Services.AddTransient<IInicioSesionRepositorio, InicioSesionRepositorio>();
builder.Services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddTransient<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddTransient<ICategoriaRepositorio, CategoriaRepositorio>();
builder.Services.AddTransient<IMarcaRepositorio, MarcaRepositorio>();
builder.Services.AddTransient<IServicioJwt, ServicioJwt>();
builder.Services.AddTransient<IOrdenDeCompraRepositorio, OrdenDeCompraRepositorio>();



builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
