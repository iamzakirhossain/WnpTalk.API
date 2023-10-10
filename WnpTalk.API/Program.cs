using Microsoft.EntityFrameworkCore;
using WnpTalk.API.Controllers.ChatHub;
using WnpTalk.API.Data;
using WnpTalk.API.Functions.ChatRoom;
using WnpTalk.API.Functions.ChatRoomMessage;
using WnpTalk.API.Functions.Message;
using WnpTalk.API.Functions.User;
using WnpTalk.API.Functions.UserFriend;
using WnpTalk.API.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WnpTalkContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddTransient<IUserFunction, UserFunction>();
builder.Services.AddTransient<IUserFriendFunction, UserFriendFunction>();
builder.Services.AddTransient<IMessageFunction, MessageFunction>();
builder.Services.AddTransient<IChatRoomFunction, ChatRoomFunction>();
builder.Services.AddTransient<IChatRoomMessageFunction, ChatRoomMessageFunction>();
builder.Services.AddScoped<UserOperator>();
builder.Services.AddScoped<ChatHub>();
builder.Services.AddScoped<ChatRoom>();

//CORS Start
//builder.Services.AddCors(options => {
//    options.AddPolicy("AllowAll",
//        b => b.AllowAnyMethod()
//        .AllowAnyHeader()
//        .AllowAnyOrigin());
//});
//CORS End

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
//app.UseCors("AllowAll");
//app.UseAuthorization();
app.UseMiddleware<JwtMiddleware>();

//app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/ChatHub");
    endpoints.MapHub<ChatRoom>("/ChatRoom");
});

//Is this Ok?

app.Run();
