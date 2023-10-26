using BlogApi.Application.Services.Domain.Contracts;
using BlogApi.Application.Services.Validation;
using BlogApi.Application.Services.Validation.Contracts;
using BlogApi.Application.Utilities;
using BlogApi.Persistence.Context;
using BlogApi.Persistence.UnitOfWork;
using BlogApi.Persistence.UnitOfWork.Contracts;
using Microsoft.EntityFrameworkCore;
using SmallBlog.Application.Services.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDatabase")));

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IBlogService, BlogService>();
builder.Services.AddTransient<IBlogValidationService, BlogValidationService>();
builder.Services.AddAutoMapper(typeof(Mappings));

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
