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

namespace Phoenix.Bot.Dialogs
{
    public class MainDialog : ComponentDialog
    {
        public IConfiguration Configuration { get; }
        public PhoenixContext DBContext { get; }

        public MainDialog(IConfiguration configuration, PhoenixContext phoenixContext)
            : base(nameof(MainDialog))
        {
            Configuration = configuration;
            DBContext = phoenixContext;

            AddDialog(new AuthDialog(DBContext));
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
            if (!(stepContext.Result as bool? ?? false))
                return await stepContext.EndDialogAsync();

            string mess = stepContext.Context.Activity.Text;
            if (mess == "--persistent-get-started--")
                return await stepContext.BeginDialogAsync(nameof(WelcomeDialog), null, cancellationToken);

            if (!mess.ContainsSynonyms(SynonymsExtensions.Topics.Greetings))
                return await stepContext.NextAsync(null, cancellationToken);

            var reply = MessageFactory.ContentUrl(url: await ReceiveGifAsync("g", "hi", 10, new Random().Next(10), Configuration["GiphyKey"]),
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
