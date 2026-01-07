using ORM.Services;
using System.Text.Json.Serialization;

namespace API_Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*
             * WebAPI / Restful Service ... bedeuten beide das gleiche
             *
             *      Die WebAPI-Anwendung läuft auf einem Server (24h durchgehend)
             *  wichtige EIgenschaften:
             *  - Client-Server-Architektur (WebAPI = Server, Client = z.B. Webanwendung, Mobile App, ...)
             *  - die Daten werden normalerweise im JSON-Format gesendet und empfangen
             *  - erreichbar ist sie eindeutig über:
             *          1. eine URI (z.B.: https://meinewebapi.de/api/articles)
             *          2. Http-Methoden (GET, POST, PUT, PATCH, DELETE, ...)
             * GET     : Daten vom Server anfordern
             * POST    : Neue Daten auf dem Server anlegen
             * PUT     : Vorhandene Daten auf dem Server komplett ersetzen
             * PATCH   : Vorhandene Daten auf dem Server teilweise aktualisieren
             * DELETE  : Daten auf dem Server löschen
             *
             *
             * Aufbau einer URI
             *     - WICHTIG: es sollte immer die Mehrzahl verwendet werden (Konvention)
             *
             * bsp zu URI's
             * GET     https://meinewebapi.de/api/articles          => holt alle Articles vom Server
             * user mit Id holen: GET https://meinewebapi.de/api/users/5  => holt den User mit der Id 5
             * DETELETE  https://meinewebapi.de/api/articles/3       => löscht den Article mit der Id 3
             * POST    https://meinewebapi.de/api/articles          => legt einen neuen Article an (Daten im Body der Anfrage)
             * PUT     https://meinewebapi.de/api/articles/4        => ersetzt den Article mit der Id 4 (Daten im Body der Anfrage)
             * PATCH   https://meinewebapi.de/api/articles/2        => aktualisiert den Article mit der Id 2 (Daten im Body der Anfrage)
             * DELETE  https://meinewebapi.de/api/articles/7       => löscht den Article mit der Id 7
             *
             * Die Daten werden vom Server an den Client im Body im JSON-Format gesendet
             * in umgekehrte Richtung passiert das gleiche. 
             */
            
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // DI (Dependency Injection)
            builder.Services.AddDbContext<DbManager>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
