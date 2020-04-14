using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Extensions.Configuration;
using Phoenix.Bot.Dialogs.Student;
using System;
using System.Threading;
using System.Threading.Tasks;
using Phoenix.Bot.Dialogs.Teacher;
using Microsoft.Bot.Schema;
using static Phoenix.Bot.Extensions.DialogExtensions;
using Phoenix.Bot.Extensions;
using Phoenix.DataHandle;
using Phoenix.DataHandle.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Phoenix.Bot.Dialogs
{
    public class MainDialog : ComponentDialog
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _scopeFactory;

        public MainDialog(IConfiguration configuration, IServiceScopeFactory scopeFactory)
            : base(nameof(MainDialog))
        {
            _configuration = configuration;
            _scopeFactory = scopeFactory;

            using (var scope = scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<PhoenixContext>();
            }

            AddDialog(new AuthDialog());
            AddDialog(new WelcomeDialog());
            AddDialog(new StudentDialog());
            AddDialog(new TeacherDialog());
            AddDialog(new WaterfallDialog(nameof(MainDialog) + "_" + nameof(WaterfallDialog),
                new WaterfallStep[]
                {
                    FirstTimeStepAsync,
                    GreetingStepAsync,
                    ForwardStepAsync,
                    LoopStepAsync
                }));

            InitialDialogId = nameof(MainDialog) + "_" + nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> FirstTimeStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {


            if (true) //TODO: if is not authenticated
                return await stepContext.BeginDialogAsync(nameof(AuthDialog), null, cancellationToken);
        }

        private async Task<DialogTurnResult> GreetingStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            //TODO: Register user's FB ID to DB and mark them as authenticated
            if (!(stepContext.Result as bool? ?? false))
                return await stepContext.EndDialogAsync();

            string mess = stepContext.Context.Activity.Text;
            if (mess == "--persistent-get-started--")
                return await stepContext.BeginDialogAsync(nameof(WelcomeDialog), null, cancellationToken);

            if (!mess.ContainsSynonyms(SynonymsExtensions.Topics.Greetings))
                return await stepContext.NextAsync(null, cancellationToken);

            var reply = MessageFactory.ContentUrl(url: await ReceiveGifAsync("g", "hi", 10, new Random().Next(10), _configuration["GiphyKey"]),
                contentType: "image/gif");
            await stepContext.Context.SendActivityAsync(reply);

            //TODO: Use DB User name
            reply = MessageFactory.Text($"Γεια σου {GreekNameCall(stepContext.Context.Activity.From.Name.Split(' ')[0])}! 😊");
            await stepContext.Context.SendActivityAsync(reply);

            return await stepContext.NextAsync(null, cancellationToken);
        }

        private async Task<DialogTurnResult> ForwardStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            //TODO: Find user type
            string userType = "student";

            return userType switch
            {
                "student" => await stepContext.BeginDialogAsync(nameof(StudentDialog), null, cancellationToken),
                "teacher" => await stepContext.BeginDialogAsync(nameof(TeacherDialog), null, cancellationToken),
                _ => new DialogTurnResult(DialogTurnStatus.Cancelled)
            };
        }

        private async Task<DialogTurnResult> LoopStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.ReplaceDialogAsync(nameof(MainDialog) + "_" + nameof(WaterfallDialog), null, cancellationToken);
        }
    }
}
