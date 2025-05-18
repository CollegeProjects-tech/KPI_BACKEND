using KPI_BACKEND.Contest;
using KPI_BACKEND.Interfaces;
using KPI_BACKEND.Repositries;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILogin, LoginRepository>();
builder.Services.AddScoped<IResearch, ResearchRepository>();
builder.Services.AddScoped<IInduInstiInteraction, InduInstiInteractionRepository>();
builder.Services.AddScoped<IFdpSttpWorkshop, FdpSttpWorkshopRepository>();
builder.Services.AddScoped<IStudentDevelopment, StudentDevelopmentRepository>();
builder.Services.AddScoped<IAdmmission, AdmmissionRepository>();
builder.Services.AddScoped<IAcaGustlecArranged, AcaGustlecArrangedRepository>();
builder.Services.AddScoped<IInternalRevnuGen, InternalRevnuGenRepository>();
builder.Services.AddScoped<IAdministrativeResp, AdministrativeRespRepository>();
builder.Services.AddScoped<IExamSecResp, ExamSecRespRepository>();
builder.Services.AddScoped<IPrinciple, PrincipleRepository>();
builder.Services.AddScoped<IAdmin, AdminRepository>();




builder.Services.AddSingleton<DapperContext>();
builder.Services.AddDbContext<KpiMvcDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("myTestDB")));
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.MapControllers();

app.Run();
