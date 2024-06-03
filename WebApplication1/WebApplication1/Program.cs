var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder .Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
        opt.TokenValidationParameters = new()
        {
            ValidateLifetime = true,
            ClockSkew = Timespan.Zero,
            
            ValidateIssuer = true,
            ValidIssuer = "Lohaclsoso"
            
            ValidateAudience = true,
            ValidAudience = "localhost",
            
            ValidateIssuerSigninKey = true
            IssuerSigninKey = new SymmetrucSecurtyKey(Encoding.UTF8.GetBytes(builder.Configuration["Secret"]))
        };
    )


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/* czy ta osoba to ta osoba*/
app.UseAuthentication();
/* czy ma dostep do zasobow*/
app.UseAuthorization();

app.UseHttpsRedirection();