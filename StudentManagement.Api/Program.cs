
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.OpenApi;
using StudentManagement.Api.Middleware;
using StudentManagement.Application.Shared;
using StudentManagement.Infrastructure.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost" || new Uri(origin).Host == "127.0.0.1")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("FixedPolicy", opt =>
    {
        opt.Window = TimeSpan.FromMinutes(1);    
        opt.PermitLimit = 100;                   
        opt.QueueLimit = 2;
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        
        options.AddSlidingWindowLimiter("SlidingPolicy", opt =>
        {
            opt.Window = TimeSpan.FromMinutes(1);
            opt.PermitLimit = 100;
            opt.SegmentsPerWindow = 4;
        });
        options.AddConcurrencyLimiter("ConcurrencyPolicy", opt =>
        {
            opt.PermitLimit = 10;
        });
    });
});

builder.Services.AddControllers();



builder.Services.AddInfrastructurServiceRegistration(builder.Configuration);
builder.Services.AddApplicationServiceRegistration(builder.Configuration);



builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Student Management API - V1",
            Version = "v1",
            Description = "A sample Student Management API ",
            TermsOfService = new Uri("http://tempuri.org/terms"),
            Contact = new OpenApiContact
            {
                Name = "Bilal",
                Email = "belamraoui92@gmail.com"
            },
            License = new OpenApiLicense
            {
                Name = "Apache 2.0",
                Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
            }
                
        }
    );
    c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("bearer", document)] = []
    });
});



var app = builder.Build();

app.UseCors("AllowFrontend");

// معالجة أخطاء التحقق من البيانات
app.UseValidationExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Student Management API");
        c.DisplayRequestDuration();
        c.EnablePersistAuthorization();
    });
}

app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
