
using Domain.IRepository;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserFollowerRepository, UserFollowerRepository>();
builder.Services.AddTransient<IPostRepository, PostRepository>();
builder.Services.AddTransient<ICoinRepository, CoinRepository>();
builder.Services.AddTransient<ICoinHistoryRepository, CoinHistoryRepository>();
builder.Services.AddTransient<ILikeRepository, LikeRepository>();
builder.Services.AddTransient<ICommentRepository, CommentRepository>();
builder.Services.AddTransient<IReplyRepository, ReplyRepository>();
builder.Services.AddTransient<IImageRepository, ImageRepository>();


builder.Services.AddCors();


builder.Services.AddControllers();

builder.Services.AddDbContext<CryptoHubDBContext>(
    options =>
    {
        //options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"));
        options.UseSqlServer(builder.Configuration.GetConnectionString("SmarterASPNET"));
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(
    options =>
    {
        options.
        AllowAnyOrigin().
        AllowAnyMethod().
        AllowAnyHeader();

    });

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
