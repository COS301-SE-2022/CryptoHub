using BusinessLogic.Services.AuthorizationService;
using BusinessLogic.Services.RoleServices;
using BusinessLogic.Services.UserService;
using BusinessLogic.Services.CoinService;
using BusinessLogic.Services.CoinRatingService;
using BusinessLogic.Services.UserCoinService;
using BusinessLogic.Services.UserFollowerService;
using BusinessLogic.Services.SearchService;
using BusinessLogic.Services.CommentService;
using BusinessLogic.Services.ImageService;
using BusinessLogic.Services.TagService;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using BusinessLogic.Services.LikeService;
using BusinessLogic.Services.PostService;
using BusinessLogic.Services.ReplyService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Intergration.SendGridEmailService;
using BusinessLogic.Services.ImageService;
using BusinessLogic.Services.CommentService;
using BusinessLogic.Services.TagService;
using Intergration.FireStoreService;
using Intergration.SendInBlueEmailService;
using Infrastructure.Setting;
using Microsoft.Extensions.DependencyInjection;
using CryptoHubAPI;
using CryptoHubAPI.Hubs;
using BusinessLogic.Services.MessageService;
using BusinessLogic.Services.NotificationService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<MyInitializer>();

//await Doppler.FetchSecretsAsync();
// Repository Dependency Injection.

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserFollowerRepository, UserFollowerRepository>();
builder.Services.AddTransient<IPostRepository, PostRepository>();
builder.Services.AddTransient<ICoinRepository, CoinRepository>();
builder.Services.AddTransient<ILikeRepository, LikeRepository>();
builder.Services.AddTransient<ICommentRepository, CommentRepository>();
builder.Services.AddTransient<IReplyRepository, ReplyRepository>();
builder.Services.AddTransient<IImageRepository, ImageRepository>();
builder.Services.AddTransient<ITagRepository, TagRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<ICoinRatingRepository, CoinRatingRepository>();
builder.Services.AddTransient<IUserCoinRepository, UserCoinRepository>();
builder.Services.AddTransient<IPostReportRepository, PostReportRepository>();
builder.Services.AddTransient<IMessageRepository, MessageRepository>();
builder.Services.AddTransient<INotificationRepository, NotificationRepository>();
builder.Services.AddTransient<IPostTagRepository, PostTagRepository>();



//Services Dependency Injection.
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAuthorizationService, AuthorizationService>();
builder.Services.AddTransient<ICoinService, CoinService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<ILikeService, LikeService>();
builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<IReplyService, ReplyService>();
builder.Services.AddTransient<ICoinRatingService, CoinRatingService>();
builder.Services.AddTransient<IUserCoinService, UserCoinService>();
builder.Services.AddTransient<IUserFollowerService, UserFollowerService>();
builder.Services.AddTransient<ISearchService, SearchService>();
builder.Services.AddTransient<ISendGridEmailService, SendGridEmailService>();
builder.Services.AddTransient<IImageService, ImageService>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<ITagService, TagServices>();
builder.Services.AddTransient<IFireStorageService, FireStorageService>();
builder.Services.AddTransient<ISendInBlueEmailService, SendInBlueEmailService>();
builder.Services.AddTransient<IMessageService, MessageService>();
builder.Services.AddTransient<INotificationService, NotificationService>();


//AutoMapper
builder.Services.AddAutoMapper(Assembly.Load("Infrastructure"));

builder.Services.AddHttpContextAccessor();

builder.Services.AddCors();

builder.Services.AddSignalR();

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = JWTSettings.Audience,
        ValidIssuer = JWTSettings.Issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings.Key))
    };
});

builder.Services.AddDbContext<CryptoHubDBContext>(
    options =>
    {
        if(builder.Environment.IsDevelopment())
            options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"));
        else
            options.UseSqlServer(DBConnctionSettings.ConnectionString);

    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        config.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        config.DisplayRequestDuration();
    });
}*/

app.UseSwagger();
app.UseSwaggerUI(config =>
{
    config.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    config.DisplayRequestDuration();
});

app.UseCors(
    options =>
    {
        options.
        WithOrigins("null","http://localhost:3000").
        AllowAnyMethod().
        AllowAnyHeader().
        AllowCredentials();

    });

app.MapHub<MessageHub>("/messagehub");


app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
