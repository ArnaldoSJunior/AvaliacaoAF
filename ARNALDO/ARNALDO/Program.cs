using ARNALDO.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();

builder.Services.AddCors(options =>
options.AddPolicy("Acesso Total",
        configs => configs
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod())
);

var app = builder.Build();

app.MapGet("/", () => "Avaliação Final");

app.MapPost("/aluno/cadastrar", ([FromBody] Aluno Aluno,
[FromServices] AppDataContext context) =>
{
    context.Alunos.Add(Aluno);
    context.SaveChanges();
    return Results.Created($"/funcionario/{Aluno.Id}", Aluno);
});

app.MapPost("/aluno/cadastrarimc", ([FromBody] IMC imc,
[FromServices] AppDataContext context) =>
{
    Aluno? aluno = context.Alunos.Find(imc.AlunoId);
    if (aluno is null)
        return Results.NotFound("Aluno não encontrado");

    imc.Aluno = aluno;

    context.Imc.Add(imc);
    context.SaveChanges();
    return Results.Ok("IMC cadastrado");
});


app.MapGet("/aluno/listarimc", ([FromServices] AppDataContext context) =>
{

    return Results.Ok(context.Imc.ToList());
});


app.UseCors("Acesso Total");
app.Run();
