using BusinessLogic.Services.UserService;
using Domain.Infrastructure;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Repository Dependency Injection.

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserFollowerRepository, UserFollowerRepository>();
builder.Services.AddTransient<IPostRepository, PostRepository>();
builder.Services.AddTransient<ICoinRepository, CoinRepository>();
builder.Services.AddTransient<ICoinHistoryRepository, CoinHistoryRepository>();
builder.Services.AddTransient<ILikeRepository, LikeRepository>();
builder.Services.AddTransient<ICommentRepository, CommentRepository>();
builder.Services.AddTransient<IReplyRepository, ReplyRepository>();

//Services Dependency Injection.
builder.Services.AddTransient<IUserService, UserService>();




//


builder.Services.AddCors();


builder.Services.AddControllers();

builder.Services.AddDbContext<CryptoHubDBContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"));
    });

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
